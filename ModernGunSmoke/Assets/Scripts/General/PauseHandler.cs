using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    public void ReturnToGame()
    {
        Time.timeScale = 1;
    }
}
