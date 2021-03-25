using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    
    private Vector2 _direction = Vector2.zero;

    private Rigidbody2D _rigidbody2D;

    public void SetSpeed(float value) => moveSpeed = value;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public Vector2 Direction
    {
        get => _direction;
        set => _direction = value.normalized;
    }

    public void SetRandomDirection()
    {
        var x = Random.Range(-1f, 1f);
        var y = Random.Range(-1f, 1f);

        Direction = new Vector2(x, y);
    }
    
    private void FixedUpdate()
    {
        _rigidbody2D.velocity = Direction * moveSpeed;
    }
}
