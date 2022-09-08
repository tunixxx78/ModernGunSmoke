using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject plr;
    [SerializeField] Transform[] spawnpoints;
    [SerializeField] GameObject roadPrefab;

    public void SpawnNextBlock()
    {
        Instantiate(roadPrefab, spawnpoints[0].position, Quaternion.identity);
    }
}
