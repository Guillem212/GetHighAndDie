using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start() {
        FindObjectOfType<AudioManager>().Stop("MainTheme");
        FindObjectOfType<AudioManager>().Play("MenuTheme");
    }

    public void PlayGame()
    {
        LevelChangerScript.Instance.FadeToNextLevel("World1Level1");
        FindObjectOfType<AudioManager>().Stop("MenuTheme");
        FindObjectOfType<AudioManager>().Play("MainTheme");
    }

    public void PlayTutorial()
    {
        LevelChangerScript.Instance.FadeToNextLevel("Tutorial");
        FindObjectOfType<AudioManager>().Stop("MenuTheme");
        FindObjectOfType<AudioManager>().Play("MainTheme");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
