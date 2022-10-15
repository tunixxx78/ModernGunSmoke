using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject levelOne, levelTwo, levelThree, levelFour, levelFive;
    [SerializeField] GameObject levelOneLock, levelTwoLock, levelThreeLock, levelFourLock, levelFiveLock;

    SaveSystem saveSystem;

    private void Awake()
    {
        saveSystem = FindObjectOfType<SaveSystem>();

        
    }

    private void Start()
    {
        
        SaveSystem.savingInstance.levelOneDone = true;
        

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
        if (SaveSystem.savingInstance.levelThreeDone)
        {
            levelThree.SetActive(true);
            levelThreeLock.SetActive(false);
        }
        if (SaveSystem.savingInstance.levelFourDone)
        {
            levelFour.SetActive(true);
            levelFourLock.SetActive(false);
        }
        if (SaveSystem.savingInstance.levelFiveDone)
        {
            levelFive.SetActive(true);
            levelFiveLock.SetActive(false);
        }
    }
}
