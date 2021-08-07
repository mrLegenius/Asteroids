using UnityEngine;

namespace Asteroids.UFO
{
[CreateAssetMenu]
public class UFOSettings : ScriptableObject
{
    public UFOView Prefab;
    public float Speed;
    public int Score;
}
}