using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Models;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Asteroids.UFO
{
public class UFO
{
    public UFOView View { get; set; }
    public UFOModel Model { get; set; }
}

public class UFOController : ITickable, IInitializable, IDisposable
{
    private readonly SignalBus _signalBus;
    private readonly ShipModel _ship;
    private readonly UFOSettings _ufoSettings;
    private readonly Transform _ufoContainer;

    private readonly Pool<UFO> _pool = new Pool<UFO>();
    private readonly List<UFO> _ufos = new List<UFO>();

    public UFOController(SignalBus signalBus,
        UFOSettings settings,
        ShipModel shipModel)
    {
        _signalBus = signalBus;
        _ship = shipModel;
        _ufoSettings = settings;

        _ufoContainer = new GameObject("UFO").transform;
        _pool
            .SetConstructor(ConstructUFO)
            .OnPopped(ufo =>
            {
                ufo.View.gameObject.SetActive(true);
                _ufos.Add(ufo);
            })
            .OnPushed(ufo => { ufo.View.gameObject.SetActive(false); })
            .OnCleared(bullets => bullets.ForEach(x
                => Object.Destroy(x.View.gameObject)));
    }

    public void Initialize()
    {
        _signalBus.Subscribe<ShipDestroyedSignal>(OnShipDestroyed);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<ShipDestroyedSignal>(OnShipDestroyed);
    }

    public void Tick()
    {
        foreach (var ufo in _ufos)
        {
            Move(ufo.Model);
            Repaint(ufo);
        }
    }

    public void CreateUFO(Vector2 position, float angle)
    {
        var ufo = _pool.Pop();

        var model = ufo.Model;
        model.Position = position;
        model.Angle = angle;
    }

    private UFO ConstructUFO()
    {
        var ufo = new UFO
        {
            View = Object.Instantiate(_ufoSettings.Prefab,
                Utilities.GetFarPoint(),
                Quaternion.identity,
                _ufoContainer),
            Model = new UFOModel
            {
                ScoreOnDestroyed = _ufoSettings.Score,
                Speed = _ufoSettings.Speed
            }
        };

        ufo.View.OnCollided(OnUFOCollided);
        ufo.View.OnRayHit(OnUFOHitByRay);
        return ufo;
    }

    private void Move(UFOModel model)
    {
        var direction = _ship.Position - model.Position;
        model.Position +=
            model.Speed * Time.deltaTime * direction.normalized;
        model.Position =
            Utilities.GetWrapAroundPosition(model.Position);
    }

    private void Repaint(UFO ufo) { ufo.View.Repaint(ufo.Model); }

    private void Destroy(UFO ufo)
    {
        _pool.Push(ufo);
        _ufos.Remove(ufo);

        _signalBus.Fire(
            new UFODestroyedSignal(ufo.Model.ScoreOnDestroyed));
    }

    private void OnUFODestroyed(UFOView view)
    {
        var ufo = _ufos.FirstOrDefault(x => x.View == view);
        if (ufo == null) return;

        Destroy(ufo);
    }


    private void OnUFOCollided(Collider2D other, UFOView view)
    {
        OnUFODestroyed(view);
    }

    private void OnUFOHitByRay(UFOView view)
    {
        OnUFODestroyed(view);
    }

    private void OnShipDestroyed(ShipDestroyedSignal signal)
    {
        foreach (var ufo in _ufos)
        {
            Object.Destroy(ufo.View.gameObject);
        }

        _ufos.Clear();
        _pool.Clear();
    }
}
}