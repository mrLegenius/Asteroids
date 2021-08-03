using System;
using Asteroids.Views;
using Zenject;

namespace Asteroids.Controllers
{
public class MenuController : IInitializable, IDisposable
{
    private readonly MenuView _menuView;

    private SignalBus _signalBus;
    public MenuController(MenuView menuView, SignalBus signalBus)
    {
        _menuView = menuView;
        _signalBus = signalBus;
        
        Init();
    }

    public void Initialize() { }

    public void Dispose() { }

    public void Init()
    {
        _menuView.OnStartGameButtonClicked(StartGame);
    }

    public void StartGame()
    {
        _signalBus.Fire<GameStartedSignal>();
        _menuView.Hide();
    }

    public void RepaintView()
    {
        //TODO: Get Values from ScoreManager
        _menuView.Repaint(0, 0);
    }
}
}