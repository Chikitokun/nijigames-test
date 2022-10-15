using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SauceScript : MonoBehaviour
{
    public Sate newfood;
    public List<GameObject> plates;

    public void SauceUp()
    {
        foreach(GameObject plate in plates)
        {
            if (plate.GetComponent<PlateScript>().GetFood() != null)
            {
                if (plate.GetComponent<PlateScript>().GetFood().GetType() == typeof(Sate))
                {
                    var saucecheck = plate.GetComponent<PlateScript>().GetFood() as Sate;
                    if(saucecheck.saus == Sate.Sauce.plain)
                    {
                        plate.GetComponent<PlateScript>().SetFood(newfood);
                        return;
                    }
                }
            }
        }
        Debug.Log("nothing to sauce");
    }

}
