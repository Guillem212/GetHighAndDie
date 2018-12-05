using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            GameManager.changeLevel = true;
        }
        
    }
}
