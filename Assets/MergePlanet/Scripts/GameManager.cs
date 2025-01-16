using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

namespace PlanetMerge
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public int Count;
        public int WrongCount;
        public int RanNum;
        public bool isPause;
        [Header("사운드")]
        public AudioMixer mixer;
        [Header("사운드 토글")]
        public Color DefaultBackGroundColor;
        public Color DefaultHandleColor;
        public Color ActiveBackGroundColor;
        public Color ActiveHandleColor;
        [Header("매뉴")]
        public GameObject Set;
        [Header("점수")]
        public int Score;
        public int HighScore;
        public int MaxScore;
        public int MinScore;
        public TextMeshProUGUI ScoreText;
        public TextMeshProUGUI HighScoreText;

        public TextMeshProUGUI MenuScoreText;
        public TextMeshProUGUI MenuHighScoreText;
        [Header("타이머")]
        public Image TimerImage;
        public float timer;
        [Header("행성")]
        public GameObject GameStartObject;
        public bool isOver;
        public GameObject GameOverObject;
        public GameObject GameOverUI;
        private void Awake()
        {
            instance = this;
        }
        public IEnumerator GameStart()
        {
            yield return new WaitForSecondsRealtime(GameStartObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            GameStartObject.SetActive(false);
            IsPause(false);
        }
        public void Timer()
        {
            TimerImage.fillAmount -= 1 * Time.deltaTime / (timer - (0.01f * Count) - (0.1f * WrongCount));
            if (TimerImage.fillAmount >= 0.5f)
            {
                TimerImage.color = Color.Lerp(Color.green, Color.yellow, 2 - TimerImage.fillAmount * 2);
            }
            else TimerImage.color = Color.Lerp(Color.yellow, Color.red, 1 - TimerImage.fillAmount * 2);
            if (TimerImage.fillAmount <= 0.4f && TimerImage.fillAmount > 0 && !TimerImage.gameObject.GetComponent<AudioSource>().isPlaying)
                TimerImage.gameObject.GetComponent<AudioSource>().Play();
            if (TimerImage.fillAmount <= 0 && !GameOverObject.activeSelf && !GameOverUI.activeSelf)
            {
                TimerImage.fillAmount = 0;
                StartCoroutine(GameOver());
            }
        }
        public IEnumerator GameOver()
        {
            TimerImage.gameObject.GetComponent<AudioSource>().Stop();
            Time.timeScale = 0f;
            GameOverObject.SetActive(true);
            yield return new WaitForSecondsRealtime(GameOverObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            GameOverObject.SetActive(false);
            GameOverUI.SetActive(true);
        }
        public void SetOnOff(bool on)
        {
            IsPause(on);
            Set.SetActive(on);
        }
        public void GameReStart()
        {
            Time.timeScale = 1f;
            StartCoroutine(SceneLoading("MergePlanetGameScene"));
        }
        public void Quit()
        {
            Set.SetActive(false);
            IsPause(false);
            StartCoroutine(SceneLoading("MergePlanetMainScene"));
        }
        public IEnumerator SceneLoading(string sceneName)
        {
            yield return new WaitForSeconds(0.2f);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        public void IsPause(bool on)
        {
            isPause = on;
            if (isPause)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }
}
