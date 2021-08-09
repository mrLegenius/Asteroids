using UnityEngine;

namespace Asteroids.Asteroid
{
[CreateAssetMenu(menuName = "Settings/Asteroids")]
public class AsteroidsSettings : ScriptableObject
{ 
    [SerializeField] private AsteroidView[] _bigAsteroids; 
    [SerializeField] private AsteroidView[] _mediumAsteroids;
    [SerializeField] private AsteroidView[] _smallAsteroids;

    public float SpeedModifier = 1.2f;

    public float MinInitialSpeed;
    public float MaxInitialSpeed;
    
    public float SplitCount;
    public float SplitSpreadAngle;
    
    public AudioClip ExplosionClip;
    public ParticleSystem ExplosionParticles;

    public AsteroidView GetRandomAsteroidPrefab(AsteroidType type)
    {
        return type switch
        {
            AsteroidType.Big => _bigAsteroids[Random.Range(0, _bigAsteroids.Length)],
            AsteroidType.Medium => _mediumAsteroids[Random.Range(0, _mediumAsteroids.Length)],
            AsteroidType.Small => _smallAsteroids[Random.Range(0, _smallAsteroids.Length)],
            _ => null
        };
    }
}
}