using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PangPangPang
{

    public class InGameOptionPanel : MonoBehaviour
    {
        private Toggle _bgmToggle;
        private Toggle _sfxToggle;

        private Button _returnBtn;
        private Button _exitBtn;
        void Start()
        {
            _bgmToggle = UIManager.Instance.GetUI<Toggle>("BGMSwitchToggle");
            _sfxToggle = UIManager.Instance.GetUI<Toggle>("SFXSwitchToggle");

            _bgmToggle.isOn = !SoundManager.Instance.isMutedBGM;
            _sfxToggle.isOn = !SoundManager.Instance.isMutedSFX;

            _bgmToggle.onValueChanged.AddListener((value) =>
            {
                SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_BtnClick");
                SoundManager.Instance.SetMute(Define.AudioType.BGM, !value);

            });
            _sfxToggle.onValueChanged.AddListener((value) =>
            {
                SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_BtnClick");
                SoundManager.Instance.SetMute(Define.AudioType.SFX, !value);

            });


            _returnBtn = UIManager.Instance.GetUI<Button>("GameReturnBtn");
            _exitBtn = UIManager.Instance.GetUI<Button>("GameExitBtn");

            // 계속하기 버튼
            _returnBtn.onClick.AddListener(() =>
            {

                SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_BtnClick");

                Time.timeScale = 1;

                GameManager.Instance.Pause(false);

                UIManager.Instance.GetUI<CanvasGroup>("OptionPanel").FadeOfCanvasGroup(0, 0.2f);

            });
            // 메인화면 버튼
            _exitBtn.onClick.AddListener(() =>
            {


                SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_BtnClick");

                Time.timeScale = 1;

                SceneManager.LoadScene("MainScene");


            });


        }


    }
}