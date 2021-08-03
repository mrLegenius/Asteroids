using UnityEngine;

namespace Asteroids.Asteroid
{
/// <summary>
/// Набор префабов астероидов разных размеров
/// </summary>
[CreateAssetMenu(menuName = "Asteroids")]
public class AsteroidsSet : ScriptableObject
{ 
    [SerializeField] private AsteroidView[] _bigAsteroids; 
    [SerializeField] private AsteroidView[] _mediumAsteroids;
    [SerializeField] private AsteroidView[] _smallAsteroids;
    
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