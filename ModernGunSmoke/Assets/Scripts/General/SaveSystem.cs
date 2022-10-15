using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem savingInstance;

    public bool levelOneDone, levelTwoDone, levelThreeDone, levelFourDone, levelFiveDone;
    public int savedPoints;

    public bool startWithLoad = false;


    private void Awake()
    {
        LoadData();

        Screen.SetResolution(1920, 1080, true);

        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 0)
        {
            //PlayerPrefs.SetInt("PointsToNextLevel", 0);
            //PlayerPrefs.SetInt("PLRPoints", 0);
        }



        if (savingInstance != null && savingInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        savingInstance = this;
        DontDestroyOnLoad(this);

    }

    public void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedata.dat");
        GameData data = new GameData();

        data.levelOneDone = levelOneDone;
        data.levelTwoDone = levelTwoDone;
        data.levelThreeDone = levelThreeDone;
        data.levelFourDone = levelFourDone;
        data.levelFiveDone = levelFiveDone;

        savedPoints = PlayerPrefs.GetInt("PLRPoints");
        data.savedPoints = savedPoints;
       
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/savedata.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedata.dat", FileMode.Open);
            GameData data = (GameData)bf.Deserialize(file);

            levelOneDone = data.levelOneDone;
            levelTwoDone = data.levelTwoDone;
            levelThreeDone = data.levelThreeDone;
            levelFourDone = data.levelFourDone;
            levelFiveDone = data.levelFiveDone;

            savedPoints = data.savedPoints;

            PlayerPrefs.SetInt("PLRPoints", savedPoints);

        }
    }

    public void ResetData()
    {
        levelOneDone = false;
        levelTwoDone = false;
        levelThreeDone = false;
        levelFourDone = false;
        levelFiveDone = false;

        PlayerPrefs.SetInt("PLRPoints", 0);

        
    }

}

[Serializable]

class GameData
{
    public bool levelOneDone, levelTwoDone, levelThreeDone, levelFourDone, levelFiveDone;
    public int savedPoints;

}
