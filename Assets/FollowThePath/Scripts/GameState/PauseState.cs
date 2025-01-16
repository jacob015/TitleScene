using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FollowThePath
{
    public class PauseState : GameState
    {
        public PauseState()
        {
            
        }

        public override void Init()
        {
            _panel = UIManager.Instance.GetUI<CanvasGroup>("PausePanel");
        }
        public override void Enter()
        {
            base.Enter();
            
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Stay()
        {
           
        }
    }
}