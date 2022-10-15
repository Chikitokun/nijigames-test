using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public Levels leveldata;
    public SceneField scene;
    public string levelunlockcode;
    public LevelType buttontype;
    public string nextunlocks;

    public AudioClip clicksfx;

    public enum LevelType
    {
        debug,
        gameplay,
    }

    public void Start()
    {
        if(string.IsNullOrEmpty(levelunlockcode) == false)
        {
            if (GameManager.Instance.playerdata.levels[levelunlockcode] == true)
            {
            }
            else
            {
                gameObject.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void OnClick()
    {
        if(clicksfx != null)
        {
            GameManager.Instance.PlayEffectAudio(clicksfx, gameObject);
        }

        if(buttontype == LevelType.debug)
        {
            Debug.Log("Done");
            GameManager.Instance.UnlockLevel(nextunlocks);
            SceneField scfield = new SceneField();
            scfield.SetScene(SceneManager.GetActiveScene());
            GameManager.Instance.LoadNewScene(scfield);
        }
        else
        {
            GameManager.Instance.LoadNewLevel(scene, leveldata);
        }
    }
}
