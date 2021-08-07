using UnityEngine;

namespace Asteroids.Ship
{
[CreateAssetMenu]
public class ShipSettings : ScriptableObject
{
    [Header("General")]
    public Vector2 InitialPosition;

    [Header("Movement")]
    public float MaxSpeed;
    public float Acceleration;
    public float Deceleration;
    public float RotationSpeed;

    [Header("Shooting")]
    public float FireRate;

    [Header("Laser Firing")]
    public int LasersCount;
    public float LaserReload;
}
}