using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Asteroid")]
public class AsteroidsSet : ScriptableObject
{ 
    [SerializeField] private Asteroid[] bigAsteroids; 
    [SerializeField] private Asteroid[] mediumAsteroids;
    [SerializeField] private Asteroid[] smallAsteroids;

    public Asteroid GetAsteroid(Asteroid.Type type)
    {
        return type switch
        {
            Asteroid.Type.Big => bigAsteroids[Random.Range(0, bigAsteroids.Length)],
            Asteroid.Type.Medium => mediumAsteroids[Random.Range(0, mediumAsteroids.Length)],
            Asteroid.Type.Small => smallAsteroids[Random.Range(0, smallAsteroids.Length)],
            _ => null
        };
    }
}
