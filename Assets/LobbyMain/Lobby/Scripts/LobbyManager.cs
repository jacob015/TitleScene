using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    void Update()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
            || Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("LobbyMain/Scenes/MainScene");
        }
    }
}