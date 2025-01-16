using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace SpaceFarm
{
    public class StartState : GameState
    {
        public override void Init()
        {
            _panel = UIManager.Instance.GetUI<CanvasGroup>("StartPanel");

            UIManager.Instance.GetUI<Button>("PlayButton").onClick.AddListener(() =>
            {
                GameManager.Instance.CurGameState = Define.GameState.PLAY;
                UIManager.Instance.GetUI<CanvasGroup>("StartPanel").FadeOfCanvasGroup(0, 0.5f,0,()=> 
                {            
                    SoundManager.Instance.PlayBGM("BGM_InGame");
                });
                
            });
        }
        public override void Enter()
        {
            UIManager.Instance.GetUI<CanvasGroup>("StartFadePanel").FadeOfCanvasGroup(0, 1f);

        }

        public override void Exit()
        {

            _panel.FadeOfCanvasGroup(0, 0);
            
        }

        public override void Stay()
        {

        }
    }
}