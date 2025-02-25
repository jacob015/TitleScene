using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace PlanetMerge
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager instance;

        public int defaultCapacity = 10;
        public int maxPoolSize = 50;
        public GameObject plants;
        public GameObject pool;

        public IObjectPool<GameObject> Pool { get; private set; }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);


            Init();
        }

        private void Init()
        {
            Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
            OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);
            for (int i = 0; i < defaultCapacity; i++)
            {
                Planet planet = CreatePooledItem().GetComponent<Planet>();
                planet.Pool.Release(planet.gameObject);
            }
        }
        private GameObject CreatePooledItem()
        {
            GameObject poolGo = Instantiate(plants, pool.transform);
            poolGo.GetComponent<Planet>().Pool = this.Pool;
            return poolGo;
        }
        private void OnTakeFromPool(GameObject poolGo)
        {
            poolGo.SetActive(true);
        }
        private void OnReturnedToPool(GameObject poolGo)
        {
            poolGo.SetActive(false);
        }
        private void OnDestroyPoolObject(GameObject poolGo)
        {
            Destroy(poolGo);
        }
    }
}