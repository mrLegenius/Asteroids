using System;
using UnityEngine;

namespace Asteroids.UFO
{
public class UFOView : MonoBehaviour
{
    private Transform _transform;

    private Action<Collider2D, UFOView> _collided;

    private void Awake()
    {
        _transform = transform;
    }

    public void OnCollided(Action<Collider2D, UFOView> callback)
    {
        _collided = callback;
    }
    public void Repaint(UFOModel model)
    {
        _transform.position = model.Position;

        var rotation = _transform.rotation;
        rotation.eulerAngles = new Vector3(0, 0, model.Angle);

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        _collided?.Invoke(other, this);
    }
}
}