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
    }
}
}