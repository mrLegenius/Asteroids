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
    
    private static Camera _mainCamera;
    public static Camera GetMainCamera()
    {
        if(!_mainCamera)
            _mainCamera = Camera.main;

        return _mainCamera;
    }

    public static Vector2 GetRandomPositionOutOfScreen(Vector2 offset)
    {
        var x = Screen.width + offset.x;
        var y = Random.Range(-offset.y, Screen.height + offset.y);
        return GetMainCamera().ScreenToWorldPoint(new Vector2(x, y));
    }

    public static float GetRandomAngle()
    {
       return Random.Range(-180f, 180f);
    }

    public static Vector2 GetWrapAroundPosition(Vector2 currentPosition)
    {
        var offset = new Vector2(0.1f, 0.1f);
        var beyondScreenSpace = new Vector2(50, 50);
        var screenPos = _mainCamera.WorldToScreenPoint(currentPosition);
        
        if (screenPos.x < -beyondScreenSpace.x)
            currentPosition.x = -currentPosition.x - offset.x;
        
        if(screenPos.x > _mainCamera.pixelWidth + beyondScreenSpace.x)
            currentPosition.x = -currentPosition.x + offset.x;

        if (screenPos.y < -beyondScreenSpace.y)
            currentPosition.y = -currentPosition.y - offset.y;
        
        if(screenPos.y > _mainCamera.pixelHeight + beyondScreenSpace.y)
            currentPosition.y = -currentPosition.y + offset.x;
        
        return currentPosition;
    }
}
}