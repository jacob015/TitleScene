using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace SpaceFarm
{
    public class UIDragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {

        private RectTransform _rectTransform;
        private Canvas _canvas;
        private Vector2 _startPoint;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //Debug.Log("Ω√¿€");
            _startPoint = _rectTransform.anchoredPosition;
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //Debug.Log("≥°");
            _rectTransform.anchoredPosition = _startPoint;
            ExecuteAction(eventData);
        }

        private void ExecuteAction(PointerEventData eventData)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject != gameObject && result.gameObject.GetComponent<SeedPoint>() != null)
                {
                    GetComponent<ActionImage>().Execute(result.gameObject.transform);
                    break;
                }
            }
        }
    }
}