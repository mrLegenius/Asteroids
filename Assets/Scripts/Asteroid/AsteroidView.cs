using System;
using Asteroids.Asteroid;
using UnityEngine;

public class AsteroidView : MonoBehaviour
{
    private Transform _transform;

    private Action<Collider2D, AsteroidView> collide;

    private void Awake()
    {
        _transform = transform;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        collide?.Invoke(other, this);
    }

    public void OnCollided(Action<Collider2D, AsteroidView> callback)
    {
        collide = callback;
    }
    
    public void Repaint(AsteroidModel model)
    {
        _transform.position = model.Position;

        var rotation = _transform.rotation;
        rotation.eulerAngles = new Vector3(0, 0, model.Angle);
        _transform.rotation = rotation;
        
        // //Активирует частицы взрыва
        // var particlesGO = PoolManager.Instance.GetObject(explosionParticles.gameObject);
        // particlesGO.SetActive(true);
        // var particles = particlesGO.GetComponent<ParticleSystem>();
        // particles.transform.position = _transform.position;
        // particles.Play();
        //
        // //Активирует звук взрыва
        // AudioManager.Instance.PlayOneShot(explosionClip);
    }

   
}
