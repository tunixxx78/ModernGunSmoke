using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlrShoot : MonoBehaviour
{
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] GameObject[] bullets;
    [SerializeField] Camera main;
    [SerializeField] float shootDelay = 1;
    public bool canShoot = true;
    public int currentBullet = 0, ammoCount = 5;
    public Vector3 direction;
    GameManager gameManager;
    SFXHandler sfxHandler;

    [SerializeField] GameObject flashEffect;
    [SerializeField] float shootingRange;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        sfxHandler = FindObjectOfType<SFXHandler>();
        gameManager.plrAmmoCount = ammoCount;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canShoot && ammoCount >= 1)
            {
                GetTargetPoint();
                ammoCount--;
                canShoot = false;
                gameManager.plrAmmoCount = ammoCount;
                sfxHandler.shotgun.Play();

                StartCoroutine(ShootDelay());
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

        direction = targetpoint - bulletSpawnPoint.position;

        Shoot();
    }

    public void Shoot()
    {    
        var BulletInstance = Instantiate(bullets[currentBullet], bulletSpawnPoint.position, Quaternion.identity);
        var flashInstance = Instantiate(flashEffect, bulletSpawnPoint.position, Quaternion.identity);

        Destroy(BulletInstance, shootingRange);
        Destroy(flashInstance, 1f);
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootDelay);

        canShoot = true;
    }
}
