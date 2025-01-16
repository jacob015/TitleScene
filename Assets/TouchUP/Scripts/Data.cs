using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TouchUp
{
    public class Data : MonoBehaviour
    {
        private TileData _tileData;
        public TileData _TileData
        {
            get => _tileData;
            set => _tileData = value;
        }
    }
}
