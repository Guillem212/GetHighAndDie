using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{

    public Rigidbody2D playerRB;

    public GameObject particles;
    private GameObject dustNow;

    private ParticleSystem ps;
    private ParticleSystem.MainModule main; 

    public LayerMask groundLayer;

    private Vector3 offset;
    private RaycastHit2D hit;


    void Start()
    {
        dustNow = Instantiate(particles);
        ps = dustNow.GetComponent<ParticleSystem>();
        ps.Play();

    }

    void LateUpdate(){  
        if (Input.GetAxis("Horizontal") != 0 && detectGround()){
            StartLoop();
        }
        else{
            StartCoroutine("KillParticles");
        }
        dustNow.transform.position = transform.position;
    }

    IEnumerator KillParticles(){
        if (!dustNow.activeSelf){
            yield return new WaitForSeconds(0.01f);
        }
        main = ps.main;
        main.loop = false;
        yield return new WaitForSeconds(0.5f);
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

    bool detectGround(){	
		offset = new Vector3(transform.position.x, transform.position.y -0.5f);
		//Debug.DrawRay(offset, Vector2.down / 3, Color.green);
		hit = Physics2D.CircleCast(offset, 0.05f, Vector2.down, 0.05f, groundLayer);
		if (hit.collider != null) {
			return true;
		}
		return false;
	}

}