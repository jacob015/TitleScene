using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceFarm
{
    public class FarmManager : MonoBehaviour
    {
        [Header("ÀÌº¥Æ®")]
        [SerializeField] private Define.EventType _eventType;
        public Define.EventType EventType
        {
            get => _eventType;
            set
            {
                _eventType = value;
                switch (_eventType)
                {
                    case Define.EventType.ASTEROID:
                        _eventImage.sprite = _eventImages[0];

                        CanvasGroup cg1 = UIManager.Instance.GetUI<CanvasGroup>("AsteroidEventBackground");
                        cg1.FadeOfCanvasGroup(1, 1);
                        StartCoroutine(RemoveEventPanel(cg1, 5f));
                        break;
                    case Define.EventType.COMET:
                        _eventImage.sprite = _eventImages[1];

                        CanvasGroup cg2 = UIManager.Instance.GetUI<CanvasGroup>("CometEventBackground");
                        cg2.FadeOfCanvasGroup(1, 1);
                        StartCoroutine(RemoveEventPanel(cg2, 5f));
                        break;
                    case Define.EventType.SUPERNOVA:
                        _eventImage.sprite = _eventImages[2];

                        CanvasGroup cg3 = UIManager.Instance.GetUI<CanvasGroup>("SupernovaEventBackground");
                        cg3.FadeOfCanvasGroup(1, 1);
                        StartCoroutine(RemoveEventPanel(cg3, 5f));
                        break;
                    case Define.EventType.NONE:
                        _eventImage.sprite = _eventImages[3];
                        break;
                }
            }
        }

        [SerializeField] private List<float> _eventTime = new List<float>();
        private int _eventIndex = 0;
        [SerializeField] private List<Sprite> _eventImages = new List<Sprite>();
        [SerializeField] private Image _eventImage;

        [Header("¹ç")]
        [SerializeField] private List<Image> _seedGrounds = new List<Image>();

        [Header("¾¾¾Ñ")]
        [SerializeField] private List<SeedData> _seedDatas = new List<SeedData>();

        [SerializeField] private List<SeedPointList> _seedPointList;

        [Header("ÇÞºû")]
        [SerializeField] private Define.SunLightPoint _curSunLightPoint;

        [SerializeField] private Scrollbar _sunShield;

        private Dictionary<Define.SunLightPoint, Vector2> _sunLightPoints = new Dictionary<Define.SunLightPoint, Vector2>();

        private bool isChanged = false;

        private void Start()
        {
            _sunLightPoints.Add(Define.SunLightPoint.LEFT, new Vector2(0, 1));
            _sunLightPoints.Add(Define.SunLightPoint.CENTER, new Vector2(1, 2));
            _sunLightPoints.Add(Define.SunLightPoint.RIGHT, new Vector2(2, 3));

            _sunShield?.onValueChanged.AddListener((value) => 
            {
                if (value == 1)
                {
                    _curSunLightPoint = Define.SunLightPoint.RIGHT;
                }
                else if (value == 0.5f)
                {
                    _curSunLightPoint = Define.SunLightPoint.CENTER;
                }
                else
                {
                    _curSunLightPoint = Define.SunLightPoint.LEFT;
                }
            });
        }

        private void Update()
        {
            if (GameManager.Instance.CurGameState != Define.GameState.PLAY) return;

            switch(_eventType)
            {
                case Define.EventType.ASTEROID:
                    for (int i = 0; i < _seedPointList.Count; i++)
                    {
                        ChangeGroundColor(i, Color.gray);
                        _seedPointList[i].SetSunLight(Define.SunLightState.OFF);
                    }
                    break;
                case Define.EventType.COMET:
                    for (int i = 0; i < _seedPointList.Count; i++)
                    {
                        ChangeGroundColor(i, Color.white);
                        _seedPointList[i].SetSunLight(Define.SunLightState.ON);
                    }
                    break;
                case Define.EventType.SUPERNOVA:

                    if(!isChanged)
                    {
                        for (int i = 0; i < _seedPointList.Count; i++)
                        {
                            _seedPointList[i].SetRandomSeedData(_seedDatas);
                        }
                        isChanged = true;
                    }

                    for (int i = 0; i < _seedPointList.Count; i++)
                    {
                        if (i == _sunLightPoints[_curSunLightPoint].x || i == _sunLightPoints[_curSunLightPoint].y)
                        {
                            ChangeGroundColor(i, Color.gray);
                            _seedPointList[i].SetSunLight(Define.SunLightState.OFF);
                        }
                        else
                        {
                            ChangeGroundColor(i, Color.white);
                            _seedPointList[i].SetSunLight(Define.SunLightState.ON);
                        }
                    }

                    break;
                case Define.EventType.NONE:

                    if (isChanged)
                    {
                        isChanged = false;
                    }

                    for (int i = 0; i < _seedPointList.Count; i++)
                    {
                        if (i == _sunLightPoints[_curSunLightPoint].x || i == _sunLightPoints[_curSunLightPoint].y)
                        {
                            ChangeGroundColor(i, Color.gray);
                            _seedPointList[i].SetSunLight(Define.SunLightState.OFF);
                        }
                        else
                        {
                            ChangeGroundColor(i, Color.white);
                            _seedPointList[i].SetSunLight(Define.SunLightState.ON);
                        }
                    }
                    break;
            }
           
        }

        public void ChangeEvent(float timer)
        {
            if (_eventIndex >= _eventTime.Count)
            {
                return;
            }

            if (_eventTime[_eventIndex] >= timer)
            {
                EventType = (Define.EventType)Random.Range(0, 3);
                //EventType = Define.EventType.SUPERNOVA;
                _eventIndex++;

                StartCoroutine(InitEvent());
            }
        }

        private IEnumerator InitEvent()
        {
            float timer = 0;

            while (timer < 20)
            {
                if (GameManager.Instance.CurGameState == Define.GameState.PLAY)
                {
                    timer += Time.deltaTime;
                }

                yield return null;
            }

            EventType = Define.EventType.NONE;
        }

        private void ChangeGroundColor(int index, Color color)
        {
            if (_seedGrounds[index].color == color) return;

            _seedGrounds[index].color = color;
        }

        private IEnumerator RemoveEventPanel(CanvasGroup canvasGroup, float t)
        {
            float timer = 0;

            while (timer < t)
            {
                if (GameManager.Instance.CurGameState == Define.GameState.PLAY)
                {
                    timer += Time.deltaTime;
                }

                yield return null;
            }

            canvasGroup.FadeOfCanvasGroup(0, 1);
        }
    }
}

