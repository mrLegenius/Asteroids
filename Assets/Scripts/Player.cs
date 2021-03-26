using System;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Shooting shooting;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private Thruster thruster;
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


        movement.RotationDir =
            Input.GetKey(KeyCode.A) ? PlayerMovement.RotationDirection.Left :
            Input.GetKey(KeyCode.D) ? PlayerMovement.RotationDirection.Right :
            PlayerMovement.RotationDirection.None;

        if (Input.GetKey(KeyCode.Space) && !movement.IsAccelerating)
            shooting.Shoot(_transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.OnPlayerDestroyed();
        gameObject.SetActive(false);
    }
}
