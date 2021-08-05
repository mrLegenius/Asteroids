using Asteroids.Asteroid;
using Asteroids.Controllers;
using Asteroids.Models;
using Asteroids.Ship;
using Asteroids.Ship.Bullet;
using UnityEngine;
using Zenject;

namespace Asteroids
{
public class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        BindControllers();
        BindSignals();
        
        Container.Bind<ShipModel>().FromMethod(CreateShipModel).AsSingle().NonLazy();
    }

    private static ShipModel CreateShipModel(InjectContext ctx)
    {
        return new ShipModel
        {
            Movement = new Models.Movement
            {
                MaxSpeed = 10,
                Acceleration = 20,
                Deceleration = 10,
                RotationSpeed = 180
            },
            Shooting = new Models.Shooting
            {
                BulletPrefab = Resources.Load<BulletView>("Bullet"),
                FireRate = 10
            },
            LaserFiring = new LaserFiring()
        };
    }

    private void BindControllers()
    {
        Container.BindInterfacesAndSelfTo<MenuController>()
            .AsSingle()
            .NonLazy();
        
        Container.BindInterfacesAndSelfTo<GameplayUIController>()
            .AsSingle()
            .NonLazy();
        
        Container.BindInterfacesAndSelfTo<GameOverController>()
            .AsSingle()
            .NonLazy();
        
        Container.BindInterfacesAndSelfTo<ShipController>()
            .AsSingle()
            .NonLazy();
        
        Container.BindInterfacesAndSelfTo<BulletsController>()
            .AsSingle()
            .NonLazy();
        
        Container.BindInterfacesAndSelfTo<AsteroidsController>()
            .AsSingle()
            .NonLazy();
        
        Container.BindInterfacesAndSelfTo<AsteroidSpawnController>()
            .AsSingle()
            .NonLazy();
    }

    private void BindSignals()
    {
        Container.DeclareSignal<GameStartedSignal>();
        Container.DeclareSignal<DestroyedAllAsteroidsSignal>();
    }
}
}