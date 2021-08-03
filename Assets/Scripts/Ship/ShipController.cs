using Asteroids.Models;
using Asteroids.Ship.Bullet;
using Asteroids.Views;
using UnityEngine;

namespace Asteroids.Ship
{
    public class ShipController
    {
        private readonly ShipView _shipView;
        private readonly ShipModel _shipModel;

        private readonly BulletsController _bulletsController;
        
        public ShipController(ShipView shipView, 
            BulletsController bulletsController)
        {
            _shipView = shipView;
            _bulletsController = bulletsController;
        
            Init();
            _shipModel = new ShipModel
            {
                Movement = new Movement
                {
                    MaxSpeed = 10,
                    Acceleration = 20,
                    Deceleration = 10,
                    RotationSpeed = 180
                },
                Shooting = new Shooting
                {
                    BulletPrefab = Resources.Load<BulletView>("Bullet"),
                    FireRate = 10
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
        private void HandleMove(Vector2 input)
        {
            var movement = _shipModel.Movement;
        
            RotateShip(input, movement);
            CalculateVelocity(input, movement);
            _shipModel.Position += movement.Velocity * Time.deltaTime;
            Repaint();
        }
        private void RotateShip(Vector2 input, Movement movement)
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
        private void CalculateVelocity(Vector2 input, Movement movement)
        {
            var angle = _shipModel.Angle * Mathf.Deg2Rad;
            var moveDirection = Utilities.GetDirectionFromAngle(angle);

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

        private void HandleShoot(bool shootCommanded)
        {
            var shooting = _shipModel.Shooting;
            shooting.FireTimer -= Time.deltaTime;

            if (!shooting.CanShoot || !shootCommanded) return;

        _bulletsController.CreateBullet(_shipModel.Position, 
                _shipModel.Angle, 
                shooting.BulletPrefab);
            
            shooting.FireTimer = shooting.FireDelay;
        }

        private void HandleLaserFired()
        {
            //TODO: add laser firing
            Debug.Log("Fire Laser");
        }

        private void Repaint()
        {
            _shipView.Repaint(_shipModel);
        }
    }
}
