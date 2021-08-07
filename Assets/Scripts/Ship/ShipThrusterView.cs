using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShipThrusterView : MonoBehaviour
{
    [SerializeField] private float _thrustFrequency;
    private SpriteRenderer _sprite;

    private float _timer;
    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }
    
    public void Thrust(bool isThrusting)
    {
        if (!isThrusting)
        {
            _timer = _thrustFrequency;
            _sprite.enabled = false;
            return;
        }

        _timer -= Time.deltaTime;
        if(_timer > 0) return;

        _sprite.enabled = !_sprite.enabled;
        _timer = _thrustFrequency;
    }

}
