using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlrShoot : MonoBehaviour
{
    [SerializeField] Transform bulletSpawnPointright, bulletSpawnPointLeft;
    [SerializeField] GameObject[] bullets;
    [SerializeField] Camera main;
    public bool canShoot = true;
    public int currentBullet = 0;
    public Vector3 direction;

    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canShoot)
            {
                GetTargetPoint();
            }
        }
    }

    private void GetTargetPoint()
    {
        Ray ray = main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 targetpoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetpoint = hit.point;
        }
        else
        {
            targetpoint = ray.GetPoint(75);
        }

        direction = targetpoint - bulletSpawnPointLeft.position;

        Shoot();
    }

    public void Shoot()
    {    
        var leftBulletInstance = Instantiate(bullets[currentBullet], bulletSpawnPointLeft.position, Quaternion.identity);
        var rightBulletInstance = Instantiate(bullets[currentBullet], bulletSpawnPointright.position, Quaternion.identity);

        Destroy(leftBulletInstance, 5f);
        Destroy(rightBulletInstance, 5f);
    }
}
