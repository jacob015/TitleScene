using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
namespace ColorMatch
{
    public class SoundSet : MonoBehaviour
    {
        public AudioMixer mixer;

        public float defaultBGMVolume = 1;
        public float defaultSFXVolume = 1;

        void Awake()
        {
            defaultBGMVolume = PlayerPrefs.GetFloat("BGMVolume", defaultBGMVolume);
            defaultSFXVolume = PlayerPrefs.GetFloat("SFXVolume", defaultSFXVolume);
            mixer.SetFloat("BGM", Mathf.Log10(defaultBGMVolume) * 20);
            mixer.SetFloat("SFX", Mathf.Log10(defaultSFXVolume) * 20);
        }
        public void SetBGMVolume(float volume)
        {
            mixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("BGMVolume", volume);
        }
        public void SetSFXVolume(float volume)
        {
            mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }
    }
}