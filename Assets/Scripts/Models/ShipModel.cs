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
public class ShipModel
{
    public Vector2 Coord { get; set; }
    
    public float Angle { get; set; }
    
    public Movement Movement { get; set; }
    
    public int LaserCount { get; set; }

    public float LaserCooldown { get; set; }

    public GameObject BulletPrefab { get; set; }

    public float FireRate { get; set; }
}
}