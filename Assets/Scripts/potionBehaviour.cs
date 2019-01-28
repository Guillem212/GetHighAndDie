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
    private float lifeToAdd = 0.0125f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            potionTouched = true;                     
        }
    }

    void Update()
    {
        if (potionTouched)
        {
            playerManager.GetComponent<PlayerManager>().AddAmount(lifeToAdd); 
            potionCatched = true;
        }

        if (potionCatched && playerManager.GetComponent<PlayerManager>().GetLifeBar() < playerManager.GetComponent<PlayerManager>().GetLifeBarRest())
        {
            playerManager.GetComponent<PlayerManager>().AddLife(lifeToAdd);
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,0f);

            if (potionCatched && playerManager.GetComponent<PlayerManager>().GetLifeBar() >= playerManager.GetComponent<PlayerManager>().GetLifeBarRest())
            {
                potionCatched = false;
                Destroy(gameObject);
            }
        }
 
       
    }
}


