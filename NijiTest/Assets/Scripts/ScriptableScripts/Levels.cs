using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Levels : ScriptableObject
{
    public int customer;
    public int timebetweencustomer;
    public string unlocks;

    public List<Menu> orders;
    public List<WinCondition> goals;

    public void Init(Levels lvl)
    {
        customer = lvl.customer;
        timebetweencustomer = lvl.timebetweencustomer;
        unlocks = lvl.unlocks;

        orders = new List<Menu>(lvl.orders);
        goals = new List<WinCondition>(lvl.goals);
    }
}

[System.Serializable]
public class WinCondition
{
    public enum GoalType
    {
        timer,
        customer,
    }

    public GoalType type;
    public int amount;
}

