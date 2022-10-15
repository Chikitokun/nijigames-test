using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldTimeCustomer : MonoBehaviour
{
    public GameObject cookingmanager;
    public TextMeshProUGUI gold;
    public TextMeshProUGUI customerleft;
    public TextMeshProUGUI time;
    // Update is called once per frame
    void FixedUpdate()
    {
        gold.text = GameManager.Instance.playerdata.moni.ToString();
        customerleft.text = cookingmanager.GetComponent<CookingManager>().levelcustomer.ToString();
        time.text = cookingmanager.GetComponent<CookingManager>().leveltimer.ToString();
    }
}
