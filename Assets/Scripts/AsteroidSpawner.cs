using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private AsteroidsSet set;
    [SerializeField] private int startAsteroidsCount;
    
    [SerializeField] private Vector2 spawnOffset;

    private Camera _mainCamera;
    private int _level = 0;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void StartSpawning()
    {
        Spawn();
        _level++;
    }

    private void Spawn()
    {
        for(int i = 0; i < startAsteroidsCount + _level; i++)
        {
            var x = Screen.width + spawnOffset.x;
            var y = Random.Range(-spawnOffset.y, Screen.height + spawnOffset.y);
            var position = _mainCamera.ScreenToWorldPoint(new Vector2(x, y));

            var asteroidPrefab = set.GetAsteroid(Asteroid.Type.Big);
            var asteroid = GameManager.Instance.CreateAsteroid(asteroidPrefab, position, Quaternion.identity);

            asteroid.Set = set;
            asteroid.StartMoving();
        }
    }
}
