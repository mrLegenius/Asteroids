using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Asteroids.Asteroid
{
public class AsteroidsController : ITickable, IInitializable, IDisposable
{
    private class PooledAsteroid
    {
        public bool IsActive { get; set; }

        public AsteroidView View { get; set; }

        public AsteroidModel Model { get; set; }
    }

    private readonly List<PooledAsteroid> _asteroids = new List<PooledAsteroid>();

    private IEnumerable<PooledAsteroid> AvailableSmallAsteroids
        => _asteroids.Where(x
            => !x.IsActive && x.Model.Type == AsteroidType.Small);

    private IEnumerable<PooledAsteroid> AvailableMediumAsteroids
        => _asteroids.Where(x
            => !x.IsActive && x.Model.Type == AsteroidType.Medium);

    private IEnumerable<PooledAsteroid> AvailableBigAsteroids
        => _asteroids.Where(x
            => !x.IsActive && x.Model.Type == AsteroidType.Big);
    
    private IEnumerable<PooledAsteroid> ActiveAsteroids
        => _asteroids.Where(x => x.IsActive);
    
    private readonly AsteroidsSet _asteroidsSet;

    private readonly Transform _asteroidsContainer;

    private readonly SignalBus _signalBus;

    private bool _isGameStarted = false;
    
    public AsteroidsController(AsteroidsSet asteroidsSet, SignalBus signalBus)
    {
        _asteroidsSet = asteroidsSet;
        _signalBus = signalBus;
        
        _asteroidsContainer = new GameObject("Asteroids").transform;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
    }
    
    public void CreateAsteroid(AsteroidType type,
        Vector2 position,
        float angle)
    {
        var pooledAsteroid = type switch
        {
            AsteroidType.Big => AvailableBigAsteroids.FirstOrDefault(),
            AsteroidType.Medium =>
                AvailableMediumAsteroids.FirstOrDefault(),
            AsteroidType.Small =>
                AvailableSmallAsteroids.FirstOrDefault(),
            _ => null
        };

        if (pooledAsteroid != null)
        {
            pooledAsteroid.View.gameObject.SetActive(true);

            var model = pooledAsteroid.Model;
            model.Position = position;
            model.Angle = angle;
            model.Speed = model.GetRandomSpeed();

            pooledAsteroid.IsActive = true;
        }
        else
        {
            var prefab = _asteroidsSet.GetRandomAsteroidPrefab(type);
            var view = Object.Instantiate(prefab, _asteroidsContainer);

            var model = new AsteroidModel
            {
                Position = position,
                Angle = angle,
                MinSpeed = 10,
                MaxSpeed = 20
            };
            model.Speed = model.GetRandomSpeed();
            
            _asteroids.Add(new PooledAsteroid
            {
                View = view,
                Model = model,
                IsActive = true
            });
        }
    }

    public void Tick()
    {
        if(!_isGameStarted) return;
        
        var activeAsteroids = ActiveAsteroids.ToList();
        if (!activeAsteroids.Any())
        {
            _signalBus.Fire<DestroyedAllAsteroidsSignal>();
            return;
        }
        
        foreach (var asteroid in activeAsteroids)
        {
            var model = asteroid.Model;
            var view = asteroid.View;

            model.Position =
                Utilities.GetWrapAroundPosition(model.Position);
            
            MoveAsteroid(model);
            RepaintView(view, model);
        }
    }
    
    private void MoveAsteroid(AsteroidModel model)
    {
        var direction = Utilities.GetDirectionFromAngle(model.Angle * Mathf.Deg2Rad);
        model.Position += model.Speed * Time.deltaTime * direction;
    }

    private void RepaintView(AsteroidView view, AsteroidModel model)
    {
        view.Repaint(model);
    }

    private void OnGameStarted(GameStartedSignal signal)
    {
        _isGameStarted = true;
    }
}
}