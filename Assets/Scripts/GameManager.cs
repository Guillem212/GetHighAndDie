using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    //public Animator animator;
    //public PlayerManager playerManager;

    public static GameManager instance;

    public int actualLevel;
    public int actualWorld;

    public static int enemiesKilled = 0;
    public static int playerLife = 1;

    void Awake(){
        if(instance != null){
            Destroy(gameObject);
        }
        else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start(){
        actualLevel = 0;
        actualWorld = 1;
        //animator = GetComponentInChildren<Animator>();
        //mText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        //mText.text = "World " + actualWorld.ToString() + " Level " + (actualLevel+1).ToString();

        //playerManager = GameObject.FindWithTag("CanvasManger").GetComponent<PlayerManager>();
    }

    void LateUpdate(){
        /*if(changeLevel){
            if(actualLevel < 2){
                actualLevel++;
                FadeToLevel();
                mText.text = "World " + actualWorld.ToString() + " Level " + (actualLevel-1).ToString();
            }
            else{
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                actualLevel = 0;
                changeLevel = false;
                changeWorld();
            }
            levelArray[actualLevel].SetActive(true);
            changeLevel = false;
        }*/
    }

    /*public void RestartWorld(){
        animator.SetBool("IsFading", true);
        levelArray[actualLevel].SetActive(false);
        actualLevel = 0;
    }*/
}
