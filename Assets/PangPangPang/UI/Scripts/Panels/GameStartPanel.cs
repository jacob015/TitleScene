using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace PangPangPang
{
    public class GameStartPanel : MonoBehaviour
    {

        private void StartGame()
        {
            UIManager.Instance.GetUI<CanvasGroup>("GameStartPanel").SetActiveOfCanvasGroup(false);
            GameManager.Instance.GameState = Define.GameState.BEGIN;

            SoundManager.Instance.Play(Define.AudioType.BGM, "BGM_GamePlay");
        }

        private void ChangeText(string str)
        {


            if (str == "Start!")
            {
                SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_Start");
            }
            else
            {
                SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_CountDown");
            }


        }
    }
}
