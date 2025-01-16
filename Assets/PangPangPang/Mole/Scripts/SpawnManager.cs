using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PangPangPang
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private List<Hole> _holeList;

        private float _spawnDelayTime; //���� ���� Ȯ��
        [SerializeField] private float _spawnDelay; //���� ����(s)
        [SerializeField] private float _subSpawnDelay; //���� ���� ���ҷ�(s)

        private float _subSpawnDelayTime; //���� ���� ���� �ð� Ȯ��
        [SerializeField] private float _subSpawnDelayMaxTime; //���� ���� ���� �ð�(s) (�� �ð� ��ŭ ������ ���� ������ ������)


        //[SerializeField] private int _spawnCountMax;   //���� ����

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

        //�δ��� ����
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
            int percent = Random.Range(0, 100); //���� ���� �δ��� ���� ����

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

            if (tempList.Count < moleCount) //���ۺ��� ���� ������ ������ �ִ밪���� ��ȯ
            {
                moleCount = tempList.Count;
            }

            //���� ��ȣ�� ���� ���� �δ��� ����
            if (tempList.Count > 0) 
            {
                Hole tempHole = tempList[Random.Range(0, tempList.Count)];
                tempHole.Execute(TrafficLightController.curColorType);
                tempList.Remove(tempHole);
            }
            //�߰� �δ��� ����

            for (int i = 0; i < moleCount-1; i++)
            {
                //����
                Hole tempHole = tempList[Random.Range(0, tempList.Count)];
                tempHole.Execute((Define.ColorType)Random.Range(0,3));
                tempList.Remove(tempHole);
            }
        }
    }
}