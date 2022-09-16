using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAvatarSkin : MonoBehaviour
{
    [SerializeField] GameObject[] bossSkins;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            bossSkins[0].SetActive(true);
            bossSkins[1].SetActive(false);
            bossSkins[2].SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            bossSkins[0].SetActive(false);
            bossSkins[1].SetActive(true);
            bossSkins[2].SetActive(false);
        }
    }
}
