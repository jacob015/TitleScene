using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShipRepair
{
    public class GameData : MonoBehaviour
    {
        public static GameData instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SaveScore(int score)
        {
            PlayerPrefs.SetInt("CurrentScore", score);
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            if (score > highScore)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }

        public int LoadHighScore()
        {
            return PlayerPrefs.GetInt("HighScore", 0);
        }
    }
}