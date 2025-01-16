using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FollowThePath
{
    public class PausePanel : MonoBehaviour
    {

        private Button _closeBtn;
        private Button _restartBtn;
        private Button _HomeBtn;

        private Slider _bgmSlider;
        private Slider _sfxSlider;
        void Start()
        {
            _closeBtn = UIManager.Instance.GetUI<Button>("CloseButton");
            _restartBtn = UIManager.Instance.GetUI<Button>("PauseRestartButton");
            _HomeBtn = UIManager.Instance.GetUI<Button>("PauseHomeButton");

            _bgmSlider = UIManager.Instance.GetUI<Slider>("BGMSlider");
            _sfxSlider = UIManager.Instance.GetUI<Slider>("SFXSlider");

            _closeBtn.onClick.AddListener(() => 
            {
                SoundManager.Instance.PlaySFX("SFX_BtnClick");
                _closeBtn.transform.ChangeScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f, true,()=> { GameManager.Instance.CurGameState = Define.GameState.PLAY; });               
            });
            _restartBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySFX("SFX_BtnClick");
                _restartBtn.transform.ChangeScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f, true, () => { SceneManager.LoadScene("FollowThePath_Game"); });
            });
            _HomeBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySFX("SFX_BtnClick");
                _HomeBtn.transform.ChangeScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f, true, () => { SceneManager.LoadScene("FollowThePath_Main"); });
            });

            _bgmSlider.value = PlayerPrefs.GetFloat("BGM");
            _sfxSlider.value = PlayerPrefs.GetFloat("SFX");

            SoundManager.Instance.SetBGMVolume(_bgmSlider.value);
            SoundManager.Instance.SetSFXVolume(_sfxSlider.value);

            _bgmSlider.onValueChanged.AddListener((value) => 
            {
                SoundManager.Instance.SetBGMVolume(value);
                PlayerPrefs.SetFloat("BGM", value);
            });

            _sfxSlider.onValueChanged.AddListener((value) =>
            {
                SoundManager.Instance.SetSFXVolume(value);
                PlayerPrefs.SetFloat("SFX", value);
            });
        }


    }

}