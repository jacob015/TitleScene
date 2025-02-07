using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceShipRepair
{
    public class GameOverMenu : MonoBehaviour
    {
        public GameObject gameOverMenuUI;
        public Text currentScoreText;
        public Text highScoreText;
        private int currentScore;
        private int highScore;

        void Start()
        {
            // ���� ���� UI�� ��Ȱ��ȭ�մϴ�.
            gameOverMenuUI.SetActive(false);

            // �ְ� ������ �ҷ��ɴϴ�.
            highScore = PlayerPrefs.GetInt("HighScore", 0); // �⺻���� 0���� ����
        }

        public void ShowGameOverMenu(int score)
        {
            gameOverMenuUI.SetActive(true);

            // ���� ������ UI�� �ݿ��մϴ�.
            currentScore = score;
            currentScoreText.text = currentScore.ToString();

            // �ְ� ���� ���� ���� Ȯ�� �� ����
            if (currentScore > highScore)
            {
                highScore = currentScore;
                PlayerPrefs.SetInt("HighScore", highScore);
                PlayerPrefs.Save(); // ��� ����
                highScoreText.text = highScore.ToString() + " (NEW)";
            }
            else
            {
                highScoreText.text = highScore.ToString();
            }
        }

        public void RestartGame()
        {
            Debug.Log("Restart");
            SceneManager.LoadScene("SpaceshipRepair/Scenes/SpaceShip GameScene");
        }

        public void QuitToMainMenu()
        {
            SceneManager.LoadScene("SpaceshipRepair/Scenes/SpaceShip MainMenu");
        }
    }
}