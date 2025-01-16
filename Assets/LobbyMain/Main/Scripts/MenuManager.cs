using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance = null;
    public GameObject settingPanel;
    bool isSettingPanelOn = true;

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

    public void OnClickSettingButton()
    {
        settingPanel.SetActive(isSettingPanelOn);
        isSettingPanelOn = !isSettingPanelOn;
    }
    
    public void OnClickSelectSceneButton()
    {
        SceneManager.LoadScene("LobbyMain/Scenes/SelectScene");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
