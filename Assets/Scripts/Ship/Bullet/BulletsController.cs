using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Asteroids.Ship.Bullet
{
public class BulletsController : ITickable
{
    private class PooledBullet
    {
        public bool IsActive { get; set; }
        public BulletView View { get; set; }
        public BulletModel Model { get; set; }
    }

    private readonly List<PooledBullet> _bullets = new List<PooledBullet>();

    private IEnumerable<PooledBullet> AvailableBullets
        => _bullets.Where(bullet => !bullet.IsActive);
    private IEnumerable<PooledBullet> ActiveBullets
        => _bullets.Where(bullet => bullet.IsActive);

    private readonly Transform _bulletsContainer;
    
    public BulletsController()
    {
        _bulletsContainer = new GameObject("Bullets").transform;
    }
    
    public void CreateBullet(Vector2 position, float angle, BulletView prefab)
    {
        var pooledBullet =
            AvailableBullets.FirstOrDefault(x => !x.IsActive);
        
        if (pooledBullet != null)
        {
            pooledBullet.View.gameObject.SetActive(true);

            var model = pooledBullet.Model;
            model.Position = position;
            model.Angle = angle;
            model.LifeTimer = model.LifeTime;

            pooledBullet.IsActive = true;
        }
        else
        {
            var bulletView = Object.Instantiate(prefab, _bulletsContainer);

            var bulletModel = new BulletModel
            {
                Position = position,
                Angle = angle,
                MoveSpeed = 30,
                LifeTime = 2,
                LifeTimer = 2
            };
            
            _bullets.Add(new PooledBullet
            {
                View = bulletView,
                Model = bulletModel,
                IsActive = true
            });
        }
    }

    public void Tick()
    {
        foreach (var bullet in ActiveBullets)
        {
            var model = bullet.Model;
            var view = bullet.View;
            
            MoveBullet(model);
            RepaintView(view, model);

            model.LifeTimer -= Time.deltaTime;

            if (IsBulletIsDead(model))
                DisableBullet(bullet);
        }
    }

    private void MoveBullet(BulletModel model)
    {
        var direction = Utilities.GetDirectionFromAngle(model.Angle * Mathf.Deg2Rad);
        model.Position += model.MoveSpeed * Time.deltaTime * direction;
    }

    private void RepaintView(BulletView view, BulletModel model)
    {
        view.Repaint(model);
    }

    private bool IsBulletIsDead(BulletModel model)
    {
        return model.LifeTimer <= 0.0;
    }

    private void DisableBullet(PooledBullet bullet)
    {
        bullet.IsActive = false;
        bullet.View.gameObject.SetActive(false);
    }
}
}