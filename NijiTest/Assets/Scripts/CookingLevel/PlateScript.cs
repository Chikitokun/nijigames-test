using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateScript : MonoBehaviour
{
    [SerializeField] private Menu food;
    public GameObject foodobj;

    public void Start()
    {
    }

    public void Serve()
    {
        var cookmanager = GameObject.FindGameObjectWithTag("CookingManager").GetComponent<CookingManager>();
        if (cookmanager.ServeFood(food) == true)
        {
            foodobj.SetActive(false);
            food = null;
        }
    }

    public void SetFood(Menu f)
    {
        if(f != null)
        {
            foodobj.SetActive(true);
            food = f; 
            if (f.GetType() == typeof(Sate))
            {
                var satef = f as Sate;
                foodobj.GetComponent<Image>().sprite = f.menusprite;
            }
        }
    }

    public Menu GetFood()
    {
        if(food == null)
        {
            return null;
        }
        else
        {
        return food;
        }
    }

    public void Trashfood()
    {
        food = null;
        foodobj.SetActive(false);
    }
}
