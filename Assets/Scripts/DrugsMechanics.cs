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
        //Input de Guillem del Dash
        //
        dashInput = Input.GetAxis("Meth") != 0;
        dashGuillem();
        //
        //Fin de los inputs

        DashController();
        if (methActive)
        {
            if (startDelay)
            {
                timePassed += Time.fixedDeltaTime;

                if (timePassed <= delay)
                {
                    if (rightPress >= 2)
                    {
                        rb.velocity = new Vector2(speed, rb.velocity.y);
                        rightPress = 0;
                    }
                    else if (leftPress >= 2)
                    {
                        rb.velocity = new Vector2(-speed, rb.velocity.y);
                        leftPress = 0;
                    }
                    /*else if (upPress >= 2)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, speed);
                        leftPress = 0;
                    }
                    else if (downPress >= 2)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, -speed);
                        leftPress = 0;
                    }*/
                }
                else
                {
                    timePassed = 0;
                    startDelay = false;
                    rightPress = 0;
                    leftPress = 0;


                }


            }
        }

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
        if (Input.GetKeyDown(KeyCode.R) && !methActive)
        {
            methActive = true;
            /*StartCoroutine(StartMeth());
            DashController();*/
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

    public IEnumerator StartMeth(float countdownValueM = 10)
    {
        currCountdownValueMeth = countdownValueM;
        while (currCountdownValueMeth > 0)
        {
         //HACER ANIMACION METH
            yield return new WaitForSeconds(0.5f);
            
            currCountdownValueMeth--;

        }
      
        methActive = false;
    }

    ///
    //CODIGO ALTERNATIVO AL DASH
    ///
    void dashGuillem(){
        if(!canDash){
            dashCD -= Time.deltaTime;
            if(dashCD < 0){
                dashCD = 2f;
                canDash = true;
            }
        }

        direction = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        if(dashInput && canDash){
            if(direction != Vector3.zero){
                detectWallDash(direction);
            }
        }
    }
    
    void detectWallDash(Vector3 dir){
		hit = Physics2D.Raycast(transform.position + dir * 10f, dir, 0.01f, groundLayer);
		if (hit.collider == null){
            Vector3 offset = new Vector3(transform.position.x + 10f * dir.x, transform.position.y, transform.position.z);
            transform.position = offset;
            canDash = false;
		}
	}
    ///
    //FIN DEL CODIGO ALTERNATIVO
    ///

    void DashController()
    {
        if ((Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0))
        {
            leftPress++;
            startTimer = true;
        }
        else if ((Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0))
        {
            rightPress++;
            startTimer = true;

        }
        /*else if ((Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0))
        {
            upPress++;
            startTimer = true;

        }
        else if ((Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0))
        {
            downPress++;
            startTimer = true;

        }*/


        if (startTimer)
        {
            timePassedPress += Time.deltaTime;
            if (timePassedPress >= delayPress)
            {
                startTimer = false;
                leftPress = 0;
                rightPress = 0;
                upPress = 0;
                downPress = 0;
                timePassedPress = 0;

            }
        }

        if (leftPress >= 2 || rightPress >= 2)
        {
            startDelay = true;


        }
        if (upPress >= 2 || downPress >= 2)
        {
            startDelay = true;


        }
    }







}
