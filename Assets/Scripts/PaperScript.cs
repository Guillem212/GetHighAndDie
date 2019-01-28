using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // FindObjectOfType<AudioManager>().Stop("MainTheme");
        FindObjectOfType<AudioManager>().Play("DarkDefeat");
    }
}
