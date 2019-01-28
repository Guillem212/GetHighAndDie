using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelChangerScript : MonoBehaviour
{
    private static LevelChangerScript instance;
    public static LevelChangerScript Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<LevelChangerScript>();
            return instance;
        }
    }

    private TextMeshProUGUI text;

    private Animator animator;
    private string levelToLoad;

    void Awake(){
        animator = gameObject.GetComponent<Animator>();
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void FadeToNextLevel(string nameScene)
    {
        if(nameScene != "Tutorial"){
            if(nameScene == "Menu" || nameScene == "VideoScene" || nameScene == "FinalPaper"){
                GameManager.instance.actualWorld = 1;
                GameManager.instance.actualLevel = 0;
                text.text = " ";
            }
            else{
                if(GameManager.instance.actualLevel >= 3){
                    GameManager.instance.actualWorld ++;
                    GameManager.instance.actualLevel = 0;
                }
                else{
                    GameManager.instance.actualLevel++;
                }
                text.text = "World " + GameManager.instance.actualWorld.ToString() + " Level " + GameManager.instance.actualLevel.ToString();
            }
        }
        else{
            text.text = "This is the Tutorial, so... Git Gud.";
        }
        FadeToLevel(nameScene);
    }

    public void FadeToLevel(string nameScene)
    {
        levelToLoad = nameScene;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}


