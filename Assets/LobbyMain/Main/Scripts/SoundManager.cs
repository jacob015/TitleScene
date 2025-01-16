using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private AudioSource _bgmPlayer;
    private AudioSource _sfxPlayer;
    public Slider bgmSlider;
    public Slider sfxSlider;
    
    private void Awake()
    {
        _bgmPlayer = GameObject.Find("BGM Player").GetComponent<AudioSource>();
        _sfxPlayer = GameObject.Find("SFX Player").GetComponent<AudioSource>();

        bgmSlider = bgmSlider.GetComponent<Slider>();
        sfxSlider = sfxSlider.GetComponent<Slider>();
        
        bgmSlider.onValueChanged.AddListener(ChangeBgmSound);
        sfxSlider.onValueChanged.AddListener(ChangeSfxSound);
    }
    
    void ChangeBgmSound(float value)
    {
        _bgmPlayer.volume = value;
    }
    
    void ChangeSfxSound(float value)
    {
        _sfxPlayer.volume = value;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
