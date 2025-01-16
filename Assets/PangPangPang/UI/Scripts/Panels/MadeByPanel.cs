using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace PangPangPang
{
    public class MadeByPanel : MonoBehaviour
    {

        void Start()
        {
            UIManager.Instance.GetUI<Button>("MadeByPopupExitBtn").onClick.AddListener(() =>
            {
                SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_BtnClick");
                UIManager.Instance.GetUI<CanvasGroup>("MadeByPanel").FadeOfCanvasGroup(0, 0.2f);
            });
        }


    }
}