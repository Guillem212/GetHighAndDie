using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeAnim : MonoBehaviour
{

    private GameObject player;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<DrugsMechanicsSergio>().methActive)
            anim.SetBool("meth", true);
        else if (player.GetComponent<DrugsMechanicsSergio>().hashActive)
            anim.SetBool("hash", true);
        else if (player.GetComponent<DrugsMechanicsSergio>().cocaineActive)
            anim.SetBool("cocaine", true);
        else if (player.GetComponent<DrugsMechanicsSergio>().speedActive)
            anim.SetBool("speed", true);


    }
}
