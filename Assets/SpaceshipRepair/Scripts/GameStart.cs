using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public float countdownTime = 3.0f;
    private float currentTime;
    private bool gameStarted = false;

    void Start()
    {
        currentTime = countdownTime;
    }

    void Update()
    {
        if (!gameStarted)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                gameStarted = true;
                StartGame();
            }
        }
    }

    void StartGame()
    {
        // 게임 시작 로직 구현
        Debug.Log("게임이 시작되었습니다.");
        // 타임 어택 시작 로직 등 추가
        FindObjectOfType<SpaceShipRepair.TimerAndScore>().StartTimer();
    }
}
