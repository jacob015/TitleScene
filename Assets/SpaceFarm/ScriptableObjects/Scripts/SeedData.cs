using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceFarm
{
    [System.Serializable]
    public class SeedState
    {
        public Define.SunLightState goal;
        public float maxTime;
        public Sprite sprite;
    }

    [CreateAssetMenu(fileName = "SeedData", menuName = "Seed Data")]
    public class SeedData : ScriptableObject
    {
        public float score;
        [SerializeField] protected Sprite _firstSprite;
        public virtual Sprite FirstSprite { get => _firstSprite; }
        public List<SeedState> seedStateList = new List<SeedState>();

     
    }
}
