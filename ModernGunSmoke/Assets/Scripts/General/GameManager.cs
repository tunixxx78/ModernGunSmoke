using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject[] roadPrefab;
    [SerializeField] GameObject FinalRoadBlok, pausePanel, gameOverPanel, youWonPanel;
    [SerializeField] Sprite[] bossImages;
    public int[] pointsFromBoss;
    [SerializeField] GameObject bossImageSpotWon, bossImageSpotLost;
    Transform sPPos;
    Vector3 sP = new Vector3(0, 0, 0);
    int spawnIndex = 0;

    [SerializeField] GameObject enemyPrefab;

    public int plrPoints, plrHealth, plrAmmoCount = 5, currentHighScore;

    [SerializeField] TMP_Text playerPointsText, highScoreText, playerHealthText, ammoCountText;

    public List<GameObject> roadBlocks = new List<GameObject>();
    public List<Vector3> spawnPositions = new List<Vector3>();

    [SerializeField] HealthBar plrHealthBar;
    public AmmoBar plrAmmoBar;

    public bool forcingMovement;
    [SerializeField] int wantedAmountOfBloks, stopingDelay;
    PlrMovement plrMovement;

    public bool bossIsDead = false;

    private void Awake()
    {
        plrMovement = FindObjectOfType<PlrMovement>();
        highScoreText.text = "500000";
    }

    private void Start()
    {
        EnemySpawnTest();
        playerHealthText.text = plrHealth.ToString();
        ammoCountText.text = plrAmmoCount.ToString();
        plrHealthBar.SetMaxHealth(plrHealth);
        plrAmmoBar.SetMaxAmmo(plrAmmoCount);
        forcingMovement = true;

        int bestScore = PlayerPrefs.GetInt("BestScore");
        highScoreText.text = bestScore.ToString();

        plrPoints = PlayerPrefs.GetInt("PLRPoints");

    }

    private void Update()
    {
        playerPointsText.text = plrPoints.ToString();
        playerHealthText.text = plrHealth.ToString();
        ammoCountText.text = plrAmmoCount.ToString();
        plrHealthBar.SetHealth(plrHealth);
        plrAmmoBar.SetAmmo(plrAmmoCount);

        if(roadBlocks.Count >= wantedAmountOfBloks)
        {
            StartCoroutine(StopForcedMovement());
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }

        //ONLY FOR DEV USE!
        /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerPrefs.SetInt("PLRPoints", plrPoints);

            SceneManager.LoadScene(3);
        }
        */

        
    }


    public void SpawnNextBlock()
    {
        int randomRoadBlock = Random.Range(0, roadPrefab.Length);

        if(roadBlocks.Count >= wantedAmountOfBloks - 1)
        {
            var roadInstance = Instantiate(FinalRoadBlok, spawnPositions[spawnIndex], Quaternion.identity);
            roadBlocks.Add(roadInstance);
            sP = new Vector3(sP.x, sP.y, sP.z + 200);
        }
        else
        {
            var roadInstance = Instantiate(roadPrefab[randomRoadBlock], spawnPositions[spawnIndex], Quaternion.identity);
            roadBlocks.Add(roadInstance);
            sP = new Vector3(sP.x, sP.y, sP.z + 200);
        }

        

        spawnPositions.Add(sP);

        spawnIndex++;

    }

    private void EnemySpawnTest()
    {
        Instantiate(enemyPrefab, new Vector3(0, 1.5f, 0), Quaternion.Euler(0, -180, 0));
    }

    private IEnumerator StopForcedMovement()
    {
        yield return new WaitForSeconds(stopingDelay);

        forcingMovement = false;
        plrMovement.onTheWay = false;
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        Scene scene = SceneManager.GetActiveScene();
        bossImageSpotLost.GetComponent<Image>().sprite = bossImages[scene.buildIndex - 2];

        //bossImageSpotLost.GetComponent<Image>().
        PlayerPrefs.SetInt("PLRPoints", plrPoints);
    }
    public void ShowYouWonPanel()
    {
        youWonPanel.SetActive(true);

        Scene scene = SceneManager.GetActiveScene();
        bossImageSpotWon.GetComponent<Image>().sprite = bossImages[scene.buildIndex - 2];

        //bossImageSpotWon = bossImages[scene.buildIndex + 1];
        PlayerPrefs.SetInt("PLRPoints", plrPoints);
        

    }

    public void SaveCurrentProgress()
    {
        Scene scene = SceneManager.GetActiveScene();

        if(scene.buildIndex == 2)
        {
            SaveSystem.savingInstance.levelTwoDone = true;
            SaveSystem.savingInstance.SaveData();
        }
        if (scene.buildIndex == 3)
        {
            SaveSystem.savingInstance.levelThreeDone = true;
            SaveSystem.savingInstance.SaveData();
        }
        if (scene.buildIndex == 4)
        {
            SaveSystem.savingInstance.levelFourDone = true;
            SaveSystem.savingInstance.SaveData();
        }
        if (scene.buildIndex == 5)
        {
            SaveSystem.savingInstance.levelFiveDone = true;
            SaveSystem.savingInstance.SaveData();
        }

    }
}
