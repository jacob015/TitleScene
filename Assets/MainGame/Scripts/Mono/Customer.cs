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
        //percentage������ ���� �ð�
        if (percentage >= 80)
        {
            Debug.Log("�ð� �븸��");
            Satisfaction = 50;
        }
        else if (percentage < 0f)
        {
            Debug.Log("�ð� �Ҹ���");
            Satisfaction = 0;
        }
        else
        {
            Debug.Log("�ð� ����");
            Satisfaction = 25;
        }
        //guestdia.text = speechDB[Satisfaction + 1].Reaction;
        return Satisfaction;
    }
}
