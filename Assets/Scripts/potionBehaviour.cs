using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionBehaviour : MonoBehaviour
{
    private bool potionTouched = false;
    private bool potionCatched = false;
    [SerializeField]
    private GameObject playerManager;
    [SerializeField]
    private float lifeToAdd = 0.0625f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            potionTouched = true;                     
        }
    }

    void Update()
    {
        if (potionTouched && Input.GetKeyDown(KeyCode.P))
        {
            playerManager.GetComponent<PlayerManager>().AddAmount(lifeToAdd); 
            potionCatched = true;
        }

        if (potionCatched && playerManager.GetComponent<PlayerManager>().GetLifeBar() < playerManager.GetComponent<PlayerManager>().GetAmount())
        {
            playerManager.GetComponent<PlayerManager>().AddLife(lifeToAdd);   
        }

        if (potionCatched && playerManager.GetComponent<PlayerManager>().GetLifeBar() >= playerManager.GetComponent<PlayerManager>().GetAmount())
        {
            potionCatched = false;
            Destroy(gameObject);
        }
    }
}


