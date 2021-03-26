using System.Collections;
using UnityEngine;

public class Hyperspace : MonoBehaviour
{
    [SerializeField] private float jumpDelay = 1;

    [SerializeField] private Vector2 screenOffset = new Vector2(0.1f, 0.9f);

    private Camera _mainCamera;
    private Transform _transform;
    private bool _isJumping = false;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
        _transform = transform;
    }

    public void Jump(Transform player)
    {
        if (!_isJumping)
            StartCoroutine(JumpToHyperSpace(player));
    }

    private IEnumerator JumpToHyperSpace(Transform player)
    {
        player.gameObject.SetActive(false);
        _isJumping = true;
        
        yield return new WaitForSeconds(jumpDelay);

        player.position = GetRandomPosition();
        player.gameObject.SetActive(true);
        _isJumping = false;
    }

    private Vector3 GetRandomPosition()
    {
        var screenPosX = Random.Range(Screen.width * screenOffset.x, Screen.width * (1f - screenOffset.x));
        var screenPosY = Random.Range(Screen.height * screenOffset.y, Screen.height * (1f - screenOffset.y));

        var newPos = _mainCamera.ScreenToWorldPoint(new Vector3(screenPosX, screenPosY));
        newPos.z = 0;
        return newPos;
    }
}
