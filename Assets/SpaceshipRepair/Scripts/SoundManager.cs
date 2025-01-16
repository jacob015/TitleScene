using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShipRepair
{
    public class SoundManager : MonoBehaviour
    {
        public AudioSource backgroundMusic;
        public AudioClip[] soundEffects;

        void Start()
        {
            PlayBackgroundMusic();
        }

        public void PlayBackgroundMusic()
        {
            if (backgroundMusic != null)
            {
                backgroundMusic.Play();
            }
        }

        public void PlaySoundEffect(int index)
        {
            if (index >= 0 && index < soundEffects.Length)
            {
                AudioSource.PlayClipAtPoint(soundEffects[index], transform.position);
            }
        }
    }
}