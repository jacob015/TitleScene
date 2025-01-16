using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FollowThePath
{
    public class StartPanel : MonoBehaviour
    {

        void Start()
        {
            UIManager.Instance.GetUI<CanvasGroup>("StartFadePanel").FadeOfCanvasGroup(0,0.5f);
        }


    }
}
