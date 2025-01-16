using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace PangPangPang
{
    public class MethodPanel : MonoBehaviour
    {
        void Start()
        {

            UIManager.Instance.GetUI<Button>("MethodPopupExitBtn").onClick.AddListener(() =>
            {
                SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_BtnClick");
                UIManager.Instance.GetUI<CanvasGroup>("MethodPanel").FadeOfCanvasGroup(0, 0.2f);
            });
        }
    }
}
