using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SpaceFarm
{
    public class SeedPoint : MonoBehaviour
    {
        private Animator _ani;

        private SeedData _seedData;
        public SeedData SeedData
        {
            get => _seedData;
            set
            {
                _seedData = value;

                if (_seedData == null)
                {
                    GetComponent<Image>().color = new Color(1,1,1,1);
                    _seedImg.enabled = false;
                    _ani.SetBool("Move", false);              
                }
                else
                {
                    GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    _seedImg.enabled = true;

                    if (_seedData.seedStateList[0].goal == Define.SunLightState.NONE)
                    {
                        _ani.SetBool("Move", false);
                    }
                    else
                    {
                        _ani.SetBool("Move", true);
                    }
                }
            }
        }

        private Define.SunLightState _curSunLightState = Define.SunLightState.OFF;
        public Define.SunLightState SunLightState { get => _curSunLightState; set => _curSunLightState = value; }

       

        private Define.GrowthState _seedState = Define.GrowthState.GROWTH;
        public Define.GrowthState SeedState { get => _seedState; }

        private float _sunLightTimer = 0;
        public float SunLightTimer { get => _sunLightTimer; set => _sunLightTimer = value; }
        private int _index = 0;
        public int Index { get => _index; set => _index = value; }

        [SerializeField] private Image _growthBar;
        public Image GrowthBar { get => _growthBar; }
        [SerializeField] private Image _seedImg;
        private void Start()
        {
            _ani = GetComponent<Animator>();
            Init();
        }

        private void Update()
        {
            if (GameManager.Instance.CurGameState != Define.GameState.PLAY) return;
   
            SetSunLight();           
        }

        public void UpdateSeed(Sprite sprite)
        {
            _seedImg.sprite = sprite;
        }


        public void Init()
        {
            _seedState = Define.GrowthState.GROWTH;
            _sunLightTimer = 0;
            _growthBar.fillAmount = 0;
            _index = 0;
        }

        private void SetSunLight() // 햇빛 조건이 맞으면 실행하는 함수
        {
            if (_seedState == Define.GrowthState.FINISH || _seedData == null)
            {
                return;
            }

            if (_seedData.seedStateList[_index].goal == _curSunLightState)
            {
                if (_sunLightTimer >= _seedData.seedStateList[_index].maxTime)
                {
                    Growth();
                }
                else
                {
                    _sunLightTimer += Time.deltaTime;
                    _growthBar.fillAmount = _sunLightTimer / _seedData.seedStateList[_index].maxTime;
                }
                
            }
        }

        private void Growth() // 성장 함수
        {
            UpdateSeed(_seedData.seedStateList[_index].sprite);
            _growthBar.fillAmount = 0;

            _index++;

            if (_index == _seedData.seedStateList.Count)
            {
                SoundManager.Instance.PlaySFX("SFX_Finish");
                _seedState = Define.GrowthState.FINISH;
            }
            else
            {
                SoundManager.Instance.PlaySFX("SFX_Growth");
                _seedState = Define.GrowthState.GROWTH;
                _sunLightTimer = 0;
            }
        }

      
    }
}