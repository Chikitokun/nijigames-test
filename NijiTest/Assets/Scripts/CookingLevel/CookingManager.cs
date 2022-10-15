using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CookingManager : MonoBehaviour
{
    public Levels leveldifficulty;
    public GameObject customerprefab;
    public List<CustomerSlot> customers;
    public GameObject resultUI;
    public TextMeshProUGUI goldtext;
    public TextMeshProUGUI servedcustomertext;
    public SceneField mainmenu;

    public AudioClip customerin;
    public AudioClip monipay;
    public AudioClip winlevel;

    public int leveltimer;
    public int levelcustomer;
    [SerializeField] private int targetcustomerserved;
    [SerializeField] private int customerserved;
    
    public void Start()
    {
    }

    public void StartLevel()
    {
        StartCoroutine(StartLevelRoutine());
    }

    public IEnumerator StartLevelRoutine()
    {
        levelcustomer = leveldifficulty.customer;
        foreach(WinCondition wc in leveldifficulty.goals)
        {
            if(wc.type == WinCondition.GoalType.timer)
            {
                leveltimer = wc.amount;
            }
            else if(wc.type == WinCondition.GoalType.customer)
            {
                targetcustomerserved = wc.amount;
            }
            else { /* IN CASE OF EXTRA WIN CONDITION */ }
        }

        var customercoming = leveldifficulty.timebetweencustomer;
        while (leveltimer > 0) 
        {
            customercoming--;
            if(customercoming == 0 && levelcustomer > 0)
            {
                AddCustomer();
                customercoming = leveldifficulty.timebetweencustomer;
            }
            leveltimer--;
            if (levelcustomer == 0 && NoCustomer() == true)
            {
                //END LEVEL
                EndLevel();
                yield return null;
            }
            yield return new WaitForSeconds(1f);
        }
        //END LEVEL
        EndLevel();
        yield return null;
    }

    public void EndLevel()
    {
        foreach (WinCondition wc in leveldifficulty.goals)
        {
            if (wc.type == WinCondition.GoalType.customer)
            {
                if(customerserved >= targetcustomerserved)
                {
                    //WIN
                    GameManager.Instance.UnlockLevel(leveldifficulty.unlocks);
                    GameManager.Instance.PlayEffectAudio(winlevel, gameObject);
                    Debug.Log("you won this level btw, next level is now unlocked");
                }
                else
                {
                    //LOSE
                    Debug.Log("you lose this level btw, next level is still locked");
                }
                StopAllCoroutines();
                resultUI.SetActive(true);
                servedcustomertext.text = customerserved.ToString();
                goldtext.text = GameManager.Instance.playerdata.moni.ToString();
            }
            else { /* IN CASE OF EXTRA WIN CONDITION */ }
        }
    }

    public void RetryLevel(bool easy)
    {
        SceneField sf = new SceneField();
        sf.SetScene(SceneManager.GetActiveScene());
        if (easy == true)
        {

            Levels easierlvl = ScriptableObject.CreateInstance<Levels>();
            easierlvl.Init(leveldifficulty);
            easierlvl.customer += 5;
            GameManager.Instance.ReloadLevel(sf, easierlvl);
        }
        else
        {
            GameManager.Instance.ReloadLevel(sf,leveldifficulty);
        }
    }

    public void ReturnToMainmenu()
    {
        GameManager.Instance.LoadNewScene(mainmenu);
    }

    public bool ServeFood(Menu food)
    {
        foreach(CustomerSlot c in customers)
        {
            if(c.customer != null)
            {
                foreach (Menu order in c.customer.GetComponent<Customer>().orders)
                {
                    if (food == order)
                    {
                        c.customer.GetComponent<Customer>().RemoveOrder(food);
                        if (c.customer.GetComponent<Customer>().orders.Count == 0)
                        {
                            Destroy(c.customer);
                            RemoveCustomer(c.customer);
                            customerserved++;
                        }
                        GameManager.Instance.PlayEffectAudio(monipay, gameObject);
                        GameManager.Instance.playerdata.moni += 5;
                        //GAIN MONEY
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void AddCustomer()
    {
        levelcustomer--;
        var slot = FindEmptySlot();
        if (slot.position != null)
        {
            GameManager.Instance.PlayEffectAudio(customerin, gameObject);
            var cus = Instantiate(customerprefab, slot.position.transform.position , Quaternion.identity);
            slot.customer = cus;
            cus.GetComponent<Customer>().orders = GenerateOrder();
        }
        else
        {
            Debug.Log("full slot, attempt on inserting customer");
            //MISSED CUSTOMER, MAKES LEVEL HARDER HUEHUEHUEUHE
        }
    }

    public CustomerSlot FindEmptySlot()
    {
        foreach (CustomerSlot c in customers)
        {
            if (c.CheckEmpty() == true)
            {
                return c;
            }
        }
        return new CustomerSlot();
    }

    public void RemoveCustomer(GameObject customer)
    {
        foreach(CustomerSlot c in customers)
        {
            if(c.customer == customer)
            {
                c.EmptySlot();
            }
        }
    }

    public List<Menu> GenerateOrder()
    {
        var orders = new List<Menu>();
        var rng = Random.Range(0, 10);
        var iteration = 0;
        if(rng > 5)
        {
            iteration = 1;
        }
        else
        {
            iteration = 2;
        }

        for(int a = 0; a != iteration; a++)
        {
            orders.Add(leveldifficulty.orders[Random.Range(0, leveldifficulty.orders.Count)]);
        }
        return orders;
    }

    public bool NoCustomer()
    {
        foreach (CustomerSlot c in customers)
        {
            if (c.CheckEmpty() == false)
            {
                return false;
            }
        }
        return true;
    }

}

[System.Serializable]
public class CustomerSlot
{
    public GameObject customer;
    public GameObject position;

    public void EmptySlot()
    {
        customer = null;
    }

    public bool CheckEmpty()
    {
        if (customer == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
