using System;
using UnityEngine;
using Zenject;

namespace Asteroids.UFO
{
public class UFOSpawnController : IInitializable, IDisposable, ITickable
{
    private float _spawnTimer;
    
    private readonly UFOController _ufoController;
    private readonly SignalBus _signalBus;
    private readonly UFOSpawnSettings _spawnSettings;
    
    private bool _isGameStarted;
    
    public UFOSpawnController(
        UFOController ufoController,
        SignalBus signalBus,
        UFOSpawnSettings settings)
    {
        _ufoController = ufoController;
        _signalBus = signalBus;
        _spawnSettings = settings;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        _signalBus.Subscribe<ShipDestroyedSignal>(OnShipDestroyed);
    }
    public void Dispose()
    {
        _signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
        _signalBus.Unsubscribe<ShipDestroyedSignal>(OnShipDestroyed);
    }
    public void Tick()
    {
        if(!_isGameStarted) return;
        
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer < _spawnSettings.SpawnInterval) return;
        
        SpawnUFO();
        _spawnTimer = 0;
    }
    
    private void SpawnUFO()
    {
        _ufoController.CreateUFO(
            Utilities.GetRandomPositionOutOfScreen(Vector2.zero),
            Utilities.GetRandomAngle());
    }

    private void OnGameStarted(GameStartedSignal signal)
    {
        _isGameStarted = true;
    }
    private void OnShipDestroyed(ShipDestroyedSignal signal)
    {
        _isGameStarted = false;
    }
}
}