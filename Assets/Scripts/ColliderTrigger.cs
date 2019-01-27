using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    private GameObject canvas;

    void Start() {
        canvas = transform.GetChild(0).gameObject;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            canvas.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player") {
            canvas.SetActive(false);
        }
    }
}
