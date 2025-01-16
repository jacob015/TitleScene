using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TouchUp
{
    [CreateAssetMenu(fileName = "TileData", menuName = "Tile/Data", order = 0)]
    public class TileData : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;

        public Sprite sprite
        {
            get => _sprite;
            set => _sprite = value;
        }

        [SerializeField] private Sprite _emptySprite;

        public Sprite EmptySprite
        {
            get => _emptySprite;
            set => _emptySprite = value;
        }
    }
}