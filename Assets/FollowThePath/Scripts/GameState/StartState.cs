using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FollowThePath
{
    public class StartState : GameState
    {

        public override void Init()
        {
            _panel = UIManager.Instance.GetUI<CanvasGroup>("StartPanel");

            UIManager.Instance.GetUI<Button>("PlayButton").onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySFX("SFX_BtnClick");
                GameManager.Instance.CurGameState = Define.GameState.PLAY;
                SoundManager.Instance.PlayBGM("BGM_InGame");
                SoundManager.Instance.PlayLoopingSFX("SFX_Walk");
                SoundManager.Instance.StopLoopingSFX("SFX_Walk");
            });


            SoundManager.Instance.StopBGM();
        }

        public override void Enter()
        {
           
            
            
        }

        public override void Stay()
        {
          
        }
    }
}