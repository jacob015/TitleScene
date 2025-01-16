using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShipRepair
{
    public class TimerAndScore : MonoBehaviour
    {
        public Text timerText;
        public Text scoreText;
        public GameOverMenu gameOverMenu;  // GameOverMenu를 참조

        private float timer = 3f;
        private int score = 0;

        void Start()
        {
            Time.timeScale = 1.0f;
            if (gameOverMenu == null)
            {
                Debug.LogError("GameOverMenu가 할당되지 않았습니다. 인스펙터에서 참조를 설정해주세요.");
            }
        }

        void Update()
        {
            timer -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timer).ToString();
            if (timer < 0)
            {
                timer = 0;
                Time.timeScale = 0;
                GameOver();
            }
        }

        public void AddScore(int amount)
        {
            score += amount;
            scoreText.text = score.ToString();
        }

        public void StartTimer()
        {
            timer = 60f;
        }

        void GameOver()
        {
            Debug.Log("게임 오버");

            // 현재 점수를 GameOverMenu에 전달
            gameOverMenu.ShowGameOverMenu(score);
        }
    }
}