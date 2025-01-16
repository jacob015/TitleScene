using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceFarm
{
    public class PauseState : GameState
    {
        public override void Init()
        {
            _panel = UIManager.Instance.GetUI<CanvasGroup>("PausePanel");
        }

        public override void Stay()
        {

        }

    }
}