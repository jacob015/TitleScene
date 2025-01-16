using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    void Update()
    {
        // Check if the screen has been touched or mouse clicked
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            // Load the MainScene
            SceneManager.LoadScene("LobbyMain/Scenes/MainScene");
        }
    }
}