using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TouchUp
{
    public class UIManager : MonoBehaviour
    {
        private void Start()
        {
            Time.timeScale = 1;
        }

        public void GamePause()
        {
            Time.timeScale = 0;
        }

        public void GameCountinue()
        {
            Time.timeScale = 1;
        }

        public void GameExit()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("TouchUP/Scenes/TouchUP Title");
        }

        public void GameRestart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("TouchUP/Scenes/TouchUP Main");
        }
    }
}