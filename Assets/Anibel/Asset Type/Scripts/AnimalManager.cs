using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using DG.Tweening;
using UnityEngine;

namespace Anibel
{
    public class AnimalManager : SingletonMonoBehaviour<AnimalManager>
    {
        public Transform[] pos;
        public GameObject[] Animals;
        public List<WeightedItem<AnimalData>> _datas = new List<WeightedItem<AnimalData>>();
        public GameObject BackGround;

        private int animalCount = 0;
        private ScoreSet _scoreSet;
        private PenaltyGauge _penaltyGauge;
        private ComboSet _comboSet;
        public JudgeArea _judgeArea;

        private void Start()
        {
            _scoreSet = gameObject.GetComponent<ScoreSet>();
            _penaltyGauge = gameObject.GetComponent<PenaltyGauge>();
            _comboSet = gameObject.GetComponent<ComboSet>();
            for (int i = 0; i < Animals.Length; i++)
            {
                Animals[i].transform.position = pos[i].position;
                Animals[i].GetComponent<AnimalMove>().CurrentPos = i;
                Animals[i].GetComponent<AnimalMove>().Data = WeightedRandomUtility.GetWeightedRandom(_datas);
                Animals[i].GetComponent<AnimalMove>().SetData();
            }
        }

        public void Surprise()
        {
            foreach (var animal in Animals)
            {
                if (animal.GetComponent<AnimalMove>().CurrentPos == 5)
                {
                    animal.GetComponent<AnimalMove>().isSurprised = true;
                    animal.GetComponent<AnimalMove>().SetData();
                }
            }
        }

        public void AnimalsStack() // 쌓일 때 실행
        {
            foreach (var animal in Animals)
            {
                if (animal.GetComponent<AnimalMove>().IsMove)
                    return;
            }

            foreach (var animal in Animals)
            {
                animal.GetComponent<AnimalMove>().CurrentPos++;
                if (animal.GetComponent<AnimalMove>().CurrentPos >= 7)
                {
                    animal.GetComponent<AnimalMove>().CurrentPos = 0;
                    animal.GetComponent<AnimalMove>().isSurprised = false;
                    animal.GetComponent<AnimalMove>().Data = WeightedRandomUtility.GetWeightedRandom(_datas);
                    animal.transform.position = pos[0].position;
                }
                else
                {
                    animal.GetComponent<AnimalMove>().Move();
                }

                animal.GetComponent<AnimalMove>().SetData();
            }

            BackGround.transform.DOMoveY(BackGround.transform.position.y - 1.0f, 0.3f);
            _comboSet.ComboUp();
            AnimalScore();
            AnimalCount();
        }

        public void AnimalsDump() // 버려졌을 때 실행
        {
            foreach (var animal in Animals)
            {
                if (animal.GetComponent<AnimalMove>().IsMove)
                    return;
            }

            foreach (var animal in Animals)
            {
                if (animal.GetComponent<AnimalMove>().CurrentPos == 3)
                {
                    string[] animalName = animal.GetComponent<AnimalMove>().Data.name.Split('.');
                    if (animalName[0] != "Wolf")
                    {
                        SoundManager.Instance.PlaySFX("AnimalPunchFail");
                        _penaltyGauge.PenaltyStack();
                        _comboSet.ResetCombo();
                    }
                    else
                    {
                        if (_judgeArea.GameOverCount == 0 && animalName[1] != "Bomb")
                        {
                            SoundManager.Instance.PlaySFX("AnimalPunchFail2");
                            _comboSet.ResetCombo();
                        }
                        else
                        {
                            SoundManager.Instance.PlaySFX("AnimalPunchFail3");
                            AnimalScore();
                            _comboSet.ComboUp();
                        }
                    }

                    animal.GetComponent<AnimalMove>().Dump();
                }
                else if (animal.GetComponent<AnimalMove>().CurrentPos <= 2)
                {
                    animal.GetComponent<AnimalMove>().CurrentPos++;
                    animal.GetComponent<AnimalMove>().Move();
                    animal.GetComponent<AnimalMove>().SetData();
                }
            }

            AnimalCount();
        }

        private void AnimalCount()
        {
            animalCount++;
            TimeGauge.Instance.ResetGauge();
            if (animalCount % 5 == 0) // animal 블럭 갯수가 5개씩 지나갈때마다 TimeGauge를 0.98배 줄인다.
            {
                TimeGauge.Instance.DecreaseDuration();
            }
        }

        private void AnimalScore()
        {
            _scoreSet.ScoreUp();
        }
    }
}