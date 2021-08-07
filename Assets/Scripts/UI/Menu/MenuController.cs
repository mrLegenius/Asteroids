using System;
using Asteroids.Views;
using Zenject;

namespace Asteroids.Controllers
{
public class MenuController : IInitializable, IDisposable
{
    private readonly MenuView _menuView;

    private readonly SignalBus _signalBus;
    private readonly ScoreManager _scoreManager;
    public MenuController(MenuView menuView, SignalBus signalBus, ScoreManager scoreManager)
    {
        _menuView = menuView;
        _signalBus = signalBus;
        _scoreManager = scoreManager;
    }

    public void Initialize()
    {
        _menuView.OnStartGameButtonClicked(StartGame);
        RepaintView();
    }

    public void Dispose() { }

    public void StartGame()
    {
        _signalBus.Fire<GameStartedSignal>();
        _menuView.Hide();
    }

    public void RepaintView()
    {
        _menuView.Repaint(_scoreManager.CurrentScore,
            _scoreManager.HighScore);
    }
}
}