using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private playerMovement player;

    public GameObject particles;
    private GameObject dustNow;

    private ParticleSystem ps;
    private ParticleSystem.MainModule main; 

    public LayerMask groundLayer;
    private RaycastHit2D hit;

    private void Awake() {
        player = GetComponentInParent<playerMovement>();
    }

    void Start()
    {
        dustNow = Instantiate(particles);
        ps = dustNow.GetComponent<ParticleSystem>();
    }

    void Update(){
        if (player.horizontalMove != 0 && player.playerSpeedY == 0){
            StartLoop();
        }
        else if(dustNow.activeSelf){
            StartCoroutine("KillParticles");
        }
        dustNow.transform.position = transform.position;
    }

    IEnumerator KillParticles(){
        main = ps.main;
        main.loop = false;
        yield return new WaitForSeconds(0.3f);
        dustNow.SetActive(false);
    }

     void StartLoop(){
        if (dustNow.activeSelf){
            return;
        }
        dustNow.SetActive(true);
        main = ps.main;
        main.loop = true;
    }

}