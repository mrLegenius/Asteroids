using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;

    private float _lifeTimeTimer;
    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    private void Awake()
    {
        _transform = transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Init()
    {
        _lifeTimeTimer = lifeTime;
        _rigidbody2D.velocity = speed * _transform.right;
    }

    private void Update()
    {
        _lifeTimeTimer -= Time.deltaTime;
        
        if(_lifeTimeTimer <= 0)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.SetActive(false);
    }
}
