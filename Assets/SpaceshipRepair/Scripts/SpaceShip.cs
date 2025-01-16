using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShipRepair
{
    public class SpaceShip : MonoBehaviour
    {
        public enum SpaceshipType { A, B, C }
        public SpaceshipType type;

        public GameObject[] wings;
        public GameObject[] windows;
        public GameObject[] lids;
        public GameObject[] engines;

        void Start()
        {
            SetupSpaceship();
        }

        void SetupSpaceship()
        {
            switch (type)
            {
                case SpaceshipType.A:
                    // A 타입 우주선 설정
                    EnableComponents(3, 2, 1, 1);
                    break;
                case SpaceshipType.B:
                    // B 타입 우주선 설정
                    EnableComponents(3, 3, 1, 1);
                    break;
                case SpaceshipType.C:
                    // C 타입 우주선 설정
                    EnableComponents(0, 2, 3, 3);
                    break;
            }
        }

        void EnableComponents(int wingCount, int windowCount, int lidCount, int engineCount)
        {
            EnableGameObjects(wings, wingCount);
            EnableGameObjects(windows, windowCount);
            EnableGameObjects(lids, lidCount);
            EnableGameObjects(engines, engineCount);
        }

        void EnableGameObjects(GameObject[] objects, int count)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SetActive(i < count);
            }
        }
    }
}