using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShipRepair
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject pauseMenuUI;
        private bool isPaused = false;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }

        void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }

        public void QuitToMainMenu()
        {
            Time.timeScale = 1f;
            // 메인 메뉴로 돌아가는 로직 추가
            SceneManager.LoadScene("MainMenu");
        }
    }
}