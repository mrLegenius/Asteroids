
using System;
using UnityEngine;
using Zenject;

namespace Asteroids.Asteroid
{
public class AsteroidSpawnController : IInitializable, IDisposable
{
    private float _spawnTimer;
    private int _level;
    private const int INITIAL_SPAWN_COUNT = 5;

    private readonly AsteroidsController _asteroidsController;
    private readonly SignalBus _signalBus;
    public AsteroidSpawnController(
        AsteroidsController asteroidsController,
        SignalBus signalBus)
    {
        _asteroidsController = asteroidsController;
        _signalBus = signalBus;
    }

    public void SpawnAsteroids(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _asteroidsController.CreateAsteroid(AsteroidType.Big, 
                Utilities.GetRandomPositionOutOfScreen(Vector2.zero),
                Utilities.GetRandomAngle());
        }
    }

    public void Initialize()
    { 
        _signalBus.Subscribe<DestroyedAllAsteroidsSignal>(OnAllAsteroidsWereDestroyed);   
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<DestroyedAllAsteroidsSignal>(OnAllAsteroidsWereDestroyed);   
    }

    public void OnAllAsteroidsWereDestroyed(DestroyedAllAsteroidsSignal signal)
    {
        SpawnAsteroids(INITIAL_SPAWN_COUNT + _level++);
    }
}
}