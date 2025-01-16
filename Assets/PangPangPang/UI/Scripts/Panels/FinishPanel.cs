using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace PangPangPang
{
    public class FinishPanel : MonoBehaviour
    {
        [SerializeField] private Sprite _timeOutPanel;
        [SerializeField] private Sprite _gameOverPanel;

        private TextMeshProUGUI _finishScoreText;



        void Start()
        {


            _finishScoreText = UIManager.Instance.GetUI<TextMeshProUGUI>("FinishScoreText");


            UIManager.Instance.GetUI<Button>("FinishPopupExitBtn").onClick.AddListener(() =>
            {

                SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_BtnClick");
            //UIManager.Instance.GetUI<CanvasGroup>("FinishPanel").SetActiveOfCanvasGroup(false);
            SceneManager.LoadScene("MainScene");
            });
        }

        public void OnFinishPanel()
        {
            if (GameManager.Instance.MissCount <= 0)
            {
                UIManager.Instance.GetUI<Image>("FinishPopup").sprite = _gameOverPanel;
            }
            else
            {

                UIManager.Instance.GetUI<Image>("FinishPopup").sprite = _timeOutPanel;
            }



        }

        public void FinishAnimaion()
        {
            StartCoroutine(CountUpScore(0, GameManager.Instance.Score, 0.05f, 1f));
        }

        private IEnumerator CountUpScore(int from, int to, float duration, float finish)
        {


            int scoreCount = from;
            float currentTime = 0;
            float maxTime = 0;



            while (scoreCount < to)
            {
                if (maxTime >= finish)
                {
                    break;
                }
                else
                {
                    if (currentTime >= duration )
                    {
                        SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_CountUp");

                        currentTime = 0;
                        scoreCount++;
                        _finishScoreText.text = scoreCount.ToString("N0");
                    }
                    else
                    {
                        currentTime += Time.deltaTime;

                    }

                    maxTime += Time.deltaTime;
                }

                yield return null;
            }
            scoreCount = to;
            _finishScoreText.text = scoreCount.ToString("N0");

            UIManager.Instance.GetUI<CanvasGroup>("FinishPopupExitBtn").FadeOfCanvasGroup(1, 0.5f);
        }
    }
}