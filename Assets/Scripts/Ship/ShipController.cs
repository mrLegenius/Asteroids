using Asteroids.Models;
using Asteroids.Views;
using UnityEngine;

public class ShipController
{
    private readonly ShipView _shipView;
    private ShipModel _shipModel;
    
    public ShipController(ShipView shipView)
    {
        _shipView = shipView;
        
        Init();
        _shipModel = new ShipModel
        {
            Movement = new Asteroids.Models.Movement
            {
                MaxSpeed = 10,
                Acceleration = 20,
                Deceleration = 10,
                RotationSpeed = 180
            }
        };
    }

    public void Init()
    {
        _shipView
            .OnMoved(HandleMove)
            .OnShoot(HandleShoot)
            .OnLaserFired(HandleLaserFired);
    }
    

    public void HandleMove(Vector2 input)
    {
        var movement = _shipModel.Movement;
        
        RotateShip(input, movement);
        CalculateVelocity(input, movement);
        _shipModel.Coord += movement.Velocity * Time.deltaTime;
        Repaint();
    }

    private void CalculateVelocity(Vector2 input, Asteroids.Models.Movement movement)
    {
        var moveDirection = GetMoveDirection();

        if (input.y > float.Epsilon)
        {
            movement.Velocity +=
                movement.Acceleration * Time.deltaTime * moveDirection;
        }
        else
        {
            movement.Velocity =
                Vector2.MoveTowards(movement.Velocity,
                    Vector2.zero,
                    movement.Deceleration * Time.deltaTime);
        }

        Vector2.ClampMagnitude(movement.Velocity, movement.MaxSpeed);
    }

    private Vector2 GetMoveDirection()
    {
        var x = Mathf.Cos(_shipModel.Angle * Mathf.Deg2Rad);
        var y = Mathf.Sin(_shipModel.Angle * Mathf.Deg2Rad);
        
        return new Vector2(x, y);
    }

    private void RotateShip(Vector2 input, Asteroids.Models.Movement movement)
    {
        var rotationSpeed = movement.RotationSpeed * Time.deltaTime;

        if (input.x > float.Epsilon)
        {
            _shipModel.Angle -= rotationSpeed;
        }

        if (input.x < -float.Epsilon)
        {
            _shipModel.Angle += rotationSpeed;
        }
    }

    public void HandleShoot(bool value)
    {
        //TODO: add shooting
    }

    public void HandleLaserFired()
    {
        //TODO: add laser firing
        Debug.Log("Fire Laser");
    }
    
    public void Repaint()
    {
        _shipView.Repaint(_shipModel);
    }
}
