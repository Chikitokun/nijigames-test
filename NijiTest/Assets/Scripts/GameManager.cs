using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null; // the private static singleton instance variable
    public static GameManager Instance { get { return instance; } } // public getter property, anyone can access it!

    public PlayerData playerdata;
    public float effectaudio;
    public string path;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        //LOAD PLAYER DATA HERE-------------------
        //CHECK FOR CONSOLE TYPE AND ADJUST SAVING LOCATION ACCORDINGLY
        path = Application.persistentDataPath + "/save.data";

        if (!File.Exists(path))
        {
            playerdata = new PlayerData();
            Debug.Log("savingdata-new data creation");

            PlayerData savedata = new PlayerData(playerdata.levels, playerdata.moni);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            formatter.Serialize(stream, savedata);
            stream.Close();
            DataLoad();
        }
        else
        {
            DataLoad();
        }

        DontDestroyOnLoad(gameObject);
        if (instance == null)
        { // if the singleton instance has not yet been initialized
            instance = this;
        }
        else
        { // the singleton instance has already been initialized
            if (instance != this)
            { // if this instance of GameManager is not the same as the initialized singleton instance, it is a second instance, so it must be destroyed!
                Destroy(gameObject); 
            }
        }
    }

    public void Start()
    {
        StartCoroutine(AutoSaveEvery20());
    }

    public IEnumerator AutoSaveEvery20()
    {
        while(1 != 0)
        {
            DataSave();
            yield return new WaitForSeconds(20f);
        }
    }

    public void DataSave()
    {
        Debug.Log("savingdata");

        PlayerData savedata = new PlayerData(playerdata.levels, playerdata.moni);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
        formatter.Serialize(stream, savedata);
        stream.Close();
    }

    public void DataLoad()
    {
        Debug.Log("loadingdata");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            PlayerData loaddata = formatter.Deserialize(stream) as PlayerData;
            playerdata.levels = loaddata.levels;
            playerdata.moni = loaddata.moni;
            stream.Close();
        }
        else
        {
            Debug.Log("no data found");
        }
    }

    public void LoadNewScene(SceneField newscene)
    {
        StartCoroutine(LoadNewSceneRoutine(newscene));
    }

    public IEnumerator LoadNewSceneRoutine(SceneField newscene)
    {
        yield return new WaitForSecondsRealtime(0.4f);
        SceneManager.LoadScene(newscene);
        yield return new WaitForSecondsRealtime(0.2f);
        DataSave();
    }

    public void LoadNewLevel(SceneField newscene, Levels leveldata)
    {
        StartCoroutine(LoadNewLevelRoutine(newscene, leveldata));
        DataSave();
    }

    public void ReloadLevel(SceneField newscene, Levels leveldata)
    {
        StartCoroutine(ReloadLevelRoutine(newscene, leveldata));
        DataSave();
    }

    public IEnumerator LoadNewLevelRoutine(SceneField newscene,Levels leveldata)
    {
        yield return new WaitForSecondsRealtime(0.3f);
        var curidx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(newscene);
        yield return new WaitForSecondsRealtime(0.3f);
        while (SceneManager.GetActiveScene().buildIndex == curidx)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSecondsRealtime(0.1f);
        var cookingmanager = GameObject.FindGameObjectWithTag("CookingManager");
        cookingmanager.GetComponent<CookingManager>().leveldifficulty = leveldata;
        cookingmanager.GetComponent<CookingManager>().StartLevel();
        yield return new WaitForSecondsRealtime(0.2f);
    }

    public IEnumerator ReloadLevelRoutine(SceneField newscene, Levels leveldata)
    {
        yield return new WaitForSecondsRealtime(0.3f);
        var curidx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(newscene);
        yield return new WaitForSecondsRealtime(0.1f);
        var cookingmanager = GameObject.FindGameObjectWithTag("CookingManager");
        cookingmanager.GetComponent<CookingManager>().leveldifficulty = leveldata;
        cookingmanager.GetComponent<CookingManager>().StartLevel();
        yield return new WaitForSecondsRealtime(0.2f);
    }

    public void PlayEffectAudio(AudioClip sound, GameObject caller)
    {
            if (sound == null)
            {
                return;
            }
            else
            {
                StartCoroutine(PlayEffectAudioRoutine(sound, caller));
            }
    }

    public IEnumerator PlayEffectAudioRoutine(AudioClip sound, GameObject caller)
    {
        var s = gameObject.AddComponent<AudioSource>();
        s.clip = sound;
        s.volume = effectaudio;
        s.Play();
        yield return new WaitForSeconds(sound.length + 0.5f);
        Destroy(s);
        yield return null;
    }

    public void UnlockLevel(string levelstring)
    {
        playerdata.levels[levelstring] = true;
    }
}
