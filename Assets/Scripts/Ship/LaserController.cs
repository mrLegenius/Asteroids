using System;
using Asteroids.Models;
using UnityEngine;
using Zenject;

namespace Asteroids.Ship
{
public class LaserController : ITickable
{
    private readonly LaserFiring _laser;
    private readonly ShipModel _ship;
    private readonly ShipSettings _settings;

    public LaserController(ShipModel shipModel, ShipSettings shipSettings)
    {
        _ship = shipModel;
        _laser = shipModel.LaserFiring;
        _settings = shipSettings;
    }

    public void Fire()
    {
        if (_laser.Count <= 0 || _laser.IsFiring)
            return;

        _laser.Timer = _settings.LaserDuration;
        _laser.Count--;
        _laser.IsFiring = true;
    }

    public void Tick()
    {
        if (_laser.IsFiring)
        {
            _laser.Timer -= Time.deltaTime;
            CastRay();
            if (_laser.Timer <= float.Epsilon) _laser.IsFiring = false;
        }

        if (_laser.Count >= _settings.LasersCount) return;

        _laser.Cooldown -= Time.deltaTime;

        if (_laser.Cooldown >= float.Epsilon) return;

        _laser.Count++;
        _laser.Cooldown = _settings.LaserReload;
    }

    private void CastRay()
    {
        var rayDirection =
            Utilities.GetDirectionFromAngle(_ship.Angle * Mathf.Deg2Rad);
        var hits = Physics2D.RaycastAll(_ship.Position,
            rayDirection,
            1_000,
            _settings.Layer);

        foreach (var hit in hits)
        {
            var hittable = hit.collider.GetComponent<IRayHittable>();

            hittable?.Hit();
        }
    }
}
}