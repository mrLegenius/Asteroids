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
    public AudioClip ShootingClip;

    [Header("Laser Firing")]
    public int LasersCount;
    public float LaserReload;
    public float LaserDuration;
    public LayerMask Layer;
    public AudioClip LaserClip;
}
}