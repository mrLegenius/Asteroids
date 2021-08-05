using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private UIManager ui;
    [SerializeField] private UFOSpawner ufoSpawner;
    [SerializeField] private float playerSpawnDelay = 2;
    
    private int _score = 0;
    private int _highScore = 0;
    
    private void Start()
    {
        _highScore = PlayerPrefs.GetInt("HighScore");
        ui.SetHighScore(_highScore);
    }

    /// <summary>
    /// Начало новой игры: обнуление переменных, обновление интерфейса, отключение всех старых объектов
    /// </summary>
    public void StartGame()
    {
        _score = 0;
        ui.SetScore(_score);
    }
}


    /// <summary>
    /// Убавляет количество оставшихся жизней у игрока.
    /// Обновляет интерфейс
    /// При достижении нуля жизней, открывает панель конца игры
    /// </summary>
    // public void OnPlayerDestroyed()
    // {
    //     _playerShipsCount--;
    //     ui.SetHealth(_playerShipsCount);
    //     
    //     if(_playerShipsCount > 0)
    //         Invoke(nameof(RespawnPlayer), playerSpawnDelay);
    //     else
    //     {
    //         ui.OpenGameOverScreen();
    //         SaveScore();
    //         Invoke(nameof(OpenMenu), gameOverScreenDuration);
    //     }
    // }

    /// <summary>
    /// Открывает панель меню
    /// </summary>

/// <summary>
    /// Добавляет очки за уничтожение летающей тарелки
    /// </summary>
    /// <param name="score"></param>

// private void SaveScore()
//     {
//         if (_score <= _highScore) return;
//         
//         _highScore = _score;
//         ui.SetHighScore(_highScore);
//         PlayerPrefs.SetInt("HighScore", _score);
//     }
//     
//     private void Awake()
//     {
//         if (Instance)
//             Destroy(gameObject);
//         else
//             Instance = this;
//     }

