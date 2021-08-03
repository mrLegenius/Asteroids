using UnityEngine;

namespace Asteroids
{
public static class Utilities
{
    /// <summary>
    /// Calculate radius-vector with given angle
    /// </summary>
    /// <param name="angle">in radians</param>
    /// <returns></returns>
    public static Vector2 GetDirectionFromAngle(float angle)
    {
        var x = Mathf.Cos(angle);
        var y = Mathf.Sin(angle);

        return new Vector2(x, y);
    }
}
}