using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceFarm
{
    public class PlayPanel : MonoBehaviour
    {
        private Button _pauseBtn;

        void Start()
        {
            _pauseBtn = UIManager.Instance.GetUI<Button>("PauseBtn");
            _pauseBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySFX("SFX_BtnClick");
                _pauseBtn.transform.ChangeScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f, true, () =>
                {
                    GameManager.Instance.CurGameState = Define.GameState.PAUSE;
                });

            });
        }
    }
}