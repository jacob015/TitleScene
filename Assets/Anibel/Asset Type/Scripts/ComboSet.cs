using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Anibel
{
    public class ComboSet : MonoBehaviour
    {
        private float currentCombo = 0;
        private float _combo = 0;

        public float Combo
        {
            set => _combo = value;
            get => _combo;
        }

        public TMP_Text comboText;

        private ScoreSet _scoreSet;

        private void Awake()
        {
            _scoreSet = GetComponent<ScoreSet>();
        }

        public void ComboUp()
        {
            if (currentCombo >= 50)
            {
                currentCombo = 0;
                _scoreSet.ScoreInteger += 0.1f;
            }

            _combo++;
            currentCombo++;
            comboText.text = $"{Math.Truncate(_combo)}";
        }

        public void ResetCombo()
        {
            _combo = 0;
            currentCombo = 0;
            _scoreSet.ScoreInteger = 1.0f;
            comboText.text = "";
        }
    }
}