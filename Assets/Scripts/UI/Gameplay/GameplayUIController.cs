using Asteroids.Models;
using Asteroids.Views;

namespace Asteroids.Controllers
{
public class GameplayUIController
{
    private readonly GameplayView _gameplayView;

    private ShipModel _shipModel;

    public GameplayUIController(GameplayView gameplayView)
    {
        _gameplayView = gameplayView;
    }

    public void Repaint() { _gameplayView.Repaint(_shipModel); }
}
}