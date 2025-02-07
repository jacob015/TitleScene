using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SwipeManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    public int swipeCount = 0; // �������� ī��Ʈ
    public float swipeThreshold = 50f; // �ּ� �������� �Ÿ�

    public void OnPointerDown(PointerEventData eventData)
    {
        startTouchPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        endTouchPosition = eventData.position;
        float swipeDistance = endTouchPosition.x - startTouchPosition.x;

        if (Mathf.Abs(swipeDistance) > swipeThreshold)
        {
            if (swipeDistance > 0)
            {
                swipeCount++;
                Debug.Log("������ ��������, ���� ��: " + swipeCount);
            }
            else
            {
                swipeCount--;
                Debug.Log("���� ��������, ���� ��: " + swipeCount);
            }
        }
    }
}
