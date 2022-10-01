using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject[] roadPrefab;
    [SerializeField] GameObject FinalRoadBlok;
    Transform sPPos;
    Vector3 sP = new Vector3(0, 0, 0);
    int spawnIndex = 0;

    [SerializeField] GameObject enemyPrefab;

    public int plrPoints, plrHealth, plrAmmoCount = 0;

    [SerializeField] TMP_Text playerPointsText, highScoreText, playerHealthText, ammoCountText;

    public List<GameObject> roadBlocks = new List<GameObject>();
    public List<Vector3> spawnPositions = new List<Vector3>();

    [SerializeField] HealthBar plrHealthBar;

    public bool forcingMovement;
    [SerializeField] int wantedAmountOfBloks, stopingDelay;
    PlrMovement plrMovement;

    private void Awake()
    {
        plrMovement = FindObjectOfType<PlrMovement>();
    }

    private void Start()
    {
        EnemySpawnTest();
        playerHealthText.text = plrHealth.ToString();
        ammoCountText.text = plrAmmoCount.ToString();
        plrHealthBar.SetMaxHealth(plrHealth);
        forcingMovement = true;
    }

    private void Update()
    {
        playerPointsText.text = plrPoints.ToString();
        playerHealthText.text = plrHealth.ToString();
        ammoCountText.text = plrAmmoCount.ToString();
        plrHealthBar.SetHealth(plrHealth);

        if(roadBlocks.Count >= wantedAmountOfBloks)
        {
            StartCoroutine(StopForcedMovement());
        }

        //ONLY FOR DEV USE!

        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerPrefs.SetInt("PLRPoints", plrPoints);

            SceneManager.LoadScene(3);
        }
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
}
