using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public int patience;
    public GameObject patiencebarcontrol;
    public List<Menu> orders;

    public void Start()
    {
        var rng = Random.Range(0, 10);
        if(rng > 5)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        StartCoroutine(StartPatienceGauge());
    }

    public IEnumerator StartPatienceGauge()
    {
        var patiencedummy = patience;
        while(patiencedummy > 0)
        {
            patiencedummy--;
            patiencebarcontrol.transform.localScale =new Vector3(patiencebarcontrol.transform.localScale.x , (float)patiencedummy / (float)patience);
            yield return new WaitForSeconds(1f);
        }
        var cookingmanager = GameObject.FindGameObjectWithTag("CookingManager");
        cookingmanager.GetComponent<CookingManager>().RemoveCustomer(gameObject);
        Destroy(gameObject);
        //REMOVE CUSTOMER;


        yield return null;
    }

    public void RemoveOrder(Menu food)
    {
        orders.Remove(food);
    }
}
