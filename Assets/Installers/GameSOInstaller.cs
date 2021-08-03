using Asteroids.Asteroid;
using UnityEngine;
using Zenject;

namespace Asteroids
{
[CreateAssetMenu]
public class GameSOInstaller : ScriptableObjectInstaller
{
    [SerializeField]
    private AsteroidsSet _asteroidsSet;

    public override void InstallBindings()
    {
        Container.Bind<AsteroidsSet>()
            .FromInstance(_asteroidsSet)
            .AsSingle();
    }
}
}