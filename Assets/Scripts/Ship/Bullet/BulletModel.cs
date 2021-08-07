using UnityEngine;

namespace Asteroids.Ship.Bullet
{
public class BulletModel
{
    public Vector2 Position { get; set; }
    
    public float Angle { get; set; }
    
    public float MoveSpeed { get; set; }

    public float LifeTimer { get; set; }

    public bool ShouldBeDestroyed => LifeTimer <= 0;
}
}