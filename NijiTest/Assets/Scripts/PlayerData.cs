using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public Dictionary<string, bool> levels;
    public int moni;

    public PlayerData(Dictionary<string, bool> levelf , int monif)
    {
        levels = levelf;
        moni = monif;
    }

    public PlayerData()
    {
        levels = new Dictionary<string, bool>();
        //DICTIONARY HERE
        #region //DICTIONARY OF LEVELS
        levels.Add("1-1", false);
        levels.Add("1-2", false);
        levels.Add("1-3", false);
        levels.Add("2-1", false);
        levels.Add("2-2", false);
        levels.Add("2-3", false);
        #endregion
    }
}
