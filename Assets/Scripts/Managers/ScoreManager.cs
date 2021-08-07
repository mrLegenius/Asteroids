using System;
using Asteroids;
using UnityEngine;
using Zenject;

public class ScoreManager : IInitializable, IDisposable
{
    public int CurrentScore { get; private set; }
    public int HighScore { get; private set; }
    
    private const string HIGH_SCORE_SAVE_KEY = "HighScore";
    
    private readonly SignalBus _signalBus;

    public ScoreManager(SignalBus signalBus)
    {
        _signalBus = signalBus;
        
        Load();
    }
    
    public void Initialize()
    {
        _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        _signalBus.Subscribe<AsteroidDestroyedSignal>(OnAsteroidDestroyed);
        _signalBus.Subscribe<UFODestroyedSignal>(OnUFODestroyed);
        _signalBus.Subscribe<ShipDestroyedSignal>(OnPlayerDestroyed);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
        _signalBus.Unsubscribe<AsteroidDestroyedSignal>(OnAsteroidDestroyed);
        _signalBus.Unsubscribe<UFODestroyedSignal>(OnUFODestroyed);
        _signalBus.Unsubscribe<ShipDestroyedSignal>(OnPlayerDestroyed);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(HIGH_SCORE_SAVE_KEY, HighScore);
    }

    private void Load()
    {
        HighScore = PlayerPrefs.GetInt(HIGH_SCORE_SAVE_KEY);
    }

    private void OnGameStarted(GameStartedSignal signal)
    {
        CurrentScore = 0;
    }
    private void OnAsteroidDestroyed(AsteroidDestroyedSignal signal)
    {
        CurrentScore += signal.Score;
    }

    private void OnUFODestroyed(UFODestroyedSignal signal)
    {
        CurrentScore += signal.Score;
    }
    
    private void OnPlayerDestroyed(ShipDestroyedSignal signal)
    {
        if (CurrentScore > HighScore) HighScore = CurrentScore;
        
        Save();
    }
}
