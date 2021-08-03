using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Asteroid;
using UnityEngine;

public class AsteroidView : MonoBehaviour
{
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void Repaint(AsteroidModel model)
    {
        _transform.position = model.Position;

        var rotation = _transform.rotation;
        rotation.eulerAngles = new Vector3(0, 0, model.Angle);
        _transform.rotation = rotation;
    }
}
