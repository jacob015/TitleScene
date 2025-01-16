using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMng : MonoBehaviour
{
    public static SoundMng Instance;

    public List<AudioSource> music = new List<AudioSource>();
    bool isTimer;
    bool isStop;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if(GameMng.Instance.lifeTime <= 5)
        {
            if(isTimer == false)
            {
                music.Find(x => x.name == "Timer").Play();
                isTimer = true;
            }
        }
        else
        {
            music.Find(x => x.name == "Timer").Stop();
            isTimer = false;
        }

        if(GameMng.Instance.isDead == true)
        {
            isStop = true;
            isTimer = true;
            music.Find(x => x.name == "Timer").Stop();
            music.Find(x => x.name == "DanceYellow").Stop();
            music.Find(x => x.name == "DanceGreen").Stop();
            music.Find(x => x.name == "DanceRed").Stop();
        }
    }

    public void MusicPlay(string name)
    {
        if(isStop == false)
        {
            music.Find(x => x.name == name).Play();
        }
    }

    public void MusicStop(string name)
    {
        music.Find(x => x.name == name).Stop();
    }
}
