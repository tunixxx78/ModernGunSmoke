using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] Rigidbody plrRB;
    [SerializeField] float moveSpeed = 10f;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        plrRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        plrRB.AddForce(Vector3.forward * moveSpeed * Time.deltaTime, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("NextBlockCollider"))
        {
            Debug.Log("OSUMAA TULEE");
            gameManager.SpawnNextBlock();
        }
    }
}
