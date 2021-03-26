using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Thruster : MonoBehaviour
{
    [SerializeField] private float thrustFrequency;
    private SpriteRenderer _sprite;

    private float _timer = 0;
    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Переключение эффекта трастера
    /// </summary>
    /// <param name="isThrusting">Включить ли трастер</param>
    public void Thrust(bool isThrusting)
    {
        if (!isThrusting)
        {
            _timer = thrustFrequency;
            _sprite.enabled = false;
            return;
        }

        _timer -= Time.deltaTime;
        if(_timer > 0) return;

        _sprite.enabled = !_sprite.enabled;
        _timer = thrustFrequency;
    }

}
