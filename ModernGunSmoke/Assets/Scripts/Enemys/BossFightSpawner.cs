using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPos;
    [SerializeField] Transform[] followerSpawnPos;
    [SerializeField] GameObject enemyStaticShooter, enemyFollower;

    int staticPos;
    int followerPos;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        InstansiateStaticEnemy();
    }

    private void InstansiateStaticEnemy()
    {
        if(gameManager.bossIsDead == false)
        {
            staticPos = Random.Range(0, spawnPos.Length);
            var enemyInstance = Instantiate(enemyStaticShooter, spawnPos[staticPos].position, Quaternion.Euler(0, 180, 0));

            StartCoroutine(InstansiateDelay(3));
            Destroy(enemyInstance, 5);
            if (gameManager.bossIsDead)
            {
                Destroy(enemyInstance);
            }
        }
        
    }

    IEnumerator InstansiateDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
        InstansiateStaticEnemy();
    }


}
