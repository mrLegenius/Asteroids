using UnityEngine;

public class BoundsController : MonoBehaviour
{
    [SerializeField] private Vector2 offset = new Vector2(0.1f, 0.1f);
    [SerializeField] private Vector2 beyondScreenSpace = new Vector2(50, 50);
    
    private Camera _mainCamera;
    private Transform _transform;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        _transform = transform;
    }

    private void Update()
    {
        var position = _transform.position;
        var screenPos = _mainCamera.WorldToScreenPoint(position);
        
        if (screenPos.x < -beyondScreenSpace.x)
           position.x = -position.x - offset.x;
        
        if(screenPos.x > _mainCamera.pixelWidth + beyondScreenSpace.x)
            position.x = -position.x + offset.x;

        if (screenPos.y < -beyondScreenSpace.y)
            position.y = -position.y - offset.y;
        
        if(screenPos.y > _mainCamera.pixelHeight + beyondScreenSpace.y)
            position.y = -position.y + offset.x;
        
        _transform.position = position;
    }
}
