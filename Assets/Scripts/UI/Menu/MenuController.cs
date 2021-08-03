using System;
using Asteroids.Views;
using Zenject;

namespace Asteroids.Controllers
{
public class MenuController : IInitializable, IDisposable
{
    private readonly MenuView _menuView;

    public MenuController(MenuView menuView)
    {
        _menuView = menuView;
        Init();
    }

    public void Initialize() { }

    public void Dispose() { }

    public void Init() { _menuView.OnStartGameButtonClicked(StartGame); }

    public void StartGame()
    {
        //Fire signal start Game
    }

    public void RepaintView()
    {
        //TODO: Get Values from ScoreManager
        _menuView.Repaint(0, 0);
    }
}
}