using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseBehaviour : MonoBehaviour
{

    private static bool GameInPause = false;
    [SerializeField] private GameObject UItext;
    [SerializeField] private GameObject LifeBars;
    [SerializeField] private GameObject DrugsIcons;
    [SerializeField] private GameObject Player;

    public bool exitSelected = false;
    [SerializeField] private GameObject ExitImage;
    [SerializeField] private GameObject SaveImage;
    [SerializeField] private GameObject PlayImage;
    [SerializeField] private GameObject RestartImage;
    private Animator ExitAnim;

    // Update is called once per frame
    void Update()
    {
   

        if (Input.GetButtonDown("Escape"))
        {       
                PauseGame();
          
            ExitImage.GetComponent<Animator>().SetBool("isSelected", false);
            PlayImage.GetComponent<Animator>().SetBool("isSelected", false);
            SaveImage.GetComponent<Animator>().SetBool("isSelected", false);
            RestartImage.GetComponent<Animator>().SetBool("isSelected", false);

        }

        if (GameInPause)
        {
            PauseInputs();
            //SelectAnOption();
        }

    }

   

    void PauseGame()
    {
        GameInPause = !GameInPause;
        if (GameInPause)
        {
            UItext.SetActive(true);
            DrugsIcons.SetActive(false);
            LifeBars.SetActive(false);
         
            Time.timeScale = 0f;
            
        }
        else
        {
            UItext.SetActive(false);
            DrugsIcons.SetActive(true);
            LifeBars.SetActive(true);

            Time.timeScale = 1f;
            
        }
    }

    void PauseInputs()
    {

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            
            ExitImage.GetComponent<Animator>().SetBool("isSelected", true);
            PlayImage.GetComponent<Animator>().SetBool("isSelected", false);
            SaveImage.GetComponent<Animator>().SetBool("isSelected", false);
            RestartImage.GetComponent<Animator>().SetBool("isSelected", false);
        }

         if (Input.GetAxisRaw("Horizontal") < 0)
        {
            
            PlayImage.GetComponent<Animator>().SetBool("isSelected", true);
            SaveImage.GetComponent<Animator>().SetBool("isSelected", false);
            RestartImage.GetComponent<Animator>().SetBool("isSelected", false);
            ExitImage.GetComponent<Animator>().SetBool("isSelected", false);
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
           
            SaveImage.GetComponent<Animator>().SetBool("isSelected", true);
            RestartImage.GetComponent<Animator>().SetBool("isSelected", false);
            ExitImage.GetComponent<Animator>().SetBool("isSelected", false);
            PlayImage.GetComponent<Animator>().SetBool("isSelected", false);
        }

         if (Input.GetAxisRaw("Vertical") < 0)
        {
            
            RestartImage.GetComponent<Animator>().SetBool("isSelected", true);
            ExitImage.GetComponent<Animator>().SetBool("isSelected", false);
            PlayImage.GetComponent<Animator>().SetBool("isSelected", false);
            SaveImage.GetComponent<Animator>().SetBool("isSelected", false);
        }

         


    }

    void SelectAnOption()
    {
        if (ExitImage.GetComponent<Animator>().GetBool("isSelected"))
        {
           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
         }

        if (PlayImage.GetComponent<Animator>().GetBool("isSelected"))
        {
            PauseGame();
        }

        if (RestartImage.GetComponent<Animator>().GetBool("isSelected"))
        {
            //Reiniciar escena de juego
        }

        if (SaveImage.GetComponent<Animator>().GetBool("isSelected"))
        {
            //Guardar juego
        }
    }
}
