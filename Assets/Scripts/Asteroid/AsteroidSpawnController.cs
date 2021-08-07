
using System;
using UnityEngine;
using Zenject;

namespace Asteroids.Asteroid
{
public class AsteroidSpawnController : IInitializable, IDisposable
{
    private float _spawnTimer;
    private int _level;

    private readonly AsteroidsController _asteroidsController;
    private readonly SignalBus _signalBus;
    private readonly AsteroidSpawnSettings _spawnSettings;
    
    public AsteroidSpawnController(
        AsteroidsController asteroidsController,
        SignalBus signalBus,
        AsteroidSpawnSettings settings)
    {
        _asteroidsController = asteroidsController;
        _signalBus = signalBus;
        _spawnSettings = settings;
    }
    
    public void Initialize()
    { 
        _signalBus.Subscribe<DestroyedAllAsteroidsSignal>(OnAllAsteroidsWereDestroyed);   
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<DestroyedAllAsteroidsSignal>(OnAllAsteroidsWereDestroyed);   
    }
    
    private void SpawnAsteroids(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _asteroidsController.CreateAsteroid(AsteroidType.Big, 
                Utilities.GetRandomPositionOutOfScreen(Vector2.zero),
                Utilities.GetRandomAngle());
        }
    }

    private void OnAllAsteroidsWereDestroyed(DestroyedAllAsteroidsSignal signal)
    {
        SpawnAsteroids(_spawnSettings.InitialAsteroidCount + _level++);
    }
}
}