using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlrMovement : MonoBehaviour
{
    GameManager gameManager;
    Rigidbody plrRb;
    [SerializeField] float plrSpeed = 20f, plrBaseSpeed = 10f;
    Vector3 direction;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        plrRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //plrRb.AddForce(Vector3.forward * plrBaseSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * plrBaseSpeed * Time.deltaTime);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            movement();
        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("NextBlockCollider"))
        {
            Destroy(collider.gameObject);
            Debug.Log("OSUMAA TULEE");
            gameManager.SpawnNextBlock();
            
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Mover")
        {
            Debug.Log("Osuma moveriin!");
        }
    }

    private void movement()
    {
        plrRb.AddForce(direction * plrSpeed * Time.deltaTime, ForceMode.Impulse);
    }
}
