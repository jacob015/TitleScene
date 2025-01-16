using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace PangPangPang
{
    public class Mole : MonoBehaviour, IPointerClickHandler
    {
        public enum MoleState
        {
            UNDER, ON, NONE
        }

        public MoleState state = MoleState.UNDER;
        [SerializeField] private Define.ColorType colorType;

        private Animator _ani;

        public Action endHiddenEvent;

        private RectTransform _ground;
        private Transform _holeMask;

        private float hideTime = 0;
        private void Awake()
        {
            _ani = GetComponent<Animator>();
            _ground = GameObject.Find("BackGround").GetComponent<RectTransform>();
            _holeMask = transform.parent;
        }

        private void Update()
        {
            if (state == MoleState.ON)
            {
                if (hideTime <= 0)
                {
                    Hide();
                }
                else
                {
                    hideTime -= Time.deltaTime;
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)//올라온 두더지 선택
        {
            if (state != MoleState.ON)
            {
                return;
            }

            Transform mainCanvas = FindObjectOfType<Canvas>().transform;

            Vector2 spawnPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvas.transform as RectTransform,
                                                                                       eventData.position,
                                                                                              Camera.main,
                                                                                        out spawnPosition);

            if (TrafficLightController.curColorType == colorType)
            {
                int tempValue = UnityEngine.Random.Range(0, 2);
                switch (tempValue)
                {
                    case 0:
                        SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_Scream01");
                        break;
                    case 1:
                        SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_Scream02");
                        break;
                }

                GameManager.Instance.Score++;

                GameObject pang = GameManager.Instance.CreateEffect(Define.EffectType.PANG);
                GameObject temp = Instantiate(pang, mainCanvas);
                temp.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
                temp.transform.SetParent(GameObject.Find("GamePanel").transform);
            }
            else
            {
                SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_Scream03");

                GameManager.Instance.MissCount--;

                GameObject miss = GameManager.Instance.CreateEffect(Define.EffectType.MISS);
                GameObject temp = Instantiate(miss, mainCanvas);
                temp.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
                temp.transform.SetParent(GameObject.Find("GamePanel").transform);
            }

            
            _ground.ToMoveRectTransform(Vector2.zero, new Vector2(_ground.anchoredPosition.x, _ground.anchoredPosition.y - 30), 0.2f, true);

            //Hide();

            hideTime = 0;
        }

        public void Raise()
        {
            //올라오는 애니메이션 실행
            _ani.SetTrigger("IsOn");
            state = MoleState.ON;

            hideTime = FindObjectOfType<SpawnManager>().HideTime;
        }

        private void Hide()
        {
            //숨는 애니메이션 실행
            _ani.SetTrigger("IsUnder");
            state = MoleState.UNDER;
        }


        private void FinishHide()
        {
            endHiddenEvent?.Invoke();

            endHiddenEvent = null;
        }

        private void MaskActive(int value)
        {
            if (value == 0)
            {
                _holeMask.GetComponent<Image>().enabled = false;
            }
            else
            {
                _holeMask.GetComponent<Image>().enabled = true;
            }

        }
    }
}

