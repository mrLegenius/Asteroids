using UnityEngine;
public class UFO : MonoBehaviour
{
    // [SerializeField] private float minSpeed, maxSpeed;
    // [SerializeField] private int score;
    // [SerializeField] private ParticleSystem explosionParticles;
    // [SerializeField] private AudioClip explosionClip;
    //
    // private Transform _transform;
    //
    // private void Awake()
    // {
    //     _transform = transform;
    // }
    // /// <summary>
    // /// Заставляет НЛО двигаться в случайном направлении со случайной скоростью в интервале (minSpeed, maxSpeed)
    // /// </summary>
    // public void StartMoving()
    // {
    //     movement.SetRandomDirection();
    //     movement.SetSpeed(Random.Range(minSpeed, maxSpeed));
    // }
    //
    // private void Update()
    // {
    //     if (shooting.CanShoot)
    //     {
    //         var playerPos = GameManager.Instance.Player.transform.position;
    //         var position = _transform.position;
    //         var dir = Quaternion.Euler(0, 0, 90) * (playerPos - position);
    //         
    //         var rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: dir);
    //         
    //         shooting.AttemptToShoot(rotation);
    //     }
    // }
    //
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     GameManager.Instance.OnUFODestroyed(score);
    //     
    //     //Активирует частицы взрыва
    //     var particlesGO = PoolManager.Instance.GetObject(explosionParticles.gameObject);
    //     particlesGO.SetActive(true);
    //     var particles = particlesGO.GetComponent<ParticleSystem>();
    //     particles.transform.position = _transform.position;
    //     particles.Play();
    //     
    //     //Активирует звук взрыва
    //     AudioManager.Instance.PlayOneShot(explosionClip);
    //     
    //     gameObject.SetActive(false);
    // }
}
