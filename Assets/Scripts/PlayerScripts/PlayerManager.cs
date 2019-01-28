using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayerManager : MonoBehaviour
{

    [SerializeField]
    private Image CocaineIcon;
    [SerializeField]
    private Image HashIcon;
    [SerializeField]
    private Image SpeedIcon;
    [SerializeField]
    private Image MethIcon;

    public  Image LifeBar;
    public  Image LifeBarRest;

    [SerializeField]
    private GameObject player;

    private float CocaineFillAmount;
    private float HashFillAmount;
    private float SpeedFillAmount;
    private float MethFillAmount;

    [SerializeField]
    private bool restarVida = false;
    [SerializeField]
    public float amount = 1f;

    [SerializeField]
    private bool restLifeDeltaTime = false;

    [SerializeField]
    private int restLifeRateDeltaTimeGreen = 30;

    [SerializeField]
    private int restLifeRateDeltaTimeOrange = 25;

    [SerializeField]
    private int restLifeRate = 10;

    private Animator anim;
    [SerializeField]
    private GameObject LifeBars;

    private int randomNumberBlink;
    private int randomNumberYawn;

    private GameObject gm;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {

        CocaineFillAmount = CocaineIcon.fillAmount;
        HashFillAmount = HashIcon.fillAmount;
        SpeedFillAmount = SpeedIcon.fillAmount;
        MethFillAmount = MethIcon.fillAmount;
        anim = LifeBars.GetComponent<Animator>();

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player.GetComponent<DrugsMechanics>().GetCocaineActive())

        {
            StartCoroutine(UpdateCocaineAmount());
            anim.SetBool("Cocaine_Consumed", true);
        }
        else

        {
            CocaineFillAmount = 1f;
            anim.SetBool("Cocaine_Consumed", false);
        }

        if (player.GetComponent<DrugsMechanics>().GetHashActive())
        {
            StartCoroutine(UpdateHashAmount());
            anim.SetBool("Hash_Consumed", true);
        }
        else
        {
            HashFillAmount = 1f;
            anim.SetBool("Hash_Consumed", false);
        }

        if (player.GetComponent<DrugsMechanics>().GetSpeedActive())
            StartCoroutine(UpdateSpeedAmount());
        else SpeedFillAmount = 1f;

        if (player.GetComponent<DrugsMechanics>().GetMethActive())
            StartCoroutine(UpdateMethAmount());
        else MethFillAmount = 1f;

        RestLifeDeltaTime();


        if (LifeBar.fillAmount <= .5f && LifeBarRest.fillAmount > .25f)
            anim.SetBool("IsTired", true);
       else anim.SetBool("IsTired", false);


        if (LifeBar.fillAmount <= .25f)
            anim.SetBool("IsDestroyed", true);
        else anim.SetBool("IsDestroyed", false);

   

        if(randomNumberBlink != 5 && randomNumberYawn != 5)
            StartCoroutine(MakeRandomBlink());

        if (randomNumberYawn != 5 && randomNumberBlink != 5)
            StartCoroutine(MakeRandomYawn());

        if (randomNumberYawn == 5 && player.GetComponent<Rigidbody2D>().velocity.x == 0f && player.GetComponent<Rigidbody2D>().velocity.y == 0f)
        {
            anim.SetBool("IsYawn", true);
            StartCoroutine(MakeYawnFalse());
        }

        if (randomNumberBlink == 5)
            anim.SetBool("Blink", true);
        else
            anim.SetBool("Blink", false);

        if (randomNumberBlink == 5 && player.GetComponent<DrugsMechanics>().GetHashActive())
            anim.SetBool("Blink_Hash", true);
        else
            anim.SetBool("Blink_Hash", false);

        if (randomNumberBlink == 5 && player.GetComponent<DrugsMechanics>().GetCocaineActive())
            anim.SetBool("Blink_Cocaine", true);
        else
            anim.SetBool("Blink_Cocaine", false);

        if (randomNumberBlink == 5 && (LifeBar.fillAmount <= .5f && LifeBarRest.fillAmount > .25f))
            anim.SetBool("Blink_Tired", true);
        else
            anim.SetBool("Blink_Tired", false);

        if (randomNumberBlink == 5 && LifeBar.fillAmount <= .25f)
            anim.SetBool("Blink_Destroyed", true);
        else
            anim.SetBool("Blink_Destroyed", false);

        if (LifeBar.fillAmount <= 0f)
        {
            //StartCoroutine(loadScene());
        }

  


    }

    public IEnumerator UpdateCocaineAmount()
    {

        if (CocaineFillAmount > 0)
        {
            yield return new WaitForSeconds(1f); // Tiempo que tarda en hacer la animación
            CocaineFillAmount -= 1.0f / player.GetComponent<DrugsMechanics>().GetTimeDrugActive() * Time.deltaTime;
            CocaineIcon.fillAmount = CocaineFillAmount;

        }

    }

    public IEnumerator UpdateHashAmount()
    {

        if (HashFillAmount > 0)
        {
            yield return new WaitForSeconds(2.6f); // Tiempo que tarda en hacer la animación
            HashFillAmount -= 1.0f / player.GetComponent<DrugsMechanics>().GetTimeDrugActive() * Time.deltaTime;
            HashIcon.fillAmount = HashFillAmount;

        }

    }

    public IEnumerator UpdateSpeedAmount()
    {

        if (SpeedFillAmount > 0)
        {
            yield return new WaitForSeconds(1.06f); // Tiempo que tarda en hacer la animación
            SpeedFillAmount -= 1.0f / player.GetComponent<DrugsMechanics>().GetTimeDrugActive() * Time.deltaTime;
            SpeedIcon.fillAmount = SpeedFillAmount;

        }

    }

    public IEnumerator UpdateMethAmount()
    {

        if (SpeedFillAmount > 0)
        {
            
            SpeedFillAmount -= 1.0f / player.GetComponent<DrugsMechanics>().GetTimeDrugActive() * Time.deltaTime;
            MethIcon.fillAmount = MethFillAmount;
            yield return new WaitForSeconds(1f);

        }

    }

    public IEnumerator MakeRandomBlink()
    {
        yield return new WaitForSeconds(1f);

        randomNumberBlink = Random.Range(0, 25);

        if (randomNumberBlink == 5 && randomNumberYawn != 5)

        randomNumberBlink = Random.Range(0, 25);


    }

    public IEnumerator MakeRandomYawn()
    {
        yield return new WaitForSeconds(3f);
        randomNumberYawn = Random.Range(0, 30);
        if(randomNumberYawn == 5 && randomNumberBlink != 5)
           randomNumberYawn = Random.Range(0, 30);


    }

    public IEnumerator MakeYawnFalse()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("IsYawn", false);

    }

    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(0.3f);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        TimeManager.DoSlowMotion(false);
    }

    public void RestAmount(float Restamount)
    {
        if (amount > 0)
        {
            amount -= Restamount;
            LifeBar.fillAmount -= Restamount;
        }
    }

    public void RestLife()
    {


        if (LifeBarRest.fillAmount > amount)
        {

            LifeBarRest.fillAmount -= Time.deltaTime / restLifeRate;
        }


    }

    public void RestLifeDeltaTime()
    {
        if (restLifeDeltaTime)
        {
            LifeBarRest.fillAmount -= Time.deltaTime / restLifeRateDeltaTimeGreen;
            LifeBar.fillAmount -= Time.deltaTime / restLifeRateDeltaTimeOrange;

        }

    }

    public float GetLifeBarRest()
    {
        return LifeBarRest.fillAmount;
    }

    public float GetLifeBar()
    {
        return LifeBar.fillAmount;
    }

    public void AddLife(float lifeToAdd)
    {
        if (LifeBar.fillAmount > lifeToAdd)
        {

            LifeBar.fillAmount += Time.deltaTime / restLifeRate;
        }
    }

    public void AddAmount(float lifeToAmount)
    {
        if (amount > 0)
        {
            amount += lifeToAmount;
            LifeBarRest.fillAmount += lifeToAmount;
        }
    }


    public float GetAmount()
    {
        return amount;
    }


}

