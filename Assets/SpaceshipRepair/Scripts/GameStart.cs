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
        // ���� ���� ���� ����
        Debug.Log("������ ���۵Ǿ����ϴ�.");
        // Ÿ�� ���� ���� ���� �� �߰�
        FindObjectOfType<SpaceShipRepair.TimerAndScore>().StartTimer();
    }
}
