using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PangPangPang
{
    public class TrafficLightController : MonoBehaviour
    {
        public static Define.ColorType curColorType = Define.ColorType.NONE;

        [SerializeField] private float _changeColorTypeDelay;
        private float changeColorTypeTime;

        private Animator _ani;

        private Image _redLight;
        private Image _yellowLight;
        private Image _greenLight;

        private bool isBlinking = false;

        private void Start()
        {
            _ani = GetComponent<Animator>();

            _redLight = UIManager.Instance.GetUI<Image>("RedLight");
            _yellowLight = UIManager.Instance.GetUI<Image>("YellowLight");
            _greenLight = UIManager.Instance.GetUI<Image>("GreenLight");

            changeColorTypeTime = _changeColorTypeDelay;

            GameManager.Instance.BeginEvent += () =>
            {

            };
        }

        private void Update()
        {
            if (GameManager.Instance.GameState == Define.GameState.PLAY || GameManager.Instance.GameState == Define.GameState.FEVER)
            {
                if (_changeColorTypeDelay <= changeColorTypeTime)
                {
                    ChangeColorType();
                    changeColorTypeTime = 0;
                    isBlinking = false;
                }
                else if (_changeColorTypeDelay - changeColorTypeTime <= 1.8f)
                {
                    changeColorTypeTime += Time.deltaTime;
                    Blink();
                }
                else
                {
                    changeColorTypeTime += Time.deltaTime;
                }
            }

        }

        private void Blink()
        {
            if (isBlinking)
            {
                return;
            }

            isBlinking = true;

            switch (curColorType)
            {
                case Define.ColorType.RED:
                    _redLight.GetComponent<Animator>().SetTrigger("IsBlinking");
                    break;
                case Define.ColorType.YELLOW:
                    _yellowLight.GetComponent<Animator>().SetTrigger("IsBlinking");
                    break;
                case Define.ColorType.GREEN:
                    _greenLight.GetComponent<Animator>().SetTrigger("IsBlinking");
                    break;
            }

        }

        private void ChangeColorType()
        {
            int temp = Random.Range(0, 3);

            if (curColorType == (Define.ColorType)temp)
            {
                temp = (temp + 1) % 3;

            }

            curColorType = (Define.ColorType)temp;


            switch (curColorType)
            {
                case Define.ColorType.RED:
                    _redLight.GetComponent<Animator>().SetTrigger("IsStarting");
                    break;
                case Define.ColorType.YELLOW:
                    _yellowLight.GetComponent<Animator>().SetTrigger("IsStarting"); ;
                    break;
                case Define.ColorType.GREEN:
                    _greenLight.GetComponent<Animator>().SetTrigger("IsStarting");
                    break;

            }
        }

    }
}
