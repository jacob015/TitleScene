using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace SpaceFarm
{
    public class MainPanel : MonoBehaviour
    {
        [SerializeField] private Button backBtn;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            Screen.orientation = ScreenOrientation.AutoRotation;

            // 세로 모드는 모두 비활성화
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;

            // 가로 모드만 활성화
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
        }

        void Start()
        {
            backBtn.onClick.AddListener(() => 
            {
                SceneManager.LoadScene("MainScene");
                Screen.orientation = ScreenOrientation.Portrait;
                SoundManager.Instance.StopBGM();
            });

            if (PlayerPrefs.GetInt("First") == 0)
            {
                PlayerPrefs.SetFloat("BGM", 0.5f);
                PlayerPrefs.SetFloat("SFX", 0.5f);
                PlayerPrefs.SetInt("First", 1);
            }
            else
            {
                SoundManager.Instance.SetBGMVolume(PlayerPrefs.GetFloat("BGM"));
                SoundManager.Instance.SetSFXVolume(PlayerPrefs.GetFloat("SFX"));
            }

            SoundManager.Instance.PlayBGM("BGM_Main");

            UIManager.Instance.GetUI<Button>("StartBtn").onClick.AddListener(() => 
            {
                SoundManager.Instance.PlaySFX("SFX_BtnClick");
                SoundManager.Instance.StopBGM();
                SceneManager.LoadScene("SpaceFarm_Game");
            });
        }


    }
}
