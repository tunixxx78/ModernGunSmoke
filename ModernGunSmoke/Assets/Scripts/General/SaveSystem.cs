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


    private void Awake()
    {
        LoadData();

        Screen.SetResolution(1920, 1080, true);

        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 0)
        {
            PlayerPrefs.SetInt("PointsToNextLevel", 0);
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
        FileStream file = File.Create(Application.persistentDataPath + "/gamedataaa.dat");
        GameData data = new GameData();

        data.levelOneDone = levelOneDone;
        data.levelTwoDone = levelTwoDone;
        data.levelThreeDone = levelThreeDone;
        data.levelFourDone = levelFourDone;
        data.levelFiveDone = levelFiveDone;
       
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/gamedataaa.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamedataaa.dat", FileMode.Open);
            GameData data = (GameData)bf.Deserialize(file);

            levelOneDone = data.levelOneDone;
            levelTwoDone = data.levelTwoDone;
            levelThreeDone = data.levelThreeDone;
            levelFourDone = data.levelFourDone;
            levelFiveDone = data.levelFiveDone;

        }
    }
}

[Serializable]

class GameData
{
    public bool levelOneDone, levelTwoDone, levelThreeDone, levelFourDone, levelFiveDone;

}
