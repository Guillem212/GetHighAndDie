using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private Image CocaineIcon;
    [SerializeField] private Image HashIcon;
    [SerializeField] private Image SpeedIcon;
    [SerializeField] private Image MethIcon;
    [SerializeField] private Image LifeBar;
    [SerializeField] private Image LifeBarRest;

    [SerializeField]
    private GameObject player;

    private float CocaineFillAmount;
    private float HashFillAmount;
    private float SpeedFillAmount;
    private float MethFillAmount;

    [SerializeField] private bool restarVida = false;
    [SerializeField] private float amount = 1f;


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
        if (player.GetComponent<DrugsMechanicsSergio>().cocaineActive)
            StartCoroutine(UpdateCocaineAmount());
        else CocaineFillAmount = 1f;

        if (player.GetComponent<DrugsMechanicsSergio>().hashActive)
            StartCoroutine(UpdateHashAmount());
        else HashFillAmount = 1f;

        if (player.GetComponent<DrugsMechanicsSergio>().speedActive)
            StartCoroutine(UpdateSpeedAmount());
        else SpeedFillAmount = 1f;


    }

 public IEnumerator UpdateCocaineAmount()
    {

        if (CocaineFillAmount > 0)
        {
            yield return new WaitForSeconds(1f); // Tiempo que tarda en hacer la animación
            CocaineFillAmount -= 1.0f / player.GetComponent<DrugsMechanicsSergio>().timeDrugActive * Time.deltaTime;
            CocaineIcon.fillAmount = CocaineFillAmount;
          
        }

    }

    public IEnumerator UpdateHashAmount()
    {

        if (HashFillAmount > 0)
        {
            yield return new WaitForSeconds(2.6f); // Tiempo que tarda en hacer la animación
            HashFillAmount -= 1.0f / player.GetComponent<DrugsMechanicsSergio>().timeDrugActive * Time.deltaTime;
            HashIcon.fillAmount = HashFillAmount;

        }

    }

    public IEnumerator UpdateSpeedAmount()
    {

        if (SpeedFillAmount > 0)
        {
            yield return new WaitForSeconds(1.06f); // Tiempo que tarda en hacer la animación
            SpeedFillAmount -= 1.0f / player.GetComponent<DrugsMechanicsSergio>().timeDrugActive * Time.deltaTime;
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
         
            LifeBarRest.fillAmount -= Time.deltaTime / 10;
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

            LifeBar.fillAmount += Time.deltaTime / 10;
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
