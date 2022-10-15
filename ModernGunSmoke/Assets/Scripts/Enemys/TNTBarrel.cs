using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTBarrel : MonoBehaviour
{
    SFXHandler sFXHandler;
    [SerializeField] GameObject explotionEffect;

    private void Awake()
    {
        sFXHandler = FindObjectOfType<SFXHandler>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillZone"))
        {
            sFXHandler.explosion.Play();
            GetComponent<Rigidbody>().Sleep();
            explotionEffect.SetActive(true);
            GetComponentInParent<MeshRenderer>().enabled = false;

            GetComponentInChildren<SphereCollider>().radius = 4;

            Destroy(this.gameObject, 1.5f);
        }

        if (other.CompareTag("PlrBullet"))
        {
            sFXHandler.explosion.Play();
            GetComponent<Rigidbody>().Sleep();
            explotionEffect.SetActive(true);
            GetComponentInParent<MeshRenderer>().enabled = false;

            GetComponentInChildren<SphereCollider>().radius = 4;

            Destroy(this.gameObject, 1.5f);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "PlrBullet")
        {
            sFXHandler.explosion.Play();
            GetComponent<Rigidbody>().Sleep();
            explotionEffect.SetActive(true);
            GetComponentInParent<MeshRenderer>().enabled = false;

            GetComponentInChildren<SphereCollider>().radius = 4;

            Destroy(this.gameObject, 1.5f);
        }

    }
}
