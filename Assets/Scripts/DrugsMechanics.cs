using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugsMechanics : MonoBehaviour
{
    public bool drugActive;
    private float speedDrug, speedNormal, cocaineJump, jumpNormal;
    private float currCountdownValueSpeed, currCountdownValueHash, currCountdownValueCocaine, currCountdownValueMeth;
    public bool speedActive, cocaineActive, hashActive, methActive;



    //DASH VARAIABLES
    [SerializeField]
    float speed, delay = 0.05f, delayPress;
    [HideInInspector]
    public bool startDelay;
    private Rigidbody2D rb;
    private int rightPress, leftPress, upPress, downPress;
    private float timePassed, timePassedPress;
    private bool startTimer;



    // Start is called before the first frame update
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


    }

   

    // Update is called once per frame
    void Update()
    {
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
                    else if (upPress >= 2)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, speed);
                        leftPress = 0;
                    }
                    else if (downPress >= 2)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, -speed);
                        leftPress = 0;
                    }
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

        if (Input.GetKeyDown(KeyCode.Q) && !cocaineActive)
        {

            cocaineActive = true;
            StartCoroutine(StartCocaine());


        }

        if (Input.GetKeyDown(KeyCode.W) && !hashActive)
        {

            hashActive = true;
            StartCoroutine(StartHash());
            


        }

        if (Input.GetKeyDown(KeyCode.E) && !speedActive)
        {
            speedActive = true;
            StartCoroutine(StartSpeed());


        }
          if (Input.GetKeyDown(KeyCode.R) && !methActive)
            {
            methActive = true;
            StartCoroutine(StartMeth());
            DashController();

    



        }

        if (cocaineActive || hashActive || methActive || speedActive)
            drugActive = true;
        else
            drugActive = false;





    }
    

    

    public IEnumerator StartSpeed(float countdownValueS = 10)
    {
        currCountdownValueSpeed = countdownValueS;
        while (currCountdownValueSpeed > 0)
        {
            //HACER ANIMACION SPEED        
            yield return new WaitForSeconds(0.5f);
            GetComponent<playerMovement>().movementSpeed = speedDrug;
            currCountdownValueSpeed--;
           
        }
        GetComponent<playerMovement>().movementSpeed = speedNormal;
        speedActive = false;
    }

    public IEnumerator StartCocaine(float countdownValueC = 10)
    {
        currCountdownValueCocaine = countdownValueC;
        while (currCountdownValueCocaine > 0)
        {
           //HACER ANIMACION COCAINE
            yield return new WaitForSeconds(0.5f);
            GetComponent<playerMovement>().jumpVel = cocaineJump;
            currCountdownValueCocaine--;

        }
        GetComponent<playerMovement>().jumpVel = jumpNormal;
        cocaineActive = false;
    }

    public IEnumerator StartHash(float countdownValueH = 10)
    {
        currCountdownValueHash = countdownValueH;
        while (currCountdownValueHash > 0)
        {
          //HACER ANIMACION HASH
            yield return new WaitForSeconds(0.5f);
            //RALENTIZAR LA VELOCIDAD DE LOS OBJETOS EXCEPTO LA DEL JUGADOR
            
            currCountdownValueHash--;

        }
        //NORMALIZAR LA VELOCIDAD LOS OBJETOS 
        
        hashActive = false;
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
        else if ((Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0))
        {
            upPress++;
            startTimer = true;

        }
        else if ((Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0))
        {
            downPress++;
            startTimer = true;

        }


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
