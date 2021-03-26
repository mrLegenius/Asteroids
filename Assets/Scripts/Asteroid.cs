using UnityEngine;

/// <summary>
/// Класс, отвечающий за все функции астероида
/// </summary>
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
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private AudioClip explosionClip;
    
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    /// <summary>
    /// Заставляет астероид двигаться в рандомной направлении с рандомной скоростью в интервале (minSpeed, maxSpeed)
    /// </summary>
    public void StartMoving()
    {
        movement.SetRandomDirection();
        movement.SetSpeed(Random.Range(minSpeed, maxSpeed));
    }
    
    /// <summary>
    /// Создает два астероида, если исходный был большим или средним.
    /// </summary>
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
        
        GameManager.Instance.OnAsteroidDestroyed(score);
        
        //Активирует частицы взрыва
        var particlesGO = PoolManager.Instance.GetObject(explosionParticles.gameObject);
        particlesGO.SetActive(true);
        var particles = particlesGO.GetComponent<ParticleSystem>();
        particles.transform.position = _transform.position;
        particles.Play();
        
        //Активирует звук взрыва
        AudioManager.Instance.PlayOneShot(explosionClip);
        
        gameObject.SetActive(false);
    }
    /// <summary>
    /// Получает префаб дочернего астероида
    /// </summary>
    /// <returns>Префаб дочернего астероида или null, если исходный астероид был маленьким</returns>
    private Asteroid GetSpawnableAsteroidPrefab()
    {
        return asteroidType switch
        {
            Type.Big => Set.GetAsteroid(Type.Medium),
            Type.Medium => Set.GetAsteroid(Type.Small),
            _ => null
        };
    }
}
