using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Asteroids.Ship.Bullet
{
public class BulletsController : ITickable, IInitializable, IDisposable
{
    private class Bullet
    {
        public BulletView View { get; set; }
        public BulletModel Model { get; set; }
    }

    private readonly List<Bullet> _bullets = new List<Bullet>();
    private readonly Pool<Bullet> _pool = new Pool<Bullet>();

    private readonly Transform _bulletsContainer;
    private readonly BulletSettings _bulletSettings;
    private readonly SignalBus _signalBus;

    public BulletsController(BulletSettings bulletSettings,
        SignalBus signalBus)
    {
        _bulletSettings = bulletSettings;
        _signalBus = signalBus;
        _bulletsContainer = new GameObject("Bullets").transform;

        _pool
            .SetConstructor(ConstructBullet)
            .OnPopped(bullet =>
            {
                bullet.View.gameObject.SetActive(true);
                _bullets.Add(bullet);
            })
            .OnPushed(bullet =>
            {
                bullet.View.gameObject.SetActive(false);
            })
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
        foreach (var bullet in _bullets)
        {
            var model = bullet.Model;

            Move(model);
            Repaint(bullet);

            model.LifeTimer -= Time.deltaTime;

            if (model.ShouldBeDestroyed) Destroy(bullet);
        }

        _bullets.RemoveAll(x => x.Model.ShouldBeDestroyed);
    }

    public void CreateBullet(Vector2 position, float angle)
    {
        var bullet = _pool.Pop();

        var model = bullet.Model;
        model.Position = position;
        model.Angle = angle;
        model.LifeTimer = _bulletSettings.LifeTime;
    }

    private Bullet ConstructBullet()
    {
        var view =
            Object.Instantiate(_bulletSettings.Prefab,
                Utilities.GetFarPoint(),
                Quaternion.identity,
                _bulletsContainer);

        var model = new BulletModel { MoveSpeed = _bulletSettings.Speed };

        view.OnCollided(OnBulletCollided);

        return new Bullet
        {
            View = view,
            Model = model
        };
    }

    private void Move(BulletModel model)
    {
        var direction =
            Utilities.GetDirectionFromAngle(model.Angle * Mathf.Deg2Rad);
        model.Position += model.MoveSpeed * Time.deltaTime * direction;
        model.Position =
            Utilities.GetWrapAroundPosition(model.Position);
    }

    private void Repaint(Bullet bullet)
    {
        bullet.View.Repaint(bullet.Model);
    }

    private void Destroy(Bullet bullet)
    {
        _pool.Push(bullet);
    }

    private void OnBulletCollided(Collider2D other, BulletView view)
    {
        var bullet = _bullets.FirstOrDefault(x => x.View == view);
        if(bullet == null)
            return;
        
        Destroy(bullet);
        _bullets.Remove(bullet);
    }

    private void OnShipDestroyed(ShipDestroyedSignal signal)
    {
        foreach (var bullet in _bullets)
        {
            Object.Destroy(bullet.View.gameObject);
        }
        _bullets.Clear();
        _pool.Clear();
    }
}
}