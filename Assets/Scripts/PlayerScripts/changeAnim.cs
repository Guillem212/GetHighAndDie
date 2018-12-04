using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeAnim : MonoBehaviour
{

    private GameObject player;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<DrugsMechanics>().GetMethActive())
            anim.SetBool("meth", true);
        else if (player.GetComponent<DrugsMechanics>().GetHashActive())
            anim.SetBool("hash", true);
        else if (player.GetComponent<DrugsMechanics>().GetCocaineActive())
            anim.SetBool("cocaine", true);
        else if (player.GetComponent<DrugsMechanics>().GetSpeedActive())
            anim.SetBool("speed", true);


    }
}
