using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Anibel
{
    public class StartTheGame : SingletonMonoBehaviour<StartTheGame>
    {
        private bool canStart;

        public bool CanStart
        {
            get => canStart;
        }

        public TMP_Text countText;
        public GameObject countShadow;

        private void Start()
        {
            canStart = false;
            StartCoroutine(CountDownToEnd());
        }

        IEnumerator CountDownToEnd()
        {
            for (int i = 3; i > 0; i--)
            {
                countText.text = $"{i.ToString()}";
                yield return new WaitForSeconds(1f);
            }

            countText.text = "Start!";
            yield return new WaitForSeconds(1f);
            countShadow.SetActive(false);
            countText.text = "";

            canStart = true;
        }
    }
}