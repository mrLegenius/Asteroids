using System;
using UnityEngine;

namespace Asteroids.Ship.Bullet
{
public class BulletView : MonoBehaviour
{
    private Transform _transform;

    private Action<Collider2D, BulletView> _collided;
    
    private void Awake()
    {
        _transform = transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _collided?.Invoke(other, this);
    }

    public void Repaint(BulletModel model)
    {
        Debug.Log($"Bullet Speed = {model.MoveSpeed}");
        
        _transform.position = model.Position;

        var rotation = _transform.rotation;
        rotation.eulerAngles = new Vector3(0, 0, model.Angle);
        _transform.rotation = rotation;
    }

    public void OnCollided(Action<Collider2D, BulletView> callback)
    {
        _collided = callback;
    }
}
}