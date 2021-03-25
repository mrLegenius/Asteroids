using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Shooting shooting;


    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
            shooting.Shoot();
    }
}
