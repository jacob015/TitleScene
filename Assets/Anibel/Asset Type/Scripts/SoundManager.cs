using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Anibel
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    public class SoundManager : SingletonMonoBehaviour<SoundManager>
    {
        [SerializeField] private Sound[] sfx = null;
        [SerializeField] private Sound[] bgm = null;

        [SerializeField] private AudioSource bgmPlayer = null;
        [SerializeField] private AudioSource[] sfxPlayer = null;

        public void PlayBGM(string p_bgmName)
        {
            Sound found = bgm.FirstOrDefault(x => x.name.Equals(p_bgmName));
            if (found == null)
            {
                Debug.Log($"{p_bgmName} 이름의 효과음이 없습니다");
                return;
            }

            bgmPlayer.clip = found.clip;
            bgmPlayer.Play();
        }

        public void StopBGM()
        {
            bgmPlayer.Stop();
        }

        public void PlaySFX(string p_sfxName)
        {
            if (sfx.Length <= 0)
                return;

            Sound found =
                sfx.FirstOrDefault(x =>
                    x.name.Equals(p_sfxName)); // FirstOrDefault 는 배열을 순회하는 것, x.name.Equals 에 해당하는 것을 가져온다.
            if (found == null)
            {
                Debug.Log($"{p_sfxName} 이름의 효과음이 없습니다.");
                return;
            }

            AudioSource notUsingPlayer = sfxPlayer.FirstOrDefault(x => !x.isPlaying);
            if (notUsingPlayer == null)
            {
                Debug.Log("모든 효과음이 플레이 중입니다.");
                return;
            }

            notUsingPlayer.clip = found.clip;
            notUsingPlayer.Play();
        }
    }
}