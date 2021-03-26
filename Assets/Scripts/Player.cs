using UnityEngine;

/// <summary>
/// Обработка пользовательского ввода и управление функциями корабля игрока
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] private Shooting shooting;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private Thruster thruster;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioSource thrusterSound;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
           GameManager.Instance.hyperspace.Jump(_transform);
        
        movement.IsAccelerating = Input.GetKey(KeyCode.W);

        thruster.Thrust(movement.IsAccelerating);
        if(movement.IsAccelerating && !thrusterSound.isPlaying)
            thrusterSound.Play();
        else
        {
            thrusterSound.Stop();
        }

        movement.RotationDir =
            Input.GetKey(KeyCode.A) ? PlayerMovement.RotationDirection.Left :
            Input.GetKey(KeyCode.D) ? PlayerMovement.RotationDirection.Right :
            PlayerMovement.RotationDirection.None;

        if (Input.GetKey(KeyCode.Space) && !movement.IsAccelerating)
            if (shooting.AttemptToShoot(_transform.rotation))
            {
                AudioManager.Instance.PlayOneShot(shotSound);
            }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.OnPlayerDestroyed();
        
        //Активация частиц взрыва
        var particlesGO = PoolManager.Instance.GetObject(explosionParticles.gameObject);
        particlesGO.SetActive(true);
        var particles = particlesGO.GetComponent<ParticleSystem>();
        particles.transform.position = _transform.position;
        particles.Play();
        
        //Активация звука взрыва
        AudioManager.Instance.PlayOneShot(explosionClip);
        
        gameObject.SetActive(false);
    }
}
