using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anibel
{
    public class JudgeArea : MonoBehaviour
    {
        private int gameOverCount = 0;

        public int GameOverCount
        {
            get => gameOverCount;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<AnimalMove>() == null)
                return;

            string[] test = other.GetComponent<AnimalMove>().Data.name.Split('.');
            if (test[0] == "Wolf")
            {
                switch (test[1])
                {
                    case "Bomb":
                        UIManager.Instance.ActiveGameOver();
                        break;
                    default:
                        AnimalManager.Instance.Surprise();
                        SoundManager.Instance.PlaySFX("AnimalPunch2");
                        gameOverCount++;
                        break;
                }
            }
            else
            {
                SoundManager.Instance.PlaySFX("AnimalPunch");
                gameOverCount = 0;
            }

            if (gameOverCount >= 2)
            {
                UIManager.Instance.ActiveGameOver();
            }
        }
    }
}