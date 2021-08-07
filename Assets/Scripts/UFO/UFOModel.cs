using UnityEngine;

namespace Asteroids.UFO
{
public class UFOModel
{
    public Vector2 Position { get; set; }
    
    public float Angle { get; set; }
    public float Speed { get; set; }
    
    public int ScoreOnDestroyed { get; set; }
}
}