using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    public enum Type
    {
        Big,
        Medium,
        Small
    }

    public AsteroidsSet Set { get; set; }
    [SerializeField] private float minSpeed, maxSpeed;
    [SerializeField] private float asteroidSpawnSpread;
    [SerializeField] private int score;
    [SerializeField] private Movement movement;
    [SerializeField] private Type asteroidType = Type.Big;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void StartMoving()
    {
        movement.SetRandomDirection();
        movement.SetSpeed(Random.Range(minSpeed, maxSpeed));
    }
    private Asteroid GetSpawnableAsteroidPrefab()
    {
        return asteroidType switch
        {
            Type.Big => Set.GetAsteroid(Type.Medium),
            Type.Medium => Set.GetAsteroid(Type.Small),
            _ => null
        };
    }

    private void SpawnAsteroids()
    {
        for (int i = 0; i < 2; i++)
        {
            var asteroidPrefab = GetSpawnableAsteroidPrefab();

            if(!asteroidPrefab)
                return;

            var spread = Random.Range(-asteroidSpawnSpread, asteroidSpawnSpread) / 2;
            var dir = movement.Direction;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(0, 0, angle + spread);
            var asteroid = GameManager.Instance.CreateAsteroid(asteroidPrefab, _transform.position, rotation);
            if(!asteroid)
                continue;
            
            asteroid.Set = Set;
            asteroid.StartMoving();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        SpawnAsteroids();
        gameObject.SetActive(false);
        GameManager.Instance.OnAsteroidDestroyed(score);
    }
}
