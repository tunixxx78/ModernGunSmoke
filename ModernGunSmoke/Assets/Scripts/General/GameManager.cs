using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject[] roadPrefab;
    Transform sPPos;
    Vector3 sP = new Vector3(0, 0, 0);
    int spawnIndex = 0;

    [SerializeField] GameObject enemyPrefab;

    public int plrPoints;
    [SerializeField] TMP_Text playerPoints;

    public List<GameObject> roadBlocks = new List<GameObject>();
    public List<Vector3> spawnPositions = new List<Vector3>();

    private void Start()
    {
        EnemySpawnTest();
    }

    private void Update()
    {
        playerPoints.text = plrPoints.ToString();
    }


    public void SpawnNextBlock()
    {
        int randomRoadBlock = Random.Range(0, roadPrefab.Length);

        var roadInstance = Instantiate(roadPrefab[randomRoadBlock], spawnPositions[spawnIndex], Quaternion.identity);
        roadBlocks.Add(roadInstance);
        sP = new Vector3(sP.x, sP.y, sP.z + 200);

        spawnPositions.Add(sP);

        spawnIndex++;

    }

    private void EnemySpawnTest()
    {
        Instantiate(enemyPrefab, new Vector3(0, 1.5f, 0), Quaternion.identity);
    }
}
