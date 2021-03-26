using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
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

    /// <summary>
    /// Стреляет когда прошел кулдаун выстрела.
    /// </summary>
    /// <param name="shootDirection">Направление выстрела</param>
    /// <returns>Произошел ли выстрел</returns>
    public bool AttemptToShoot(Quaternion shootDirection)
    {
        if(_fireTimer > 0 )
            return false;

        var spread = Random.Range(-spreading, spreading) / 2;

        var eulerAngles = shootDirection.eulerAngles;
        eulerAngles.z += spread;

        var bullet = PoolManager.Instance.GetObject(bulletPrefab);
        var bulletTransform = bullet.transform;
        bulletTransform.position = _transform.position;
        bulletTransform.rotation = Quaternion.Euler(eulerAngles);
        bullet.GetComponent<Bullet>().Init();
        bullet.SetActive(true);
        
        _fireTimer = _fireDelay;

        return true;
    }
}
