using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerShowOder : MonoBehaviour
{
    public GameObject customer;

    public GameObject orderslot1;
    public GameObject orderslot2;
    public GameObject orderslot3;

    public Customer customerscript;

    public void Awake()
    {
        customerscript = customer.GetComponent<Customer>();
    }

    public void FixedUpdate()
    {
        if (customerscript.orders.Count > 1)
        {
            orderslot1.SetActive(true);
            orderslot2.SetActive(true);
            orderslot3.SetActive(false);
            orderslot1.GetComponent<Image>().sprite = customerscript.orders[0].menusprite;
            orderslot2.GetComponent<Image>().sprite = customerscript.orders[1].menusprite;
        }
        else
        {
            orderslot1.SetActive(false);
            orderslot2.SetActive(false);
            orderslot3.SetActive(true);
            orderslot3.GetComponent<Image>().sprite = customerscript.orders[0].menusprite;
        }
    }
}
