using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TouchUp
{
    public class GaugeBar : MonoBehaviour
    {
        public Image gaugebar;
        public GameObject GameOverBox;
        public TMP_Text scoreText;

        public float currentTime;

        private float decreasePerSec = 1f;
        private float duration = 60f;

        private void Start()
        {
            currentTime = duration;
        }

        public void PlusTime(int plus)
        {
            currentTime += plus;
            if (currentTime > 60)
            {
                currentTime = 60;
            }

            duration = currentTime;
        }

        private void Update()
        {
            DecreaseGauge();
        }

        private void DecreaseGauge()
        {
            if (currentTime > 0)
            {
                currentTime = Mathf.Max(0, currentTime - decreasePerSec * Time.deltaTime);
                gaugebar.fillAmount = currentTime / duration;
            }
            else
            {
                TileManager.Instance.backGround.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                GameOverBox.SetActive(true);
                scoreText.text = $"점수: {TileManager.Instance.score}";
            }
        }
    }
}
