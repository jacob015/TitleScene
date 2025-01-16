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
        private GameObject dragCopy;  // ����� ������Ʈ�� ������ ����

        void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // ���� ������Ʈ�� �����Ͽ� �巡���� ������Ʈ ����
            dragCopy = Instantiate(gameObject, canvas.transform);
            dragCopy.transform.SetAsLastSibling();  // �巡�׵� ������Ʈ�� �� �տ� ���̵��� ����

            // ����� ������Ʈ���� �ʿ��� ������Ʈ���� ������
            rectTransform = dragCopy.GetComponent<RectTransform>();
            canvasGroup = dragCopy.GetComponent<CanvasGroup>();

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0.6f;  // ����� ������Ʈ ���� ����
                canvasGroup.blocksRaycasts = false;  // �巡�� �� ����ĳ��Ʈ ����
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (rectTransform != null)
            {
                // ����� ������Ʈ�� �巡��
                rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1.0f;  // �巡�� ���� �� ���� ����
                canvasGroup.blocksRaycasts = true;  // ����ĳ��Ʈ �ٽ� Ȱ��ȭ
            }

            // �巡�װ� ������ ����� ������Ʈ ����
            if (dragCopy != null)
            {
                Destroy(dragCopy);
            }
        }
    }
}