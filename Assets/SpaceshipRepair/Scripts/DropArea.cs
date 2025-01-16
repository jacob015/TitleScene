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
            // ��ӵ� ������Ʈ�� ������ ����
            GameObject droppedObject = eventData.pointerDrag;

            if (droppedObject != null)
            {
                Debug.Log(droppedObject.name + "��(��) �巡�� ������ ��ӵǾ����ϴ�!");
                // �ʿ��� ���� �߰� ���� (��: ��ӵ� ������Ʈ�� Ư�� ��ġ�� �̵�)
                FindObjectOfType<TimerAndScore>().AddScore(1);
            }
        }
    }
}