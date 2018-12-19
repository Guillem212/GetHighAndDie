using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallDamage : MonoBehaviour
{
    [SerializeField]private float minFallTime = 1.5f, amountFallDamage = 0.25f;
    public float currentFallTime = 0f;
    [SerializeField] private GameObject glassTexture, canvas;
    private float timer = 0f;
    private float timeToWait = 1f;//Lo que tarda en hacer las animaciones y el personaje esta suspendido en el aire.
    
   
    void Update()
    {
        //Para evitar que cuando uses una droga en el aire siga contando el tiempo que el personaje esta en el aire:
        if ((GetComponent<DrugsMechanics>().GetSpeedActive() || GetComponent<DrugsMechanics>().GetCocaineActive()) && timer < timeToWait)
            timer += Time.deltaTime;

        if (timer < timeToWait)
            currentFallTime = currentFallTime;



        if ( (timer > timeToWait || timer == 0f) && !GetComponent<playerMovement>().detectGround() && (!GetComponent<DrugsMechanics>().GetSpeedActive() || !GetComponent<DrugsMechanics>().GetCocaineActive()))
        {
            currentFallTime += Time.deltaTime;

        }

        else
        {
            if (currentFallTime >= minFallTime)
            {
                canvas.GetComponent<PlayerManager>().RestAmount(amountFallDamage);
                canvas.GetComponent<PlayerManager>().RestLife();
                currentFallTime = 0f;
                StartCoroutine(FadeTexture(0.0f, 4.0f));                     
            }
        }

        if (GetComponent<playerMovement>().detectGround())
        {
            StartCoroutine(ResetCurrentFallTime());
            timer = 0f;
        }

        if (GetComponent<playerMovement>().GetIsOnWall() || GetComponent<DrugsMechanics>().GetHashActive())
            currentFallTime = 0f;
    }

    public IEnumerator ResetCurrentFallTime()
    {
        yield return new WaitForSeconds(0.1f);
            currentFallTime = 0f;
    }

    public IEnumerator FadeTexture(float aValue, float aTime)
    {
        float alpha = 1;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 0, 0, Mathf.Lerp(alpha, aValue, t));
            glassTexture.GetComponent<Image>().color = newColor;
            yield return null;
        }
    } 
}