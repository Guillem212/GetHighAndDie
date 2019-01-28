using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTriggerLevel2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            LevelChangerScript.Instance.FadeToNextLevel("World1Level3");
        }
    }
}
