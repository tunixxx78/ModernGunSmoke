using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossAvatarSkin : MonoBehaviour
{
    [SerializeField] GameObject[] bossSkins;
    [SerializeField] int enemy1Health, enemy2Health, enemy3Health, enemy4Health, enemy5Health;
    EnemyBase enemyBase;

    private void Awake()
    {
        enemyBase = GetComponent<EnemyBase>();


        Scene scene = SceneManager.GetActiveScene();

        if (scene.buildIndex == 2)
        {
            bossSkins[0].SetActive(true);
            bossSkins[1].SetActive(false);
            bossSkins[2].SetActive(false);
            bossSkins[3].SetActive(false);
            bossSkins[4].SetActive(false);

            enemyBase.enemyHealth = enemy1Health;
        }

        if (scene.buildIndex == 3)
        {
            bossSkins[0].SetActive(false);
            bossSkins[1].SetActive(true);
            bossSkins[2].SetActive(false);
            bossSkins[3].SetActive(false);
            bossSkins[4].SetActive(false);

            enemyBase.enemyHealth = enemy2Health;
        }

        if (scene.buildIndex == 4)
        {
            bossSkins[0].SetActive(false);
            bossSkins[1].SetActive(false);
            bossSkins[2].SetActive(true);
            bossSkins[3].SetActive(false);
            bossSkins[4].SetActive(false);

            enemyBase.enemyHealth = enemy3Health;
        }

        if (scene.buildIndex == 5)
        {
            bossSkins[0].SetActive(false);
            bossSkins[1].SetActive(false);
            bossSkins[2].SetActive(false);
            bossSkins[3].SetActive(true);
            bossSkins[4].SetActive(false);

            enemyBase.enemyHealth = enemy4Health;
        }

        if (scene.buildIndex == 6)
        {
            bossSkins[0].SetActive(false);
            bossSkins[1].SetActive(false);
            bossSkins[2].SetActive(false);
            bossSkins[3].SetActive(false);
            bossSkins[4].SetActive(true);

            enemyBase.enemyHealth = enemy5Health;
        }
    }

    private void Start()
    {
        
    }

    
}
