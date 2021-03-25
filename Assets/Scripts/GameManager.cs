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
    
    [SerializeField] private Player player;
    
    public Player Player => player;
    private int playerShipsCount = 3;
    private int asteroidsCount = 0;
    private int _score = 0;
    private int _highScore = 0;
    
    private void Start()
    {
        _highScore = PlayerPrefs.GetInt("HighScore");
        ui.SetHighScore(_highScore);
    }

    public void StartGame()
    {
        player = Instantiate(player, Vector3.zero, Quaternion.identity);
        ui.SetHealth(playerShipsCount);
        asteroidSpawner.StartSpawning();
        ufoSpawner.StartSpawning();
    }
    
    private void RespawnPlayer()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
    }
    
    public void OnAsteroidDestroyed(int score = 0)
    {
        asteroidsCount--;
        _score += score;
        ui.SetScore(_score);
        if(asteroidsCount <= 0)
            asteroidSpawner.StartSpawning();
    }

    public Asteroid CreateAsteroid(Asteroid prefab, Vector3 position, Quaternion rotation)
    {
        if(!prefab)
            return null;
        
        position.z = 0;
            
        var asteroid = Instantiate(prefab, position, rotation);

        asteroidsCount++;
        return asteroid;
    }

    public void OnPlayerDestroyed()
    {
        playerShipsCount--;
        ui.SetHealth(playerShipsCount);
        
        if(playerShipsCount > 0)
            Invoke(nameof(RespawnPlayer), playerSpawnDelay);
        else
            SaveScore();
    }

    public void OnUFODestroyed(int score = 0)
    {
        _score += score;
        ui.SetScore(_score);
    }

    void SaveScore()
    {
        if (_score > _highScore)
        {
            _highScore = _score;
            ui.SetHighScore(_highScore);
            PlayerPrefs.SetInt("HighScore", _score);
        }
            
    }
    
    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;
    }
    
    
}
