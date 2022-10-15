using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Sate : Menu
{
    public enum Sauce
    {
        plain,
        kecap,
        kacang,
    }
    public Sauce saus;
}
