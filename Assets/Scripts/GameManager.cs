using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Hyperspace hyperspace;
    [SerializeField] private UIManager ui;
    [SerializeField] private AsteroidSpawner asteroidSpawner;
    [SerializeField] private UFOSpawner ufoSpawner;
    [SerializeField] private float playerSpawnDelay = 2;
    [SerializeField] private int startPlayerShipsCount;
    [SerializeField] private Player playerPrefab;
    [SerializeField] private float gameOverScreenDuration = 2;
    
    public Player Player { get; private set; }

    private int _playerShipsCount = 3;
    private int _asteroidsCount = 0;
    private int _score = 0;
    private int _highScore = 0;
    
    private void Start()
    {
        _highScore = PlayerPrefs.GetInt("HighScore");
        ui.SetHighScore(_highScore);
        OpenMenu();
    }

    public void StartGame()
    {
        _playerShipsCount = startPlayerShipsCount;
        _asteroidsCount = 0;
        _score = 0;
        ui.SetScore(_score);
        
        PoolManager.Instance.DisableAllObjects();
        if(!Player)
            Player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        else
        {
            var playerTransform = Player.transform;
            playerTransform.position = Vector3.zero;
            playerTransform.rotation = Quaternion.identity;
            Player.gameObject.SetActive(true);
        }
        ui.SetHealth(_playerShipsCount);
        asteroidSpawner.StartSpawning();
        ufoSpawner.StartSpawning();
        ui.OpenGameScreen();
    }
    
    private void RespawnPlayer()
    {
        Player.transform.position = Vector3.zero;
        Player.gameObject.SetActive(true);
    }
    
    public void OnAsteroidDestroyed(int score = 0)
    {
        _asteroidsCount--;
        _score += score;
        ui.SetScore(_score);
        if(_asteroidsCount <= 0)
            asteroidSpawner.StartSpawning();
    }

    public Asteroid CreateAsteroid(Asteroid prefab, Vector3 position, Quaternion rotation)
    {
        if(!prefab)
            return null;
        
        position.z = 0;

        var asteroid = PoolManager.Instance.GetObject(prefab.gameObject);
        var asteroidTransform = asteroid.transform;
        asteroidTransform.position = position;
        asteroidTransform.rotation = rotation;
        asteroid.SetActive(true);
        _asteroidsCount++;
        return asteroid.GetComponent<Asteroid>();
    }

    public void OnPlayerDestroyed()
    {
        _playerShipsCount--;
        ui.SetHealth(_playerShipsCount);
        
        if(_playerShipsCount > 0)
            Invoke(nameof(RespawnPlayer), playerSpawnDelay);
        else
        {
            ui.OpenGameOverScreen();
            SaveScore();
            Invoke(nameof(OpenMenu), gameOverScreenDuration);
        }
    }

    public void OpenMenu()
    {
        ui.OpenMenuScreen();
        PoolManager.Instance.DisableAllObjects();
        asteroidSpawner.StartSpawning();
    }

    public void OnUFODestroyed(int score = 0)
    {
        _score += score;
        ui.SetScore(_score);
    }

    private void SaveScore()
    {
        if (_score <= _highScore) return;
        
        _highScore = _score;
        ui.SetHighScore(_highScore);
        PlayerPrefs.SetInt("HighScore", _score);
    }
    
    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;
    }
}
