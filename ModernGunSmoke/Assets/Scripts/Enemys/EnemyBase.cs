using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public GameObject plr;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] GameObject enemyBulletPrefab;
    [SerializeField] float delay = 2f;

    GameManager gameManager;

    private void Awake()
    {
        plr = GameObject.Find("Player");
        gameManager = FindObjectOfType<GameManager>();
        
    }

    private void Start()
    {
        GameObject enemyBulletInstance =  Instantiate(enemyBulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Destroy(enemyBulletInstance, 10f);

        StartCoroutine(ShootingDelay());
        
    }

    private void Update()
    {
        
    }

    private void ShootingEnemy()
    {
        GameObject enemyBulletInstance = Instantiate(enemyBulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Destroy(enemyBulletInstance, 3f);

        StartCoroutine(ShootingDelay());


    }

    IEnumerator ShootingDelay()
    {
        yield return new WaitForSeconds(delay);

        ShootingEnemy();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "PlrBullet")
        {
            gameManager.plrPoints++;
            Destroy(this.gameObject);
        }
    }
}
