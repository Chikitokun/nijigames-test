using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeatCooking : MonoBehaviour
{
    public Menu foodcooked;
    public List<GameObject> plates;
    public GameObject radialbar;
    public MeatState state;
    public float cooktime;

    public AudioClip meatsizzle;

    public Sprite cooked;
    public Sprite dead;
    public Sprite raw;
    public enum MeatState
    {
        raw,
        cooked,
        dead,
    }

    public void FixedUpdate()
    {
        UpdateSprite();
    }

    public void OnEnable()
    {
        state = MeatState.raw;
        radialbar.SetActive(true);
        StartCoroutine(StartCooking(state));
    }

    public IEnumerator StartCooking(MeatState curstate)
    {
        float cooktimedummy = 0;
        while(cooktimedummy < cooktime)
        {
            cooktimedummy += 0.1f;
            radialbar.GetComponent<Image>().fillAmount = (float)cooktimedummy / (float)cooktime;
            yield return new WaitForSeconds(0.1f);
        }

        state++;
        GameManager.Instance.PlayEffectAudio(meatsizzle,gameObject);

        if (state != MeatState.dead)
        {
            StartCoroutine(StartCooking(state));
        }
        else
        {
            radialbar.SetActive(false);
            yield return null;
        }
    }

    public void OnClickPlateMeat()
    {
        if(state == MeatState.raw)
        {
            return;
        }
        else if(state == MeatState.cooked)
        {
            if (PlateSatay() == true)
            {
                StopAllCoroutines();
            }
        }
        else
        {
            state = MeatState.raw;
            gameObject.SetActive(false);
            //play trashcan audio
        }
    }

    public bool PlateSatay()
    {
        foreach (GameObject plate in plates)
        {
            Debug.Log(plate);
            Debug.Log(plate.GetComponent<PlateScript>().GetFood());
            if (plate.GetComponent<PlateScript>().GetFood() == null)
            {
                gameObject.SetActive(false);
                plate.GetComponent<PlateScript>().SetFood(foodcooked);
                return true;
            }
        }
        return false;
    }

    public void UpdateSprite()
    {
        if(state == MeatState.cooked)
        {
            gameObject.GetComponent<Image>().sprite = cooked;
        }
        else if (state == MeatState.dead)
        {
            gameObject.GetComponent<Image>().sprite = dead;
        }
        else 
        {
            gameObject.GetComponent<Image>().sprite = raw;
        }
    }

}
