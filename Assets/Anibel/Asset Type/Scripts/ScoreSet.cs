using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Anibel
{
    public class ScoreSet : MonoBehaviour
    {
        public TMP_Text scoreText;
        private float _score = 0;

        public float Score
        {
            set => _score = value;
            get => _score;
        }

        private float scoreInteger = 1.0f;

        public float ScoreInteger
        {
            set => scoreInteger = value;
            get => scoreInteger;
        }

        public void ScoreUp()
        {
            _score += scoreInteger;
            scoreText.text = $"{Math.Truncate(_score)}";
        }
    }
}