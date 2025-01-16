using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FollowThePath
{
    public class EndPanel : MonoBehaviour
    {
        private Button _restartBtn;
        private Button _homeBtn;

        void Start()
        {
            _restartBtn = UIManager.Instance.GetUI<Button>("RestartButton");
            _homeBtn = UIManager.Instance.GetUI<Button>("HomeButton");

            _restartBtn.onClick.AddListener(() => 
            {
                SoundManager.Instance.PlaySFX("SFX_BtnClick");
                _restartBtn.transform.ChangeScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f, true, () => { SceneManager.LoadScene("FollowThePath_Game"); });
                
            });
            _homeBtn.onClick.AddListener(() => 
            {
                SoundManager.Instance.PlaySFX("SFX_BtnClick");
                _homeBtn.transform.ChangeScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f, true, () => { SceneManager.LoadScene("FollowThePath_Main"); });
                
            });
        }


    }
}
