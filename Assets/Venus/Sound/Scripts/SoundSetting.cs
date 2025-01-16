using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Temp
{
    public class SoundSetting : Sound
    {
        void Start()
        {
            GameObject OBJ2 = GameObject.Find("GameSoundToss");
            if (OBJ2 != null)
            {
                int V = Mathf.RoundToInt(OBJ2.transform.position.x);
                GameSoundSlider.value = V;
                Destroy(OBJ2);
            }
            GameObject OBJ3 = GameObject.Find("EffectSoundToss");
            if (OBJ3 != null)
            {
                int V = Mathf.RoundToInt(OBJ3.transform.position.x);
                EffectSoundSlider.value = V;
                Destroy(OBJ3);
            }
        }
        public void SettingOn(GameObject OBJ)
        {
            Time.timeScale = 0;
            OBJ.SetActive(true);
        }
        public void SettingOff(GameObject OBJ)
        {
            Time.timeScale = 1;
            OBJ.SetActive(false);
            if (mainSceneManager != null)
            {
                mainSceneManager.JsonSave();
            }
        }
        public void GameSoundChange(Slider slider)
        {
            if (mainSceneManager != null)
            {
                mainSceneManager.SaveData.GameSoundValue = Mathf.RoundToInt(slider.value);
            }
            if (GameAudio != null)
                GameAudio.volume = slider.value / 4f;
        }
        public void EffectSoundChange(Slider slider)
        {
            if (mainSceneManager != null)
            {
                mainSceneManager.SaveData.EffectSoundValue = Mathf.RoundToInt(slider.value);
            }
            if (EffectAudio != null)
                EffectAudio.volume = slider.value / 4f;
        }
        public void BackgroundSoundChange(Slider slider)
        {
            if (mainSceneManager != null)
            {
                mainSceneManager.SaveData.BackgroundSoundValue = Mathf.RoundToInt(slider.value);
            }
            if (BackgroundAudio != null)
                BackgroundAudio.volume = slider.value / 4f;
        }
    }
}
