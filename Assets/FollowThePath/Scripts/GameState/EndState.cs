using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FollowThePath
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
            //SoundManager.Instance.StopBGM();
            SoundManager.Instance.StopLoopingSFX("SFX_Walk");
            CoroutineRunner.Instance.StartCoroutine(OnButtons(1));
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Stay()
        {
           
        }

        private IEnumerator OnButtons(float delay)
        {
            yield return new WaitForSeconds(delay);

            UIManager.Instance.GetUI<CanvasGroup>("EndButtons").FadeOfCanvasGroup(1, 1f);
        }
    }
}