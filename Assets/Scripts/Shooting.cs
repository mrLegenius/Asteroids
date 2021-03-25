using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float fireRate;
    [SerializeField] private float spreading;

    private Transform _transform;
    private float _fireDelay;
    private float _fireTimer;

    public bool CanShoot => _fireTimer <= 0;
    private void Awake()
    {
        _transform = transform;
        _fireDelay = 1f / fireRate;
    }

    private void Update()
    {
        _fireTimer -= Time.deltaTime;
    }

    public void Shoot(Quaternion shootDirection)
    {
        if(_fireTimer > 0 )
            return;

        var spread = Random.Range(-spreading, spreading) / 2;

        var eulerAngles = shootDirection.eulerAngles;
        eulerAngles.z += spread;
        
        var bullet = Instantiate(bulletPrefab, _transform.position, Quaternion.Euler(eulerAngles));
        bullet.Init();
        
        _fireTimer = _fireDelay;
    }
}
