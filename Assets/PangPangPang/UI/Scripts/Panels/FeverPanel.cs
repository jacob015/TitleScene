using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PangPangPang
{
    public class FeverPanel : MonoBehaviour
    {
        private void Off()
        {
            GetComponent<CanvasGroup>().SetActiveOfCanvasGroup(false);
        }
    }
}