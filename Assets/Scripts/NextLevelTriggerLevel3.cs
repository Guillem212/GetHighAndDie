using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTriggerLevel3 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
            if(other.tag == "Player"){
                LevelChangerScript.Instance.FadeToNextLevel("FinalPaper");
            }
        }
}
