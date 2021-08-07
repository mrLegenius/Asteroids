using System;
using Asteroids.Views;
using UnityEngine.SceneManagement;
using Zenject;

namespace Asteroids.Controllers
{
public class GameOverController : IInitializable, IDisposable
{
    private readonly GameOverView _gameOverView;
    private readonly SignalBus _signalBus;
    private readonly ScoreManager _scoreManager;
    
    public GameOverController(GameOverView gameOverView,
        SignalBus signalBus, ScoreManager scoreManager)
    {
        _gameOverView = gameOverView;
        _signalBus = signalBus;
        _scoreManager = scoreManager;
    }
    
    public void Initialize()
    {
        _signalBus.Subscribe<ShipDestroyedSignal>(OnShipDestroyed);
        
        _gameOverView.OnRestartButtonClicked(RestartGame);
        RepaintView();
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<ShipDestroyedSignal>(OnShipDestroyed);
    }

    private void RepaintView()
    {
        _gameOverView.Repaint(_scoreManager.CurrentScore,
            _scoreManager.HighScore);
    }
    
    private void RestartGame()
    {
        _gameOverView.gameObject.SetActive(false);
        _signalBus.Fire<GameStartedSignal>();
    }

    private void OnShipDestroyed(ShipDestroyedSignal signal)
    {
        RepaintView();
        
        _gameOverView.gameObject.SetActive(true);
    }
}
}