using System;
using Asteroids.Models;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Views
{
public class ShipView : MonoBehaviour
{
    private Transform _transform;

    [SerializeField]
    private InputAction _moveAction;
    
    [SerializeField]
    private InputAction _fireAction;

    [SerializeField]
    private InputAction _laserAction;
    
    private Action<Vector2> _moved;
    private Action<bool> _shoot;
    private void Awake()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        _moveAction.Enable();
        _fireAction.Enable();
        _laserAction.Enable();
    }
    
    private void OnDisable()
    {
        _moveAction.Disable();
        _fireAction.Disable();
        _laserAction.Disable();
    }

    public void Update()
    {
        _moved?.Invoke(_moveAction.ReadValue<Vector2>());
        _shoot?.Invoke(_fireAction.ReadValue<bool>());
    }

    public ShipView OnMoved(Action<Vector2> callback)
    {
        _moved = callback;
        return this;
    }
    public ShipView OnShoot(Action<bool> callback)
    {
        _shoot = callback;
        return this;
    }
    public ShipView OnLaserFired(Action callback)
    {
       _fireAction.performed += context =>
        {
            callback.Invoke();
        };
        return this;
    }
    
    public void Repaint(ShipModel model)
    {
        _transform.position = model.Coord;

        var rotation = _transform.rotation;
        rotation.eulerAngles = new Vector3(0, 0, model.Angle);
        _transform.rotation = rotation;
    }
}
}