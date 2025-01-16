using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceFarm
{
    public class EndState : GameState
    {
        public override void Init()
        {
            _panel = UIManager.Instance.GetUI<CanvasGroup>("EndPanel");
        }
        public override void Enter()
        {
            base.Enter();
            SoundManager.Instance.StopBGM();
            UIManager.Instance.GetUI<CanvasGroup>("EndBtnsPanel").FadeOfCanvasGroup(1, 1f,1f);
        }
        public override void Stay()
        {

        }
    }
}