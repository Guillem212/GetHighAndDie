using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Quitar y sustituir por LevelManager
        SceneManager.LoadScene("Scenes/Mundo 1");
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("Scenes/Tutorial");
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
