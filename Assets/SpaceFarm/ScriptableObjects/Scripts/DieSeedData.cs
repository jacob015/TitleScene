using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceFarm
{
    [CreateAssetMenu(fileName = "DieSeedData", menuName = "Die Seed Data")]
    public class DieSeedData : SeedData
    {
        [SerializeField] private List<Sprite> _sprites = new List<Sprite>(); 
        public override Sprite FirstSprite { get => _sprites[Random.Range(0, _sprites.Count)]; }
    }
}