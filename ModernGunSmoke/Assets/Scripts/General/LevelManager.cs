using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject levelOne, levelTwo, lewvelThree, levelFour, levelFive;
    [SerializeField] GameObject levelOneLock, levelTwoLock, lewvelThreeLock, levelFourLock, levelFiveLock;

    SaveSystem saveSystem;

    private void Awake()
    {
        saveSystem = FindObjectOfType<SaveSystem>();

        
    }

    private void Start()
    {

        SaveSystem.savingInstance.levelOneDone = true;
        SaveSystem.savingInstance.SaveData();


        if (SaveSystem.savingInstance.levelOneDone)
        {
            levelOne.SetActive(true);
            levelOneLock.SetActive(false);
        }
        if (SaveSystem.savingInstance.levelTwoDone)
        {
            levelTwo.SetActive(true);
            levelTwoLock.SetActive(false);
        }
    }
}
