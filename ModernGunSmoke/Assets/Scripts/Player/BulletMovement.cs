using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    Rigidbody bulletRb;
    [SerializeField] float bulletSpeed = 100f;
    PlrShoot plrShoot;

    private void Awake()
    {
        bulletRb = GetComponent<Rigidbody>();
        plrShoot = FindObjectOfType<PlrShoot>();
    }

    private void Update()
    {
        this.bulletRb.AddForce(plrShoot.direction.normalized * bulletSpeed * Time.deltaTime, ForceMode.Impulse);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
    
}
