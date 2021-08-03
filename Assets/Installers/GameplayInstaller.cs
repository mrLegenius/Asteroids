using Asteroids.Asteroid;
using Asteroids.Controllers;
using Asteroids.Ship;
using Asteroids.Ship.Bullet;
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