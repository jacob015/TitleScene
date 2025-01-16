using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceFarm
{
    public abstract class GameState
    {
        protected CanvasGroup _panel;

        public abstract void Init();

        public virtual void Enter()
        {
            _panel.FadeOfCanvasGroup(1,0.2f);
        }
        public abstract void Stay();
        public virtual void Exit()
        {
            _panel.FadeOfCanvasGroup(0, 0.2f);
        }
    }
}