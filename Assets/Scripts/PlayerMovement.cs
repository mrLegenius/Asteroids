using UnityEngine;

/// <summary>
/// Движение с ускорением
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public enum RotationDirection
    {
        Left = 1, Right = -1, None = 0
    }
    public bool IsAccelerating { get; set; }
    public RotationDirection RotationDir { get; set; }
    
    [SerializeField] private float accelerationRate;
    [SerializeField] private float rotationRate;
    
    private Transform _transform;
    private Rigidbody2D _rigidbody2D;
    
    private void Awake()
    {
        _transform = transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (IsAccelerating)
            _rigidbody2D.AddForce(accelerationRate * (Vector2) _transform.right, ForceMode2D.Force);

        _rigidbody2D.rotation += (int)RotationDir * rotationRate * Time.fixedDeltaTime;
    }
}
