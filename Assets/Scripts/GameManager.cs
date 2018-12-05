using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public static int enemiesKilled = 0;

    public static int playerLife = 1;

    public static bool playerDiesRestart = false;

    void Awake(){
        if(instance != null){
            Destroy(gameObject);
        }
        else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void LateUpdate(){
        if(playerLife <= 0){
            playerDiesRestart = true;
        }
        if(playerDiesRestart){
            Restart();
        }
    }

    void Restart(){
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
