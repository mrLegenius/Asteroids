using System;
using Asteroids.Models;
using Asteroids.Views;
using Zenject;

namespace Asteroids.Controllers
{
public class GameplayUIController : IInitializable, IDisposable, ITickable
{
    private readonly GameplayView _gameplayView;

    private readonly ShipModel _shipModel;
    private readonly SignalBus _signalBus;
    private readonly ScoreManager _scoreManager;

    public GameplayUIController(GameplayView gameplayView,
        SignalBus signalBus,
        ShipModel shipModel,
        ScoreManager scoreManager)
    {
        _gameplayView = gameplayView;
        _signalBus = signalBus;
        _shipModel = shipModel;
        _scoreManager = scoreManager;
    }

    public void Repaint()
    {
        _gameplayView.Repaint(_shipModel);
        _gameplayView.RepaintScores(_scoreManager.CurrentScore,
            _scoreManager.HighScore);
    }

    public void Initialize()
    {
        _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
    }
    public void Tick() { Repaint(); }
    
    private void OnGameStarted(GameStartedSignal signal)
    {
        Repaint();
        _gameplayView.Show();
    }
}
}