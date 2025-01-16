using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace PangPangPang
{
    public class MainPanel : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        void Start()
        {
            SoundManager.Instance.Play(Define.AudioType.BGM, "BGM_Main");

            UIManager.Instance.GetUI<Button>("OptionBtn").onClick.AddListener(() =>
            {
                SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_Open");
                SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_BtnClick");
                UIManager.Instance.GetUI<CanvasGroup>("OptionPanel").FadeOfCanvasGroup(1, 0.2f);
            });

            UIManager.Instance.GetUI<Button>("MadeByBtn").onClick.AddListener(() =>
            {
                SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_Open");
                SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_BtnClick");
                UIManager.Instance.GetUI<CanvasGroup>("MadeByPanel").FadeOfCanvasGroup(1, 0.2f);
            });

            UIManager.Instance.GetUI<Button>("MethodBtn").onClick.AddListener(() =>
            {
                SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_Open");
                SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_BtnClick");
                UIManager.Instance.GetUI<CanvasGroup>("MethodPanel").FadeOfCanvasGroup(1, 0.2f);
            });

            UIManager.Instance.GetUI<Button>("StartBtn").onClick.AddListener(() =>
            {
                SoundManager.Instance.StopAll();
                SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_BtnClick");
                SceneManager.LoadScene("GameScene");
            });
        }
    }
}