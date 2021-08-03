using UnityEngine;

namespace Asteroids.Ship.Bullet
{
public class BulletView : MonoBehaviour
{
    private Transform _transform;
    
    private void Awake()
    {
        _transform = transform;
    }
    
    public void Repaint(BulletModel model)
    {
        _transform.position = model.Position;

        var rotation = _transform.rotation;
        rotation.eulerAngles = new Vector3(0, 0, model.Angle);
        _transform.rotation = rotation;
    }
}
}