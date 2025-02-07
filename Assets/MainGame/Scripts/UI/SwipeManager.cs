using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SwipeManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    public int swipeCount = 0; // 스와이프 카운트
    public float swipeThreshold = 50f; // 최소 스와이프 거리

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
                Debug.Log("오른쪽 스와이프, 현재 값: " + swipeCount);
            }
            else
            {
                swipeCount--;
                Debug.Log("왼쪽 스와이프, 현재 값: " + swipeCount);
            }
        }
    }
}
