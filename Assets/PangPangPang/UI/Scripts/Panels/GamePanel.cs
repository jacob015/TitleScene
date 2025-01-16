using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PangPangPang 
{
    public class GamePanel : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            UIManager.Instance.GetUI<Button>("OptionBtn").onClick.AddListener(() =>
            {
                SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_Open");
                SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_BtnClick");

                GameManager.Instance.Pause(true);

                UIManager.Instance.GetUI<CanvasGroup>("OptionPanel").FadeOfCanvasGroup(1, 0.2f, () =>
                {
                    Time.timeScale = 0;
                });


            });
        }

    }
}

