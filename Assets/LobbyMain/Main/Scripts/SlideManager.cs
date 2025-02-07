using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class SlideManager : MonoBehaviour
{
    /*public RectTransform panelContainer; 
    public RectTransform[] pages; 
    private int currentPage = 0;
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
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float dragDelta = eventData.position.x - dragStartPos.x; 
        float threshold = Screen.width * 0.2f; 

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
    }*/
    
    public RectTransform panelContainer;
    public RectTransform[] pages; 
    public Button nextButton; 
    public Button prevButton; 

    private int currentPage = 0;
    private float slideDuration = 0.3f; 
    private float pageWidth;

    void Start()
    {
        if (pages.Length == 0) return;

        pageWidth = pages[0].rect.width; 
        panelContainer.anchoredPosition = new Vector2(0, panelContainer.anchoredPosition.y); 

        nextButton.onClick.AddListener(MoveNext);
        prevButton.onClick.AddListener(MovePrevious);
        Debug.Log("Page Width: " + pageWidth);
        Debug.Log("Panel Initial Position: " + panelContainer.anchoredPosition);
    }

    public void MoveNext()
    {
        if (currentPage < 1)
        {
            currentPage++;
            UpdatePanelPosition();
        }
    }

    public void MovePrevious()
    {
        if (currentPage > -1)
        {
            currentPage--;
            UpdatePanelPosition();
        }
    }

    void UpdatePanelPosition()
    {
        Debug.Log("Moving to page: " + currentPage);
        float targetX = -currentPage * pageWidth; // 페이지 너비만큼 이동
        Vector2 targetPos = new Vector2(targetX, panelContainer.anchoredPosition.y);
        panelContainer.DOAnchorPos(targetPos, slideDuration).SetEase(Ease.OutCubic);
    }
}