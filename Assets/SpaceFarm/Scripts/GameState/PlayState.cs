using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpaceFarm
{
    public class PlayState : GameState
    {

        private FarmManager _farmManager;
        private float _maxPlayTime = 180;

        public override void Init()
        {
            _farmManager = GameObject.Find("FarmImage").GetComponent<FarmManager>();
            _panel = UIManager.Instance.GetUI<CanvasGroup>("PlayPanel");

        }

        public override void Enter()
        {
            
        }
        public override void Exit()
        {

            
        }

        public override void Stay()
        {
            if (_maxPlayTime <= 0)
            {
                SoundManager.Instance.StopBGM();
                GameManager.Instance.CurGameState = Define.GameState.END;

            }
            else
            {
                _maxPlayTime -= Time.deltaTime;
            }

            _farmManager.ChangeEvent(_maxPlayTime);
            ShowPlayTime();
        }    

        private void ShowPlayTime()
        {
            UIManager.Instance.GetUI<TextMeshProUGUI>("PlayTimeText").text = Mathf.Ceil(_maxPlayTime).ToString();
        }
    }
}