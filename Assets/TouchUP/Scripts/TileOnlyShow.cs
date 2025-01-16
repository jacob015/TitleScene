using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TouchUp
{
    [RequireComponent(typeof(Data))]
    public class TileOnlyShow : MonoBehaviour
    {
        private TileData tileData;
        private Image _image;

        private void OnEnable()
        {
            tileData = GetComponent<Data>()._TileData;
            _image = GetComponent<Image>();
            _image.sprite = tileData.EmptySprite;
        }

        public void RightTile()
        {
            _image.sprite = tileData.sprite;
        }

        public void WrongTile()
        {
            _image.sprite = tileData.EmptySprite;
        }
    }
}