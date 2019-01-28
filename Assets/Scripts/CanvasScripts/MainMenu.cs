using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        LevelChangerScript.Instance.FadeToNextLevel("World1Level1");
    }

    public void PlayTutorial()
    {
        LevelChangerScript.Instance.FadeToNextLevel("Tutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
