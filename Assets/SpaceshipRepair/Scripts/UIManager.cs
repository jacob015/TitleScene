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
            timerText.text = "�ð�: " + Mathf.Ceil(timer).ToString();

            if (timer <= 0)
            {
                EndGame();
            }
        }

        public void AddScore(int amount)
        {
            score += amount;
            scoreText.text = "����: " + score.ToString();
        }

        void EndGame()
        {
            // ���� ���� UI ǥ��
            Debug.Log("���� ����!");
            //FindObjectOfType<GameOverMenu>().ShowGameOverMenu();
        }
    }
}