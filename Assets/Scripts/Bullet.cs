using UnityEngine;

/// <summary>
/// Обработка коллизий, отключение объекта при достижении максимального времени жизни
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime;

    private float _lifeTimeTimer;
    private Transform _transform;
    [SerializeField] private Movement movement;
    private void Awake()
    {
        _transform = transform;
    }
    
    /// <summary>
    /// Обновление времени жизни и направления движения в сторону поворота
    /// </summary>
    public void Init()
    {
        _lifeTimeTimer = lifeTime;
        movement.Direction = _transform.right;
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
