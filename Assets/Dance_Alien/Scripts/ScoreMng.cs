using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreMng : MonoBehaviour
{
    public static ScoreMng Instance;
    [SerializeField] Text scoreText;
    public float score;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Update()
    {
        scoreText.text = (int)score + "점";
    }
}
