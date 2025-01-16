using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Anibel
{
    public class PenaltyGauge : MonoBehaviour
    {
        public Image penaltyGauge;

        public void PenaltyStack()
        {
            if (penaltyGauge.fillAmount < 1)
            {
                penaltyGauge.fillAmount += 0.1f;
            }
            else
            {
                SoundManager.Instance.PlaySFX("JingleFail");
                UIManager.Instance.ActiveGameOver();
            }
        }
    }
}