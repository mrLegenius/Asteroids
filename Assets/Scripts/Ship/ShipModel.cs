using Asteroids.Ship.Bullet;
using UnityEngine;

namespace Asteroids.Models
{
public class Movement
{
    public float Speed => Velocity.magnitude;
    public float MaxSpeed { get; set; }
    public float Acceleration { get; set; }
    public float Deceleration { get; set; }
    public float RotationSpeed { get; set; }
    public Vector2 Velocity { get; set; }
}

public class Shooting
{
    public float FireRate
    {
        get => _fireRate;
        set
        {
            //if value equals zero
            if (Mathf.Abs(value) <= float.Epsilon)
            {
                _fireRate = 0;
            }
            else
            {
                _fireRate = 1f / value;
            }
        }
    }

    public float FireTimer { get; set; }
    
    public bool CanShoot => FireTimer <= 0;

    private float _fireRate;
}
public class LaserFiring
{
    public int Count { get; set; }
    public float Cooldown { get; set; }
    public float Timer { get; set; }

    public bool IsFiring { get; set; }

    public bool CanFire => Count > 0 && !IsFiring;
}
public class ShipModel
{
    public Vector2 Position { get; set; }
    public float Angle { get; set; }
    public Movement Movement { get; set; }
    public Shooting Shooting { get; set; }
    public LaserFiring LaserFiring { get; set; }
}
}