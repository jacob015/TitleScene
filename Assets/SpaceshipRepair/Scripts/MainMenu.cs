using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShipRepair
{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("SpaceshipRepair/Scenes/SpaceShip GameScene");
        }

        public void OpenSettings()
        {
            // ���� ȭ�� ����
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}