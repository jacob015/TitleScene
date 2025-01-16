using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace ColorMatch
{
    public class MainController : MonoBehaviour
    {
        public GameObject MainGame;
        public GameObject Set;
        public GameObject Tutorial;
        public GameObject End;

        public Animator Main;

        public CanvasGroup fade;

        public TextMeshProUGUI BGM;
        public TextMeshProUGUI SFX;
        public Image BGMIcon;
        public Image SFXIcon;
        public Sprite[] soundimage;

        public int MainNum;
        public int SetNum;
        public int EndNum;
        public int TutorialNum;

        public SoundSet soundSet;

        public GameController gameController;
        private void Start()
        {
            if (soundSet.defaultBGMVolume <= 0)
            {
                soundSet.defaultBGMVolume = 0.001f;
                BGMIcon.sprite = soundimage[1];
            }
            if (soundSet.defaultSFXVolume <= 0)
            {
                soundSet.defaultSFXVolume = 0.001f;
                SFXIcon.sprite = soundimage[1];
            }
            BGM.text = Mathf.RoundToInt((soundSet.defaultBGMVolume * 10)).ToString("D2");
            soundSet.SetBGMVolume(soundSet.defaultBGMVolume);

            SFX.text = Mathf.RoundToInt((soundSet.defaultSFXVolume * 10)).ToString("D2");
            soundSet.SetSFXVolume(soundSet.defaultSFXVolume);

            for (int i = 0; i < 10; i++)
            {
                if (Mathf.RoundToInt((soundSet.defaultBGMVolume * 10)) > i)
                    BGM.transform.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                else
                    BGM.transform.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);

                if (Mathf.RoundToInt((soundSet.defaultSFXVolume * 10)) > i)
                    SFX.transform.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                else
                    SFX.transform.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);
            }
        }
        private void Update()
        {
            BGMIcon.sprite = soundSet.defaultBGMVolume > 0.09f ? soundimage[0] : soundimage[1];
            SFXIcon.sprite = soundSet.defaultSFXVolume > 0.09f ? soundimage[0] : soundimage[1];
            if (Set.activeSelf)
            {
                for (int i = 1; i <= 4; i++)
                {
                    Debug.Log(i);
                    Set.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);

                    if (i == SetNum) Set.transform.GetChild(SetNum).GetChild(1).gameObject.SetActive(true);
                }
            }
            else if (MainGame.activeSelf)
            {
                for (int i = 1; i <= 3; i++)
                {
                    MainGame.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);

                    if (i == MainNum) MainGame.transform.GetChild(MainNum).GetChild(1).gameObject.SetActive(true);
                }
            }
            if (End.activeSelf)
            {
                for (int i = 1; i <= 2; i++)
                {
                    End.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
                    if (i == EndNum) End.transform.GetChild(EndNum).GetChild(1).gameObject.SetActive(true);
                }
            }
            if (Tutorial.activeSelf)
            {
                for (int i = 0; i < Tutorial.transform.childCount; i++)
                {
                    Tutorial.transform.GetChild(i).gameObject.SetActive(false);
                    if (i == TutorialNum) Tutorial.transform.GetChild(TutorialNum).gameObject.SetActive(true);
                }
            }
        }
        public void SelectMainButton(int num)
        {
            switch (num)
            {
                case 1:
                    StartCoroutine(GamePlay());
                    break;
                case 2:
                    MainNum = 0;
                    Set.SetActive(true);
                    break;
                case 3:
                    GameQuit();
                    break;
            }
        }
        public void SelectSetButton(int num)
        {
            switch (num)
            {
                case 3:
                    if(SceneManager.GetActiveScene().name == "ColorMacthMain")
                        Tutorial.SetActive(true);
                    else if(SceneManager.GetActiveScene().name == "ColorMacthGame")
                    {
                        SetNum = 0;
                        gameController.SetButton();
                    }
                    break;
                case 4:
                    if (SceneManager.GetActiveScene().name == "ColorMacthMain")
                    {
                        SetNum = 0;
                        Set.SetActive(false);
                    }
                    else if (SceneManager.GetActiveScene().name == "ColorMacthGame")
                    {
                        SetNum = 0;
                        GameOut();
                    }
                    break;
            }

        }
        public void SelectSEndButton(int num)
        {
            switch (num)
            {
                case 1:
                    ReGame();
                    break;
                case 2:
                    GameOut();
                    break;
            }
        }
        public void UPButton()
        {
            if (!Tutorial.activeSelf)
            {
                if (Set.activeSelf)
                {
                    SetNum--;
                    if (SetNum > 4) SetNum = 1;
                    if (SetNum < 1) SetNum = 4;
                }
                else if(MainGame.activeSelf)
                {
                    MainNum--;
                    if (MainNum > 3) MainNum = 1;
                    if (MainNum < 1) MainNum = 3;
                }
                if (End.activeSelf)
                {
                    EndNum--;
                    if (EndNum > 2) EndNum = 1;
                    if (EndNum < 1) EndNum = 2;
                }
            }
        }
        public void DownButton()
        {
            if (!Tutorial.activeSelf)
            {
                if (Set.activeSelf)
                {
                    SetNum++;
                    if (SetNum > 4) SetNum = 1;
                    if (SetNum < 1) SetNum = 4;
                }
                else if (MainGame.activeSelf)
                {
                    MainNum++;
                    if (MainNum > 3) MainNum = 1;
                    if (MainNum < 1) MainNum = 3;
                }
                if(End.activeSelf)
                {
                    EndNum++;
                    if (EndNum > 2) EndNum = 1;
                    if (EndNum < 1) EndNum = 2;
                }
            }
        }
        public void RightButton()
        {
            if (Set.activeSelf)
            {
                if (Tutorial.activeSelf)
                {
                    if(TutorialNum < Tutorial.transform.childCount-1)
                        TutorialNum++;
                }
                switch (SetNum)
                {
                    case 1:
                        soundSet.defaultBGMVolume += 0.1f;
                        if (soundSet.defaultBGMVolume >= 1) soundSet.defaultBGMVolume = 1;
                        BGM.text = Mathf.RoundToInt((soundSet.defaultBGMVolume * 10)).ToString("D2");
                        soundSet.SetBGMVolume(soundSet.defaultBGMVolume);
                        for (int i = 0; i < 10; i++)
                        {
                            if (Mathf.RoundToInt((soundSet.defaultBGMVolume * 10)) > i)
                                BGM.transform.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            else
                                BGM.transform.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);
                        }
                        break;
                    case 2:
                        soundSet.defaultSFXVolume += 0.1f;
                        if (soundSet.defaultSFXVolume >= 1) soundSet.defaultSFXVolume = 1;
                        SFX.text = Mathf.RoundToInt((soundSet.defaultSFXVolume * 10)).ToString("D2");
                        soundSet.SetSFXVolume(soundSet.defaultSFXVolume);
                        for (int i = 0; i < 10; i++)
                        {
                            if (Mathf.RoundToInt((soundSet.defaultSFXVolume * 10)) > i)
                                SFX.transform.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            else
                                SFX.transform.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);
                        }
                        break;
                }
            }
        }
        public void LeftButton()
        {
            if (Set.activeSelf)
            {
                if (Tutorial.activeSelf)
                {
                    if(TutorialNum > 0)
                        TutorialNum--;
                }
                switch (SetNum)
                {
                    case 1:
                        soundSet.defaultBGMVolume -= 0.1f;
                        if (soundSet.defaultBGMVolume <= 0) soundSet.defaultBGMVolume = 0.001f;
                        BGM.text = Mathf.RoundToInt((soundSet.defaultBGMVolume * 10)).ToString("D2");
                        soundSet.SetBGMVolume(soundSet.defaultBGMVolume);
                        for (int i = 0; i < 10; i++)
                        {
                            if (Mathf.RoundToInt((soundSet.defaultBGMVolume * 10)) > i)
                                BGM.transform.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            else
                                BGM.transform.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);
                        }
                        break;
                    case 2:
                        soundSet.defaultSFXVolume -= 0.1f;
                        if (soundSet.defaultSFXVolume <= 0) soundSet.defaultSFXVolume = 0.001f;
                        SFX.text = Mathf.RoundToInt((soundSet.defaultSFXVolume * 10)).ToString("D2");
                        soundSet.SetSFXVolume(soundSet.defaultSFXVolume);
                        for (int i = 0; i < 10; i++)
                        {
                            if (Mathf.RoundToInt((soundSet.defaultSFXVolume * 10)) > i)
                                SFX.transform.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            else
                                SFX.transform.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);
                        }
                        break;
                }
            }
        }
        public void AButton()
        {
            if (!Tutorial.activeSelf)
            {
                if (Set.activeSelf)
                    SelectSetButton(SetNum);
                else if (MainGame.activeSelf)
                    SelectMainButton(MainNum);
                if (End.activeSelf)
                    SelectSEndButton(EndNum);
            }
        }
        public void BButton()
        {
            MainNum = SetNum = TutorialNum = EndNum = 0;
            if (Set.activeSelf)
            {
                if (Tutorial.activeSelf)
                    Tutorial.SetActive(false);
                else
                    Set.SetActive(false);
            }
        }
        IEnumerator GamePlay()
        {
            Debug.Log("Game");
            Main.Play("CoverDown");
            yield return new WaitForSecondsRealtime(Main.GetCurrentAnimatorStateInfo(0).length);
            SceneManager.LoadScene("ColorMacthGame", LoadSceneMode.Single);
        }
        public void GameQuit()
        {
            Debug.Log("Á¾·á µÊ");
            Application.Quit();
        }
        public void SetButton()
        {
            MainNum = 0;
            Set.SetActive(!Set.activeSelf);
        }
        public void GameOut()
        {
            gameController.EndGameUI.SetActive(false);
            StartCoroutine(SceneLoad());
        }
        IEnumerator SceneLoad()
        {
            yield return new WaitForSecondsRealtime(0.2f);
            SceneManager.LoadScene("ColorMacthMain", LoadSceneMode.Single);
        }
        public void ReGame()
        {
            gameController.EndGameUI.SetActive(false);
            StartCoroutine(GamePlay());
        }
    }
}