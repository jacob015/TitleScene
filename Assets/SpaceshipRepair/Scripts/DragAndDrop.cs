using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SpaceShipRepair
{
    public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Canvas canvas;
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private GameObject dragCopy;  // 복사된 오브젝트를 저장할 변수

        void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // 현재 오브젝트를 복사하여 드래그할 오브젝트 생성
            dragCopy = Instantiate(gameObject, canvas.transform);
            dragCopy.transform.SetAsLastSibling();  // 드래그된 오브젝트가 맨 앞에 보이도록 설정

            // 복사된 오브젝트에서 필요한 컴포넌트들을 가져옴
            rectTransform = dragCopy.GetComponent<RectTransform>();
            canvasGroup = dragCopy.GetComponent<CanvasGroup>();

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0.6f;  // 복사된 오브젝트 투명도 설정
                canvasGroup.blocksRaycasts = false;  // 드래그 중 레이캐스트 차단
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (rectTransform != null)
            {
                // 복사된 오브젝트를 드래그
                rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1.0f;  // 드래그 종료 후 투명도 복구
                canvasGroup.blocksRaycasts = true;  // 레이캐스트 다시 활성화
            }

            // 드래그가 끝나면 복사된 오브젝트 삭제
            if (dragCopy != null)
            {
                Destroy(dragCopy);
            }
        }
    }
}