using System.Collections;
using System.Collections.Generic;
using System.IO;
using Temp;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LoadingScene : MonoBehaviour
{
    string path;
    [SerializeField] AudioSource audioSource;
    void Awake()
    {
        path = Path.Combine(Application.persistentDataPath, "data.json");
        JsonLoad();
    }
    public void JsonLoad()
    {
        SaveData saveData = new SaveData();

        if (File.Exists(path))
        {
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            if (saveData != null)
            {
                audioSource.volume = saveData.BackgroundSoundValue / 4f;
            }
        }
    }
}
