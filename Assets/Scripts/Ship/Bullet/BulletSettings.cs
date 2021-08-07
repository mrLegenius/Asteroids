using UnityEngine;

namespace Asteroids.Ship.Bullet
{
[CreateAssetMenu]
public class BulletSettings : ScriptableObject
{
    public BulletView Prefab;
    public float Speed;
    public float LifeTime;
}
}