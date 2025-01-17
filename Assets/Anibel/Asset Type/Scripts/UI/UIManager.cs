using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Anibel
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        public GameObject GameOverBox;
        public GameObject PauseBox;
        public TMP_Text scoreText;
        public ScoreSet _Score;

        public void ActiveGameOver()
        {
            SoundManager.Instance.PlaySFX("JingleFail");
            Time.timeScale = 0;
            GameOverBox.SetActive(true);
            scoreText.text = $"점수 : {Math.Truncate(_Score.Score)}";
        }

        public void ActivePause()
        {
            SoundManager.Instance.PlaySFX("UISmall");
            Time.timeScale = 0;
            PauseBox.SetActive(true);
        }

        public void RetryButton()
        {
            SoundManager.Instance.PlaySFX("UISmall");
            Time.timeScale = 1f;
            SceneManager.LoadScene("Anibel Main");
        }

        public void MainTitleButton()
        {
            SoundManager.Instance.PlaySFX("UISmall");
            Time.timeScale = 1f;
            SceneManager.LoadScene("Anibel Title");
        }

        public void CountinueButton()
        {
            SoundManager.Instance.PlaySFX("UISmall");
            Time.timeScale = 1f;
            PauseBox.SetActive(false);
        }
    }
}