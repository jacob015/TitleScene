using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShipRepair
{
    public class UIManager : MonoBehaviour
    {
        public Text timerText;
        public Text scoreText;
        private int score;
        private float timer = 60.0f;

        void Update()
        {
            timer -= Time.deltaTime;
            timerText.text = "시간: " + Mathf.Ceil(timer).ToString();

            if (timer <= 0)
            {
                EndGame();
            }
        }

        public void AddScore(int amount)
        {
            score += amount;
            scoreText.text = "점수: " + score.ToString();
        }

        void EndGame()
        {
            // 게임 종료 UI 표시
            Debug.Log("게임 종료!");
            //FindObjectOfType<GameOverMenu>().ShowGameOverMenu();
        }
    }
}