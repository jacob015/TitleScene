using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FollowThePath
{
    public class MainPanel : MonoBehaviour
    {
        private Button _startGameBtn;

        private void Awake()
        {
            Application.targetFrameRate = 60;

        }

        void Start()
        {
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
            GetComponent<CanvasGroup>().FadeOfCanvasGroup(1, 0.5f);



            _startGameBtn = UIManager.Instance.GetUI<Button>("StartGameButton");
            _startGameBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySFX("SFX_BtnClick");
                _startGameBtn.transform.ChangeScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f, true, () => 
                {
                    GetComponent<CanvasGroup>().FadeOfCanvasGroup(0, 0.5f,0,()=> 
                    {
                        UIManager.Instance.GetUI<CanvasGroup>("MainFadePanel").FadeOfCanvasGroup(1, 0.5f, 0,()=> { SceneManager.LoadScene("FollowThePath_Game"); });
                    });
                });
            });

            SoundManager.Instance.PlayBGM("BGM_Main");
        }
    }
}
