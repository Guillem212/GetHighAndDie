using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugsMechanicsSergio : MonoBehaviour
{
    public bool drugActive;
    private float speedDrug, speedNormal, cocaineJump, jumpNormal;
    public float methDelay = 1f;
    public bool speedActive, cocaineActive, hashActive, methActive;

    private Animator anim;
    public float timeDrugActive = 5f;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject panelColorDrugs;


    [SerializeField]
    private float restAmount = 0.125f;
    private bool restLife;

    [SerializeField]
    private Camera mainCamera;
    public bool makeRipple = false;
    public bool changeColorPanelCocaine = false;
    public bool changeColorPanelHash = false;
    public bool changeColorPanelMeth = false;
    public bool changeColorPanelSpeed = false;

    //DASH VARAIABLES
    [SerializeField]
    float speed, delay = 0.05f, delayPress;
    [HideInInspector]
    [SerializeField]
    private bool startDelay;
    private Rigidbody2D rb;
    private int rightPress, leftPress, upPress, downPress;
    private float timePassed, timePassedPress;
    private bool startTimer;

    //Dash de Guillem
    private bool canDash = true;
    private float dashCD = 2f;
    private bool dashInput;
    private RaycastHit2D hit;
    [SerializeField]
    private LayerMask groundLayer;
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
        if (makeRipple)
        {
            mainCamera.GetComponent<RipplePostProcessor>().MakeRipple();

        }

        if (changeColorPanelCocaine)
        {
            panelColorDrugs.GetComponent<ChangeColorPanel>().ChangeColorCocaine();

        }

        else if (changeColorPanelSpeed)
        {
            panelColorDrugs.GetComponent<ChangeColorPanel>().ChangeColorSpeed();

        }


        else if (changeColorPanelMeth)
        {
            panelColorDrugs.GetComponent<ChangeColorPanel>().ChangeColorMeth();

        }


        else if (changeColorPanelHash)
        {
            panelColorDrugs.GetComponent<ChangeColorPanel>().ChangeColorHash();

        }
        else
            panelColorDrugs.GetComponent<ChangeColorPanel>().SetActivePanelFalse();


        if (Input.GetAxis("Cocaina") != 0 && !cocaineActive)
        {
            cocaineActive = true;
            StartCoroutine(cocaineAnim());
            restLife = true;
            canvas.GetComponent<PlayerManager>().RestAmount(restAmount);
        }

        if (Input.GetAxis("Hash") != 0 && !hashActive)
        {

            hashActive = true;
            StartCoroutine(hashAnim());
            restLife = true;
            canvas.GetComponent<PlayerManager>().RestAmount(restAmount);
        }

        if (Input.GetAxis("Speed") != 0 && !speedActive)
        {
            speedActive = true;
            StartCoroutine(speedAnim());
            restLife = true;
            canvas.GetComponent<PlayerManager>().RestAmount(restAmount);
        }

        if (Input.GetAxis("Meth") != 0 && !methActive)
        {
            methActive = true;
            StartCoroutine(MethAnim());
            restLife = true;
            canvas.GetComponent<PlayerManager>().RestAmount(restAmount);
        }

        if (cocaineActive || hashActive || methActive || speedActive)
            drugActive = true;

        else
        {
            drugActive = false;
        }

        if (restLife)
            canvas.GetComponent<PlayerManager>().RestLife(restAmount);

        if (canvas.GetComponent<PlayerManager>().GetLifeBarRest() != canvas.GetComponent<PlayerManager>().GetLifeBarRest() - restAmount)
        {
            restLife = !restLife;
        }

    }

    public IEnumerator StartSpeed()
    {
        GetComponent<playerMovement>().movementSpeed = speedDrug;
        yield return new WaitForSeconds(timeDrugActive);
        speedActive = false;
        GetComponent<playerMovement>().movementSpeed = speedNormal;
    }

    public IEnumerator speedAnim()
    {
        anim.SetBool("isCristal", true);
        makeRipple = !makeRipple;
        changeColorPanelSpeed = !changeColorPanelSpeed;
        yield return new WaitForSeconds(1.06f);
        makeRipple = !makeRipple;
        changeColorPanelSpeed = !changeColorPanelSpeed;
        anim.SetBool("isCristal", false);

        StartCoroutine(StartSpeed());
    }

    public IEnumerator StartCocaine()
    {
        GetComponent<playerMovement>().jumpVel = cocaineJump;
        yield return new WaitForSeconds(timeDrugActive);
        GetComponent<playerMovement>().jumpVel = jumpNormal;
        cocaineActive = false;
    }

    public IEnumerator cocaineAnim()
    {
        anim.SetBool("isCocaine", true);
        makeRipple = !makeRipple;
        changeColorPanelCocaine = !changeColorPanelCocaine;
        yield return new WaitForSeconds(1f);
        anim.SetBool("isCocaine", false);
        makeRipple = !makeRipple;
        changeColorPanelCocaine = !changeColorPanelCocaine;

        StartCoroutine(StartCocaine());
    }

    public IEnumerator StartHash()
    {
        //RALENTIZAR LA VELOCIDAD DE LOS OBJETOS EXCEPTO LA DEL JUGADOR
        yield return new WaitForSeconds(timeDrugActive);
        //NORMALIZAR LA VELOCIDAD LOS OBJETOS 

        hashActive = false;
    }

    public IEnumerator hashAnim()
    {
        anim.SetBool("isSmoking", true);
        makeRipple = !makeRipple;
        changeColorPanelHash = !changeColorPanelHash;
        yield return new WaitForSeconds(2.6f);
        makeRipple = !makeRipple;
        changeColorPanelHash = !changeColorPanelHash;
        anim.SetBool("isSmoking", false);

        StartCoroutine(StartHash());
    }

    public IEnumerator StartMeth()
    {
        dashMeth();
        yield return new WaitForSeconds(methDelay);
        methActive = false;
    }

    public IEnumerator MethAnim()
    {
        anim.SetBool("isCristal", true);
        makeRipple = !makeRipple;
        changeColorPanelMeth = !changeColorPanelMeth;
        yield return new WaitForSeconds(.1f);
        makeRipple = !makeRipple;
        changeColorPanelMeth = !changeColorPanelMeth;
        anim.SetBool("isCristal", false);
        StartCoroutine(StartMeth());
    }

    void dashMeth()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        if (direction != Vector3.zero)
        {
            detectWallDash(direction);
        }
    }

    void detectWallDash(Vector3 dir)
    {
        hit = Physics2D.Raycast(transform.position + dir * 10f, dir, 0.01f, groundLayer);
        if (hit.collider == null)
        {
            Vector3 offset = new Vector3(transform.position.x + 10f * dir.x, transform.position.y, transform.position.z);
            transform.position = offset;
        }
    }

    /*public IEnumerator makeRippleeffect()
    {
        yield return new WaitForSeconds(0.2f);
        makeRipple = !makeRipple;


    }*/

}