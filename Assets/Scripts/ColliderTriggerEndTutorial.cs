using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderTriggerEndTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            LevelChangerScript.Instance.FadeToNextLevel("Menu");
        }
    }
}
