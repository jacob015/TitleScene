using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Temp
{
    [System.Serializable]
    public class SaveData
    {
        public int Score;
        public int GameSoundValue;
        public int EffectSoundValue;
        public int BackgroundSoundValue;
    }
    public class MainSceneManager : MonoBehaviour
    {
        [SerializeField] Slider GameSoundSlider;
        [SerializeField] Slider EffectSoundSlider;
        [SerializeField] Slider BackgroundSoundSlider;

        public int Score = 0;

        public SaveData SaveData = new SaveData();

        string path;
        void Awake()
        {
            path = Path.Combine(Application.persistentDataPath, "data.json");
            JsonLoad();
        }
        void Start()
        {
            GameObject OBJ = GameObject.Find("ScoreToss");
            if (OBJ != null)
            {
                int s = Mathf.RoundToInt(OBJ.transform.position.x);
                if (s > Score)
                    Score = s;
                Destroy(OBJ);
                JsonSave();
            }
        }
        void OnDisable()
        {
            StopAllCoroutines();
        }
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
        public void GameStart()
        {
            GameObject obj1 = new GameObject("PlayerBestScoreToss");
            obj1.transform.position = new Vector3(Score, 0, 0);
            DontDestroyOnLoad(obj1);
            GameObject obj2 = new GameObject("GameSoundToss");
            obj2.transform.position = new Vector3(SaveData.GameSoundValue, 0, 0);
            DontDestroyOnLoad(obj2);
            GameObject obj3 = new GameObject("EffectSoundToss");
            obj3.transform.position = new Vector3(SaveData.EffectSoundValue, 0, 0);
            DontDestroyOnLoad(obj3);
            Time.timeScale = 1;
            SceneManager.LoadScene("TempGame");
        }
        public void JsonLoad()
        {
            SaveData saveData = new SaveData();

            if (!File.Exists(path))
            {
                saveData.Score = 0;
                saveData.BackgroundSoundValue = 2;
                saveData.EffectSoundValue = 2;
                saveData.GameSoundValue = 2;
            }
            else
            {
                string loadJson = File.ReadAllText(path);
                saveData = JsonUtility.FromJson<SaveData>(loadJson);

                if (saveData != null)
                {
                    SaveData.Score = saveData.Score;
                    Score = saveData.Score;
                    SaveData.BackgroundSoundValue = saveData.BackgroundSoundValue;
                    SaveData.EffectSoundValue = saveData.EffectSoundValue;
                    SaveData.GameSoundValue = saveData.GameSoundValue;
                    GameSoundSlider.value = saveData.GameSoundValue;
                    EffectSoundSlider.value = saveData.EffectSoundValue;
                    BackgroundSoundSlider.value = saveData.BackgroundSoundValue;
                }
            }
        }
        public void JsonSave()
        {
            SaveData saveData = new SaveData();

            saveData.BackgroundSoundValue = SaveData.BackgroundSoundValue;
            saveData.GameSoundValue = SaveData.GameSoundValue;
            saveData.EffectSoundValue = SaveData.EffectSoundValue;

            string json = JsonUtility.ToJson(saveData, true);

            File.WriteAllText(path, json);
        }
    }
}
