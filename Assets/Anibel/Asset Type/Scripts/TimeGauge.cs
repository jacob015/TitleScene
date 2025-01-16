using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Anibel
{
    public class TimeGauge : SingletonMonoBehaviour<TimeGauge>
    {
        public Image gaugebar;
        public float duration = 3.0f;
        private float currentTime;

        private void Start()
        {
            currentTime = duration;
        }

        private void Update()
        {
            if (!StartTheGame.Instance.CanStart)
                return;

            DecreaseGauge();
        }

        public void ResetGauge()
        {
            currentTime = duration;
        }

        public void DecreaseDuration()
        {
            duration *= 0.98f;
        }

        private void DecreaseGauge()
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                gaugebar.fillAmount = currentTime / duration;
            }
            else
            {
                currentTime = 1f;
                UIManager.Instance.ActiveGameOver();
            }
        }
    }
}