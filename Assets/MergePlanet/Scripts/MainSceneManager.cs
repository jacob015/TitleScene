using PlanetMerge;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField]private GameObject Set;
    [SerializeField]private GameObject explanation;

    public void StartBtn()
    {
        StartCoroutine(GameManager.instance.SceneLoading("MergePlanetGameScene"));
    }
    public void SetBtn()
    {
        Set.SetActive(true);
    }
    public void ExplanationBtn(bool on)
    {
        explanation.SetActive(on);
    }
    public void QuitBtn()
    {
        Application.Quit();
    }
}
