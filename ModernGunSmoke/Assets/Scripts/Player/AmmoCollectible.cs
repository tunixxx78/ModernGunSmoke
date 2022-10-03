using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollectible : MonoBehaviour
{
    public int AmmoAmount = 10;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "PlrBullet")
        {
            Destroy(this.gameObject);
        }
    }
}


