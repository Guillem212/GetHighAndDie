using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Rigidbody2D rbPlayer;

    private Collider2D collider;

    private Transform playerPosition;

    private Vector2 targetVelocity;
    private Vector3 velocity = new Vector3();
    private float smoothMove = .005f; //Hace más suave el movimiento.

    private float movementSpeed = 300;

    public static bool isScared;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        playerPosition = GameObject.FindWithTag("Player").transform;
    }

    void Update(){
        if(isScared){
            collider.enabled = true;
        }
        else{
            collider.enabled = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player"){
            if(playerPosition.position.x > transform.position.x){
                moveEnemy(-1);
            }
            else{
                moveEnemy(1);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        
    }

    void moveEnemy(int direction){
        targetVelocity = new Vector2(direction  * movementSpeed * Time.deltaTime, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothMove);
    }
}
