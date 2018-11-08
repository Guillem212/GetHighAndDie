using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugsMechanics : MonoBehaviour
{
    public bool drugActive;
    private float speedDrug, speedNormal, cocaineJump, jumpNormal;
    private float currCountdownValueSpeed, currCountdownValueHash, currCountdownValueCocaine, currCountdownValueMeth;
    public bool speedActive, cocaineActive, hashActive, methActive;

    private Animator anim;



    //DASH VARAIABLES
    [SerializeField]
    float speed, delay = 0.05f, delayPress;
    [HideInInspector]
    public bool startDelay;
    private Rigidbody2D rb;
    private int rightPress, leftPress, upPress, downPress;
    private float timePassed, timePassedPress;
    private bool startTimer;


    //Dash de Guillem
    private bool canDash = true;
    private float dashCD = 2f;
    private bool dashInput;
    private RaycastHit2D hit;
    public LayerMask groundLayer;
    Vector3 direction = Vector3.zero;

    void Start()
    {
        drugActive = false;
        speedDrug = GetComponent<playerMovement>().movementSpeed * 2;
        speedNormal = GetComponent<playerMovement>().movementSpeed;

        cocaineJump = GetComponent<playerMovement>().jumpVel * 1.5f;
        jumpNormal = GetComponent<playerMovement>().jumpVel;

        rightPress = 0;
        leftPress = 0;
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
    }

     // Update is called once per frame
    void Update()
    {
        
        if (Input.GetAxis("Cocaina") != 0 && !cocaineActive)
        {

            cocaineActive = true;

            StartCoroutine(cocaineAnim());


        }

        if (Input.GetAxis("Hash") != 0 && !hashActive)
        {

            hashActive = true;
            StartCoroutine(hashAnim());
            


        }

        if (Input.GetAxis("Speed") != 0 && !speedActive)
        {
            
            speedActive = true;
            StartCoroutine(speedAnim());


        }
        if (Input.GetAxis("Meth") != 0 && !methActive)
        {
            methActive = true;
            StartCoroutine(MethAnim());
        }

        if (cocaineActive || hashActive || methActive || speedActive)
            drugActive = true;
        else
            drugActive = false;





    }
    

    

    public IEnumerator StartSpeed()
    {
        GetComponent<playerMovement>().movementSpeed = speedDrug;
        yield return new WaitForSeconds(5f);
        speedActive = false;
        GetComponent<playerMovement>().movementSpeed = speedNormal;
    }

    public IEnumerator speedAnim(){
        anim.SetBool("isCristal", true);
        yield return new WaitForSeconds(1.06f);
        anim.SetBool("isCristal", false);
        
        StartCoroutine(StartSpeed());
    }

    public IEnumerator StartCocaine()
    {
        GetComponent<playerMovement>().jumpVel = cocaineJump;
        yield return new WaitForSeconds(5f);
        GetComponent<playerMovement>().jumpVel = jumpNormal;
        cocaineActive = false;
    }

    public IEnumerator cocaineAnim(){
        anim.SetBool("isCocaine", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("isCocaine", false);
    
        StartCoroutine(StartCocaine());
    }

    public IEnumerator StartHash()
    {
        //RALENTIZAR LA VELOCIDAD DE LOS OBJETOS EXCEPTO LA DEL JUGADOR
        yield return new WaitForSeconds(5f);
        //NORMALIZAR LA VELOCIDAD LOS OBJETOS 
        
        hashActive = false;
    }

    public IEnumerator hashAnim(){
        anim.SetBool("isSmoking", true);
        yield return new WaitForSeconds(2.6f);
        anim.SetBool("isSmoking", false);
    
        StartCoroutine(StartHash());
    }

    public IEnumerator StartMeth()
    {
        dashMeth();
        yield return new WaitForSeconds(1f);
        methActive = false;
    }

    public IEnumerator MethAnim(){
        anim.SetBool("isCristal", true);
        yield return new WaitForSeconds(.1f);
        anim.SetBool("isCristal", false);
    
        StartCoroutine(StartMeth());
    }

    void dashMeth(){
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        if(direction != Vector3.zero){
            detectWallDash(direction);
        }
    }
    
    void detectWallDash(Vector3 dir){
		hit = Physics2D.Raycast(transform.position + dir * 10f, dir, 0.01f, groundLayer);
		if (hit.collider == null){
            Vector3 offset = new Vector3(transform.position.x + 10f * dir.x, transform.position.y, transform.position.z);
            transform.position = offset;
		}
	}

}
