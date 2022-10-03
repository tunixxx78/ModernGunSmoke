using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] GameObject plr;
    Rigidbody bulletRb;
    public Vector3 plrPos, direction;
    [SerializeField] float bulletSpeed;

    private void Awake()
    {
        plr = GameObject.Find("Player");
        bulletRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        plrPos = plr.transform.position;
        direction = plrPos - this.transform.position;
    }

    private void Update()
    {
        bulletRb.AddForce(direction * bulletSpeed * Time.deltaTime, ForceMode.Impulse);
    }
}
