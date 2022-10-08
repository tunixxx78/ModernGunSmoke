using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToStartGame : MonoBehaviour
{
    public void StartFreshGame()
    {
        SaveSystem.savingInstance.ResetData();
    }
    public void StartLoadedGame()
    {
        SaveSystem.savingInstance.LoadData();
    }
}
