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

    public GameplayUIController(GameplayView gameplayView, SignalBus signalBus, ShipModel shipModel)
    {
        _gameplayView = gameplayView;
        _signalBus = signalBus;
        _shipModel = shipModel;
    }

    public void Repaint() { _gameplayView.Repaint(_shipModel); }

    public void Initialize()
    {
        _signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
    }

    public void OnGameStarted(GameStartedSignal signal)
    {
        Repaint();
        _gameplayView.Show();
    }

    public void Tick() { Repaint(); }
}
}