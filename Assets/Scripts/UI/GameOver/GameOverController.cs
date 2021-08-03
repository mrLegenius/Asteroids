using System;
using Asteroids.Views;
using Zenject;

namespace Asteroids.Controllers
{
public class GameOverController : IInitializable, IDisposable
{
    private readonly GameOverView _gameOverView;

    public GameOverController(GameOverView gameOverView)
    {
        _gameOverView = gameOverView;
        
        Init();
    }
    
    public void Initialize()
    {
        
    }

    public void Dispose()
    {
        
    }

    public void Init()
    {
        _gameOverView.OnRestartButtonClicked(RestartGame);
        
        RepaintView();
    }
    
    public void RepaintView()
    {
        //TODO: Get Values from ScoreManager
        _gameOverView.Repaint(0,0 );
    }
    
    private void RestartGame()
    {
        
    }
}
}