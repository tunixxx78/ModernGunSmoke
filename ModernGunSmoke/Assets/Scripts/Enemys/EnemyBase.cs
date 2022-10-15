using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBase : MonoBehaviour
{
    public GameObject plr;
    public int enemyHealth;
    [SerializeField] HealthBar enemyHealthBar;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] GameObject enemyBulletPrefab;
    [SerializeField] float delay = 2f, radius;
    [Range(0, 360)]
    [SerializeField] float angle;
    [SerializeField] LayerMask player;
    [SerializeField] LayerMask obstacle;
    [SerializeField]bool canShoot = true, isThisBoss, isAlive;
    public bool canSeePlr;

    [SerializeField] GameObject flashEffect;


    GameManager gameManager;

    public Animator enemyAnimator;

    SFXHandler sFXHandler;

    private void Awake()
    {
        plr = GameObject.Find("Player");
        gameManager = FindObjectOfType<GameManager>();
        if (isThisBoss)
        {
            enemyHealthBar.SetMaxHealth(enemyHealth);
        }

        

        isAlive = true;

        sFXHandler = FindObjectOfType<SFXHandler>();
        
    }

    private void Start()
    {
        enemyAnimator = GetComponentInChildren<Animator>();
        //GameObject enemyBulletInstance =  Instantiate(enemyBulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        //Destroy(enemyBulletInstance, 10f);

        StartCoroutine(FOVRoutine());
        //StartCoroutine(ShootingDelay());
        
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
        canShoot = true;

        //ShootingEnemy();
        if (isAlive)
        {
            FieldOfViewCheck();
        }
        
    }

    IEnumerator FOVRoutine()
    {
        float delay = 0.2f;

        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();

        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, player);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacle))
                {
                    if (canShoot && isAlive)
                    {
                        canSeePlr = true;
                        sFXHandler.enemyShot.Play();
                        GameObject enemyBulletInstance = Instantiate(enemyBulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
                        var flashInstance = Instantiate(flashEffect, bulletSpawnPoint.position, Quaternion.identity);
                        Destroy(flashInstance, 1f);
                        Destroy(enemyBulletInstance, 3f);

                        canShoot = false;

                        StartCoroutine(ShootingDelay());
                    }
                    
                }
                else
                {
                    canSeePlr = false;
                }
            }
            else
            {
                canSeePlr = false;
            }
        }

        else if (canSeePlr)
        {
            canSeePlr = false;
        }
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "PlrBullet")
        {
            gameManager.plrPoints++;
            enemyHealth--;
            if (isThisBoss)
            {
                enemyHealthBar.SetHealth(enemyHealth);
            }

            if(enemyHealth > 0)
            {
                sFXHandler.getHit.Play();
            }
            
            if (enemyHealth <= 0)
            {
                isAlive = false;

                GetComponent<Rigidbody>().isKinematic = true;

                enemyAnimator.SetTrigger("Death");
                sFXHandler.dying.Play();
                

                Destroy(this.gameObject, 3f);

                if (isThisBoss)
                {
                    gameManager.bossIsDead = true;
                    gameManager.ShowYouWonPanel();

                }
            }
            
        }
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlrBullet") || other.CompareTag("KillZone"))
        {
            gameManager.plrPoints++;
            enemyHealth--;
            if (isThisBoss)
            {
                enemyHealthBar.SetHealth(enemyHealth);
            }

            if (enemyHealth > 0)
            {
                sFXHandler.getHit.Play();
            }

            if (enemyHealth <= 0)
            {
                isAlive = false;

                GetComponent<Rigidbody>().isKinematic = true;

                enemyAnimator.SetTrigger("Death");
                sFXHandler.dying.Play();


                Destroy(this.gameObject, 3f);

                if (isThisBoss)
                {
                    Scene scene = SceneManager.GetActiveScene();

                    gameManager.ShowYouWonPanel();
                    gameManager.plrPoints += gameManager.pointsFromBoss[scene.buildIndex - 2];

                }
            }
        }
        
    }


    public void getAnimatorActions()
    {
        enemyAnimator.SetTrigger("Jump");
    }
}
