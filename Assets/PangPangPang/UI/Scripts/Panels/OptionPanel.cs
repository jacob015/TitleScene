using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace PangPangPang
{
    public class OptionPanel : MonoBehaviour
    {
        private Toggle _bgmToggle;
        private Toggle _sfxToggle;

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

            _exitBtn = UIManager.Instance.GetUI<Button>("OptionPopupExitBtn");

            _exitBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_BtnClick");
                UIManager.Instance.GetUI<CanvasGroup>("OptionPanel").FadeOfCanvasGroup(0, 0.2f);
            });
        }


    }
}