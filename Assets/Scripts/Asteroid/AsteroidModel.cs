

using UnityEngine;

namespace Asteroids.Asteroid
{
public enum AsteroidType
{
    Big,
    Medium,
    Small
}

public class AsteroidModel
{
    public Vector2 Position { get; set; }
    public float Angle { get; set; }

    public float Speed { get; set; }
    public float MinSpeed { get; set; }
    public float MaxSpeed { get; set; }
    
    
    public float AsteroidSpawnSpread { get; set; }
    
    public int ScoreOnDestroyed => CalculateScore();
    public AsteroidType Type { get; set; }

    private int CalculateScore()
    {
        return Type switch
        {
            AsteroidType.Big => 100,
            AsteroidType.Medium => 50,
            AsteroidType.Small => 20,
            _ => 0
        };
    }

    public float GetRandomSpeed()
    {
        return Random.Range(MinSpeed, MaxSpeed);
    }
}
}