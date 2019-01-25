using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public Animator animator;
    public PlayerManager playerManager;
    private TextMeshProUGUI mText;

    public static GameManager instance;

    public GameObject[] levelArray;

    private int actualLevel;
    private int actualWorld = 0;
    public static bool changeLevel = false;

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
        actualLevel = 0;
    }

    void Start(){
        actualLevel = 0;
        changeWorld();
        animator = GetComponentInChildren<Animator>();
        mText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        mText.text = "World " + actualWorld.ToString() + " Level " + (actualLevel+1).ToString();

        playerManager = GameObject.FindWithTag("CanvasManger").GetComponent<PlayerManager>();
    }

    void LateUpdate(){
        if(changeLevel){
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
        }
    }

    public void FadeToLevel(){
        animator.SetBool("IsFading", true);
        levelArray[actualLevel-1].SetActive(false);
        playerManager.amount = 1f;
        playerManager.LifeBar.fillAmount = 1f;
        playerManager.LifeBarRest.fillAmount = 1f;
    }

    public void OnFadeComplete(){
        animator.SetBool("IsFading", false);
        levelArray[actualLevel].SetActive(true);
    }

    void changeWorld(){
        if(SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1){
            actualWorld++;
            levelArray = new GameObject[3];
            levelArray = new GameObject[]{GameObject.FindWithTag("Level1"), GameObject.FindWithTag("Level2"), GameObject.FindWithTag("Level3")};
        }
        else{
            actualWorld = 1;
        }
    }

    public void RestartWorld(){
        animator.SetBool("IsFading", true);
        levelArray[actualLevel].SetActive(false);
        actualLevel = 0;
    }
}
