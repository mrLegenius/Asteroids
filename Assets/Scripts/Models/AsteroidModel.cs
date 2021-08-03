namespace Asteroids.Models
{
public enum AsteroidType
{
    Big,
    Medium,
    Small
}

public class AsteroidModel
{
    public float MinSpeed { get; set; }
    public float MaxSpeed { get; set; }
    public float AsteroidSpawnSpread { get; set; }
    public int ScoreOnDestroyed { get; set; }
    public AsteroidType Type { get; set; }
}
}