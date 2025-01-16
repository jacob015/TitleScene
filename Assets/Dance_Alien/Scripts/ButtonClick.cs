using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Audio;

public class ButtonClick : MonoBehaviour
{
    public int buttonCount;
    public Image BgmImage;
    public Image EffectImage;
    public Sprite OnSprite;
    public Sprite OffSprite;
    public AudioMixer Bgm;
    bool isChange;
    bool isChange2;

    public void BgmButtonChange()
    {
        if(isChange == false)
        {
            isChange = true;
            Bgm.SetFloat("BGM", -80);
            BgmImage.sprite = OffSprite;
        }
        else
        {
            isChange = false;
            Bgm.SetFloat("BGM", 0);
            BgmImage.sprite = OnSprite;
        }
    }
    public void EffectButtonChange()
    {
        if (isChange2 == false)
        {
            isChange2 = true;
            Bgm.SetFloat("Effect", -80);
            EffectImage.sprite = OffSprite;
        }
        else
        {
            isChange2 = false;
            Bgm.SetFloat("Effect", 0);
            EffectImage.sprite = OnSprite;
        }
    }

    public void ClickDown(int count)
    {
        GameMng.Instance.buttonCount = count;
        GameMng.Instance.isClick = true;
    }

    public void ClickUp()
    {
        GameMng.Instance.buttonCount = 0;
        GameMng.Instance.isClick = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("InGame");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GameObjectActive(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void GameObjectDeaActive(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ResetScore()
    {
        Destroy(ScoreMng.Instance.gameObject);
    }
}
