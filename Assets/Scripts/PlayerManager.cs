using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private Image LifeBar;
    [SerializeField]
    private Image LifeBarRest;

    [SerializeField]
    private GameObject player;

    private float CocaineFillAmount;
    private float HashFillAmount;
    private float SpeedFillAmount;
    private float MethFillAmount;

    [SerializeField]
    private bool restarVida = false;
    [SerializeField]
    private float amount = 1f;

    [SerializeField]
    private bool restLifeDeltaTime = false;

    [SerializeField]
    private int restLifeRateDeltaTimeGreen = 30;

    [SerializeField]
    private int restLifeRateDeltaTimeOrange = 25;

    [SerializeField]
    private int restLifeRate = 10;

    // Start is called before the first frame update
    void Start()
    {

        CocaineFillAmount = CocaineIcon.fillAmount;
        HashFillAmount = HashIcon.fillAmount;
        SpeedFillAmount = SpeedIcon.fillAmount;
        MethFillAmount = MethIcon.fillAmount;

    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<DrugsMechanics>().cocaineActive)
            StartCoroutine(UpdateCocaineAmount());
        else CocaineFillAmount = 1f;

        if (player.GetComponent<DrugsMechanics>().hashActive)
            StartCoroutine(UpdateHashAmount());
        else HashFillAmount = 1f;

        if (player.GetComponent<DrugsMechanics>().speedActive)
            StartCoroutine(UpdateSpeedAmount());
        else SpeedFillAmount = 1f;

        RestLifeDeltaTime();
    }

    public IEnumerator UpdateCocaineAmount()
    {

        if (CocaineFillAmount > 0)
        {
            yield return new WaitForSeconds(1f); // Tiempo que tarda en hacer la animación
            CocaineFillAmount -= 1.0f / player.GetComponent<DrugsMechanics>().timeDrugActive * Time.deltaTime;
            CocaineIcon.fillAmount = CocaineFillAmount;

        }

    }

    public IEnumerator UpdateHashAmount()
    {

        if (HashFillAmount > 0)
        {
            yield return new WaitForSeconds(2.6f); // Tiempo que tarda en hacer la animación
            HashFillAmount -= 1.0f / player.GetComponent<DrugsMechanics>().timeDrugActive * Time.deltaTime;
            HashIcon.fillAmount = HashFillAmount;

        }

    }

    public IEnumerator UpdateSpeedAmount()
    {

        if (SpeedFillAmount > 0)
        {
            yield return new WaitForSeconds(1.06f); // Tiempo que tarda en hacer la animación
            SpeedFillAmount -= 1.0f / player.GetComponent<DrugsMechanics>().timeDrugActive * Time.deltaTime;
            SpeedIcon.fillAmount = SpeedFillAmount;

        }

    }

    public void RestAmount(float Restamount)
    {
        if (amount > 0)
        {
            amount -= Restamount;
            LifeBar.fillAmount -= Restamount;
        }
    }

    public void RestLife(float amountToRest)
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

