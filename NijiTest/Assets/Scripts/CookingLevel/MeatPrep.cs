using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatPrep : MonoBehaviour
{
    public List<GameObject> meatcooking;

    public void OnClickPrepareMeat()
    {
        foreach(GameObject meat in meatcooking)
        {
            if(meat.activeSelf == false)
            {
                meat.SetActive(true);
                return;
            }
        }
        Debug.Log("it's full bro");
    }
}
