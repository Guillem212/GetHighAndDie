using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Rigidbody2D rb;
    private Rigidbody2D rbPlayer;

    private Collider2D collider;
    //public GameObject colliderEnem;

    private Transform playerPosition;

    private Vector2 targetVelocity;
    private Vector3 velocity = new Vector3();
    private float smoothMove = .005f; //Hace más suave el movimiento.

    private float movementSpeed = 300;

    public static bool isScared;
    private bool isDead = false;
    private bool alreadyDead = false;

    private Animator anim;
    private SpriteRenderer enemyPos;
    private bool lookingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        //colliderEnem = GetComponentInParent<GameObject>();
        playerPosition = GameObject.FindWithTag("Player").transform;
        anim = GetComponentInParent<Animator>();
        enemyPos =GetComponentInParent<SpriteRenderer>();
    }

    void Update(){
        if(!isDead){
            if(isScared){
                collider.enabled = true;
                anim.SetBool("isScared", true);
            }
            else{
                anim.SetBool("isScared", false);
                collider.enabled = false;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        else if(!alreadyDead){
            StartCoroutine(deadAnim());
            alreadyDead = true;
        }
    }

    IEnumerator deadAnim(){
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(0.1f);
        Collider2D aux = GetComponentInParent<Collider2D>();
        aux.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        collider.enabled = false;


    }
    
    public void dieEnemy(){
        isDead = true;
    }

    void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player"){
            if(playerPosition.position.x > transform.position.x){
                moveEnemy(-1);
                if(lookingRight){
                    Flip();
                }
            }
            else{
                if(!lookingRight){
                    Flip();
                }
                moveEnemy(1);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            anim.SetBool("isMoving", false);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        
    }

    void moveEnemy(int direction){
        anim.SetBool("isMoving", true);
        targetVelocity = new Vector2(direction  * movementSpeed * Time.deltaTime, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothMove);
    }

    void Flip(){
		lookingRight = !lookingRight;
		enemyPos.flipX = !enemyPos.flipX;
	}
}
