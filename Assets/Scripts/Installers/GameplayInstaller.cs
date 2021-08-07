using Asteroids.Asteroid;
using Asteroids.Controllers;
using Asteroids.Models;
using Asteroids.Ship;
using Asteroids.Ship.Bullet;
using Asteroids.UFO;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Asteroids
{
public class GameplayInstaller : MonoInstaller
{
    [SerializeField]
    private ShipSettings _shipSettings;
    
    [SerializeField]
    private BulletSettings _bulletSettings;

    [SerializeField]
    private AsteroidsSettings _asteroidsSettings;

    [SerializeField]
    private AsteroidSpawnSettings _asteroidSpawnSettings;
    
    [SerializeField]
    private UFOSettings _ufoSettings;

    [SerializeField]
    private UFOSpawnSettings _ufoSpawnSettings;
    
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        BindControllers();
        BindSignals();
        BindSettings();
        
        Container.Bind<ShipModel>().FromMethod(CreateShipModel).AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<ScoreManager>()
            .AsSingle()
            .NonLazy();
    }

    private ShipModel CreateShipModel(InjectContext ctx)
    {
        return new ShipModel
        {
            Movement = new Movement
            {
                MaxSpeed = _shipSettings.MaxSpeed,
                Acceleration = _shipSettings.Acceleration,
                Deceleration = _shipSettings.Deceleration,
                RotationSpeed = _shipSettings.RotationSpeed
            },
            Shooting = new Shooting
            {
                FireRate = _shipSettings.FireRate
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

        Container.BindInterfacesAndSelfTo<UFOController>()
            .AsSingle()
            .NonLazy();
        
        Container.BindInterfacesAndSelfTo<UFOSpawnController>()
            .AsSingle()
            .NonLazy();
    }

    private void BindSignals()
    {
        Container.DeclareSignal<GameStartedSignal>();
        Container.DeclareSignal<DestroyedAllAsteroidsSignal>();
        Container.DeclareSignal<ShipDestroyedSignal>();
        Container.DeclareSignal<AsteroidDestroyedSignal>();
        Container.DeclareSignal<UFODestroyedSignal>();
    }
    
    private void BindSettings()
    {
        Container.BindInstance(_shipSettings);
        Container.BindInstance(_bulletSettings);
        Container.BindInstance(_asteroidsSettings);
        Container.BindInstance(_asteroidSpawnSettings);
        Container.BindInstance(_ufoSettings);
        Container.BindInstance(_ufoSpawnSettings);
    }
}
}