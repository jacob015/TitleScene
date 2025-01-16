using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PangPangPang
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private List<Hole> _holeList;

        private float _spawnDelayTime; //스폰 간격 확인
        [SerializeField] private float _spawnDelay; //스폰 간격(s)
        [SerializeField] private float _subSpawnDelay; //스폰 간격 감소량(s)

        private float _subSpawnDelayTime; //스폰 간격 감소 시간 확인
        [SerializeField] private float _subSpawnDelayMaxTime; //스폰 간격 감소 시간(s) (이 시간 만큼 지나면 스폰 간격이 감소함)


        //[SerializeField] private int _spawnCountMax;   //스폰 개수

        [SerializeField] private float _hideTime = 2f;

        public float SpawnDelay
        {
            get { return _spawnDelay; }
            set
            {
                _spawnDelay = value;
            }
        }

        public float HideTime
        {
            get { return _hideTime; }
            set
            {
                _hideTime = value;
            }
        }
        private void Start()
        {
            _spawnDelayTime = _spawnDelay - 1;

        }

        private void Update()
        {
            if (GameManager.Instance.GameState == Define.GameState.PLAY || GameManager.Instance.GameState == Define.GameState.FEVER)
            {

                if (_subSpawnDelayMaxTime <= _subSpawnDelayTime)
                {
                    _spawnDelay -= _subSpawnDelay;
                    _subSpawnDelayTime = 0;
                }
                else
                {
                    _subSpawnDelayTime += Time.deltaTime;
                }



                if (_spawnDelay <= _spawnDelayTime)
                {

                    Spawn();
                    _spawnDelayTime = 0;
                }
                else
                {
                    _spawnDelayTime += Time.deltaTime;
                }
            }
        }

        //두더지 스폰
        private void Spawn()
        {


            List<Hole> tempList = _holeList.ToList();
            List<Hole> removeList = new List<Hole>();

            foreach (Hole h in tempList)
            {
                if (h.IsOn)
                {
                    removeList.Add(h);
                }
            }

            foreach (Hole rh in removeList)
            {
                tempList.Remove(rh);
            }

            removeList.Clear();

            int moleCount = 0;
            int percent = Random.Range(0, 100); //색이 같은 두더지 제외 개수

            if (percent >= 70)
            {
                moleCount = 1;
            }
            else if (percent >= 30)
            {
                moleCount = 2;
            }
            else
            {
                moleCount = 3;
            }

            if (tempList.Count < moleCount) //구멍보다 스폰 개수가 많으면 최대값으로 변환
            {
                moleCount = tempList.Count;
            }

            //현재 신호등 색과 같은 두더지 생성
            if (tempList.Count > 0) 
            {
                Hole tempHole = tempList[Random.Range(0, tempList.Count)];
                tempHole.Execute(TrafficLightController.curColorType);
                tempList.Remove(tempHole);
            }
            //추가 두더지 생성

            for (int i = 0; i < moleCount-1; i++)
            {
                //스폰
                Hole tempHole = tempList[Random.Range(0, tempList.Count)];
                tempHole.Execute((Define.ColorType)Random.Range(0,3));
                tempList.Remove(tempHole);
            }
        }
    }
}