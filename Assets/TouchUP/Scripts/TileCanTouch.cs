using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TouchUp
{
    [RequireComponent(typeof(Data))]
    public class TileCanTouch : MonoBehaviour, IPointerClickHandler
    {
        private TileData _tileData;

        private void OnEnable()
        {
            _tileData = GetComponent<Data>()._TileData;
            GetComponent<Image>().sprite = _tileData.sprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            TileManager.Instance.TileTouch(_tileData);
        }
    }
}
