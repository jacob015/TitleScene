using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public static SelectManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void OnClickMainSceneButton()
    {
        SceneManager.LoadScene("LobbyMain/Scenes/MainScene");
    }
    
    public void OnClickGameSceneButton(string scenePath)
    {
        SceneManager.LoadScene(scenePath);
    }
}
