using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcedMovement : MonoBehaviour
{
    Rigidbody mover;
    [SerializeField] float Speed = 10;

    private void Awake()
    {
        mover = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
}