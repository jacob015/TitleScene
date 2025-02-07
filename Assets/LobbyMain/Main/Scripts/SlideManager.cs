using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class SlideManager : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public RectTransform slidePanel;
    public int totalPages = 3; // 총 페이지 수
    private int currentPage = 0;
    private float screenWidth;
    private Vector2 startPosition;

    void Start()
    {
        screenWidth = Screen.width;
        UpdateSlidePosition();
    }

    public void OnDrag(PointerEventData eventData)
    {
        float delta = eventData.delta.x;
        slidePanel.anchoredPosition += new Vector2(delta, 0);
        Debug.Log("Drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float dragThreshold = screenWidth * 0.2f; // 일정 거리 이상 이동해야 페이지 변경
        if (eventData.pressPosition.x - eventData.position.x > dragThreshold)
        {
            NextPage();
        }
        else if (eventData.position.x - eventData.pressPosition.x > dragThreshold)
        {
            PreviousPage();
        }
        else
        {
            UpdateSlidePosition();
        }
    }

    private void NextPage()
    {
        currentPage = (currentPage + 1) % totalPages; // 마지막 페이지에서 첫 페이지로 루프
        UpdateSlidePosition();
    }

    private void PreviousPage()
    {
        currentPage = (currentPage - 1 + totalPages) % totalPages; // 첫 페이지에서 마지막 페이지로 루프
        UpdateSlidePosition();
    }

    private void UpdateSlidePosition()
    {
        float targetX = -currentPage * screenWidth;
        slidePanel.DOAnchorPos(new Vector2(targetX, 0), 0.5f).SetEase(Ease.OutQuad);
        Debug.Log("Slide to page " + currentPage);
    }
}
