using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SpaceShipRepair
{
    public class DropArea : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            // 드롭된 오브젝트의 정보를 얻음
            GameObject droppedObject = eventData.pointerDrag;

            if (droppedObject != null)
            {
                Debug.Log(droppedObject.name + "이(가) 드래그 영역에 드롭되었습니다!");
                // 필요한 로직 추가 가능 (예: 드롭된 오브젝트를 특정 위치로 이동)
                FindObjectOfType<TimerAndScore>().AddScore(1);
            }
        }
    }
}