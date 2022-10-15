using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeaPrep : MonoBehaviour
{
    public Menu teh;
    public GameObject radialbar;
    public GameObject tehgelas;
    public float cooktime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartPreppingTea());   
    }

    public IEnumerator StartPreppingTea()
    {
        float cooktimedummy = 0;
        radialbar.SetActive(true);

        while (cooktimedummy < cooktime)
        {
            cooktimedummy += 0.1f;
            radialbar.GetComponent<Image>().fillAmount = (float)cooktimedummy / (float)cooktime;
            yield return new WaitForSeconds(0.1f);
        }
        radialbar.SetActive(false);
        tehgelas.SetActive(true);
        yield return null;   
    }

    public void OnClickServeTea()
    {
        var cookmanager = GameObject.FindGameObjectWithTag("CookingManager").GetComponent<CookingManager>();

        if (tehgelas.activeSelf == true)
        {
            //Serve
            if (cookmanager.ServeFood(teh) == true)
            {
                tehgelas.SetActive(false);
                StartCoroutine(StartPreppingTea());
            }
        }
    }
}
