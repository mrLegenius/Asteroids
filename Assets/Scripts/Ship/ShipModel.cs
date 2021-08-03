using Asteroids.Ship.Bullet;
using UnityEngine;

namespace Asteroids.Models
{
public class Movement
{
    public float Speed { get; set; }
    public float MaxSpeed { get; set; }
    public float Acceleration { get; set; }
    public float Deceleration { get; set; }
    public Vector2 Velocity { get; set; }
    public float RotationSpeed { get; set; }
}

public class Shooting
{
    public BulletView BulletPrefab { get; set; }

    public float FireRate
    {
        get => _fireRate;
        set
        {
            //if value equals zero
            if (Mathf.Abs(value) <= float.Epsilon)
            {
                _fireRate = 0;
                FireDelay = 0;
            }
            else
            {
                _fireRate = value;
                FireDelay = 1f / value;
            }
        }
    }

    public float FireDelay { get; private set; }
    
    public float FireTimer { get; set; }
    
    public bool CanShoot => FireTimer <= 0;

    private float _fireRate;
}
public class LaserFiring
{
    public int MaxLaserCount { get; set; }
    public int LaserCount { get; set; }
    public float LaserCooldown { get; set; }
    public float LaserCurrentCooldown { get; set; }
}
public class ShipModel
{
    public Vector2 Coord { get; set; }
    public float Angle { get; set; }
    public Movement Movement { get; set; }
    public Shooting Shooting { get; set; }
    public LaserFiring LaserFiring { get; set; }
}
}