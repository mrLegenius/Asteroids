using System;
using Asteroids.Models;
using Asteroids.Ship.Bullet;
using Asteroids.Views;
using UnityEngine;
using Zenject;

namespace Asteroids.Ship
{
    public class ShipController : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        
        private readonly ShipView _shipView;
        private readonly ShipModel _shipModel;

        private readonly BulletsController _bulletsController;
        private readonly ShipSettings _shipSettings;
        
        public ShipController(ShipModel model, ShipView shipView, 
            BulletsController bulletsController, SignalBus signalBus,
            ShipSettings shipSettings)
        {
            _shipView = shipView;
            _bulletsController = bulletsController;
            _signalBus = signalBus;
            _shipSettings = shipSettings;
            
            _shipModel = model;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
            
            _shipView
                .OnMoved(HandleMove)
                .OnShoot(HandleShoot)
                .OnLaserFired(HandleLaserFired)
                .OnCollided(OnShipCollided);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
        }
        
        private void HandleMove(Vector2 input)
        {
            var movement = _shipModel.Movement;
        
            RotateShip(input, movement);
            CalculateVelocity(input, movement);

            _shipModel.Position += movement.Velocity * Time.deltaTime;
            _shipModel.Position =
                Utilities.GetWrapAroundPosition(_shipModel.Position);
            
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

            if (_shipModel.Angle > 180) _shipModel.Angle = -180;
            if (_shipModel.Angle < -180) _shipModel.Angle = 180;
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

            movement.Velocity = Vector2.ClampMagnitude(movement.Velocity, movement.MaxSpeed);
        }

        private void HandleShoot(bool shootCommanded)
        {
            var shooting = _shipModel.Shooting;
            shooting.FireTimer -= Time.deltaTime;

            if (!shooting.CanShoot || !shootCommanded) return;

            _bulletsController.CreateBullet(_shipModel.Position, 
                _shipModel.Angle);
            
            shooting.FireTimer = shooting.FireRate;
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

        private void OnGameStarted(GameStartedSignal signal)
        {
            _shipModel.Movement.Velocity = Vector2.zero;
            _shipModel.Position = _shipSettings.InitialPosition;
            _shipModel.Angle = 0;
            _shipModel.Shooting.FireTimer = 0;
            _shipModel.LaserFiring.LaserCount = _shipSettings.LasersCount;
            _shipModel.LaserFiring.LaserCooldown = 0;
                
            _shipView.gameObject.SetActive(true);
        }
        private void OnShipCollided(Collider2D other, ShipView view)
        {
            view.gameObject.SetActive(false);
            _signalBus.Fire<ShipDestroyedSignal>();
        }
    }
}
