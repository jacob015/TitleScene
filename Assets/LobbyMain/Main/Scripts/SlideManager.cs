using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SlideManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform panelContainer;
    public RectTransform[] pages; 
    private int currentPage = 1;
    private float slideDuration = 0.5f; 
    private Vector2 dragStartPos;

    void Start()
    {
        UpdatePanelPosition();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragStartPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 실시간으로 따라 움직이게 하고 싶다면 여기서 panelContainer.anchoredPosition 업데이트 가능
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float dragDelta = eventData.position.x - dragStartPos.x;
        float threshold = Screen.width * 0.2f; // 화면 너비의 20%를 이동 기준으로 설정

        if (Mathf.Abs(dragDelta) > threshold) 
        {
            if (dragDelta < 0 && currentPage < pages.Length - 1) 
            {
                currentPage++;
            }
            else if (dragDelta > 0 && currentPage > 0) 
            {
                currentPage--;
            }
        }

        UpdatePanelPosition();
    }

    void UpdatePanelPosition()
    {
        Vector2 targetPos = new Vector2(-pages[currentPage].anchoredPosition.x, 0);
        panelContainer.DOAnchorPos(targetPos, slideDuration).SetEase(Ease.OutCubic);
    }
}