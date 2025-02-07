using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public Dictionary<int, Guest> guests = new Dictionary<int, Guest>();

    public MakingSystem makingSystem;

    private void Awake()
    {
        makingSystem = GameObject.Find("MakingSystem").GetComponent<MakingSystem>();
    }
    public int EndOrder(float RemainingTime)
    {
        int Satisfaction = 0;
        int percentage = Mathf.RoundToInt(RemainingTime * 100f);
        Debug.Log(percentage);
        //percentage변수는 남은 시간
        if (percentage >= 80)
        {
            Debug.Log("시간 대만족");
            Satisfaction = 50;
        }
        else if (percentage < 0f)
        {
            Debug.Log("시간 불만족");
            Satisfaction = 0;
        }
        else
        {
            Debug.Log("시간 만족");
            Satisfaction = 25;
        }
        //guestdia.text = speechDB[Satisfaction + 1].Reaction;
        return Satisfaction;
    }
}
