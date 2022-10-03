using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcedMovement : MonoBehaviour
{
    Rigidbody mover;
    [SerializeField] float Speed = 10;
    GameManager gameManager;

    private void Awake()
    {
        mover = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if(gameManager.forcingMovement == true)
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }
        
    }
}
