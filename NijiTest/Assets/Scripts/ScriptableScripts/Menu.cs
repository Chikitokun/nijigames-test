using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Menu : ScriptableObject
{ 
    public enum Type
    {
        food,
        drink,
    }
    public Type menutype;
    public Sprite menusprite;
}
