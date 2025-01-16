using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ColorMatch
{
    public class Spawner : MonoBehaviour
    {
        public List<Sprite> MeteoriteSprite1;
        public List<Sprite> MeteoriteSprite2;
        public List<Sprite> MeteoriteSprite3;

        int num;
        [SerializeField]
        Transform SpawnPoint;
        GameController gamecontroller;

        public int BigSpawn;
        void Start()
        {
            gamecontroller = FindObjectOfType<GameController>();
            StartCoroutine(Spawn());
        }

        IEnumerator Spawn()
        {
            int Bigran = Random.Range(0, BigSpawn);
            int ran = Random.Range(0, 3);
            Debug.Log(ran);
            yield return new WaitForSeconds(Bigran == 0 ? 2 : 1);
            GameObject obj = ObjectPool.instance.objectPoolList[0].Dequeue();
            obj.transform.localPosition = transform.GetChild(ran).position;
            if(Bigran == 0)
            {
                obj.GetComponent<Meteorite>().Bigger = true;
                gamecontroller.NextLine.Add(ran);
            }
            gamecontroller.NextLine.Add(ran);
            obj.SetActive(true);
            StartCoroutine(Spawn());
        }
    }
}