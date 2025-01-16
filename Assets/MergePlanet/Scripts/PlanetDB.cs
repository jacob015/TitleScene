using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlanetMerge
{
    public class PlanetDB : MonoBehaviour
    {
        public static PlanetDB instance;
        private void Awake()
        {
            instance = this;
        }

        [System.Serializable]
        public class PlanetInfo
        {
            public string Name;
            public int Level;
            public float Size;
            public Sprite Image;
            public int score;
        }
        [SerializeField]
        public List<PlanetInfo> PlanetDatas = new List<PlanetInfo>();
    }
}
