using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{

    [SerializeField] float startVolume, currentVolume, wantedVolume;
    AudioSource music;

    private void Awake()
    {
        music = GameObject.Find("Music").GetComponent<AudioSource>();
        currentVolume = 0;
        wantedVolume = 1;
    }

    private void Start()
    {
        StartCoroutine(FadeMusicOn());
    }

    public void SceneChange(int sceneIndex)

    {
        StartCoroutine(SceneChangeDelay(2f, sceneIndex));
        StartCoroutine(FadeMusicOff());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator SceneChangeDelay(float delayTime, int sceneIndex)
    {
        yield return new WaitForSeconds(delayTime);

        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator FadeMusicOn()
    {
        startVolume = 0;
        currentVolume = 0;

        while (currentVolume < wantedVolume)
        {
            currentVolume = currentVolume + 0.2f * Time.deltaTime;
            music.volume = currentVolume;

            yield return null;
        }
    }

    IEnumerator FadeMusicOff()
    {
        startVolume = music.volume;
        currentVolume = music.volume;

        while (currentVolume > 0)
        {
            currentVolume = currentVolume - 0.45f * Time.deltaTime;
            music.volume = currentVolume;

            yield return null;
        }

        music.Stop();
        music.volume = startVolume;
    }
}
