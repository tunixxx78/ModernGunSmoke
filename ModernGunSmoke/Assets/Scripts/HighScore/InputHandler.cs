using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] string fileName;
    public HighscoreHandler highscoreHandler;
    [SerializeField] string playerNameTwo;
    [SerializeField] TMP_Text playerPoints;
    int playerScore;
    [SerializeField] int testPoints;

    public List<InputEntry> entries = new List<InputEntry>();

    private void Awake()
    {
        /*
        if (SaveSystem.savingInstance.notFirstTimeToStart == false)
        {
            highscoreHandler.AddHighscoreIfPossible(new HighscoreElement("DevTeam", 215000));
            SaveSystem.savingInstance.notFirstTimeToStart = true;
            SaveSystem.savingInstance.SaveData();
        }
        */

        //Take this off!!! ONLY For dev use

        //playerPoints.text = testPoints.ToString();

        Scene scene = SceneManager.GetActiveScene();

        if(scene.buildIndex == 5)
        {
            testPoints = PlayerPrefs.GetInt("PLRPoints");
            playerScore = testPoints;
            playerPoints.text = playerScore.ToString();
        }
        
    }

    private void Update()
    {
        /*
        Scene scene = SceneManager.GetActiveScene();

        if (scene.buildIndex != 3)
        {
            playerScore = testPoints;
            playerPoints.text = playerScore.ToString();
        }
        */
    }

    private void Start()
    {
        entries = FileHandler.ReadFromJson<InputEntry>(fileName);
        highscoreHandler = FindObjectOfType<HighscoreHandler>();
    }

    public void AddNameToList()
    {
        entries.Add(new InputEntry(nameInput.text, Random.Range(0, 100)));
        //nameInput.text = "";
        //FileHandler.SaveToJson<InputEntry>(entries, fileName);
        highscoreHandler.AddHighscoreIfPossible(new HighscoreElement(nameInput.text, playerScore));
    }
}
