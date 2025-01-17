using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PangPangPang
{
    public class GameManager : Singleton<GameManager>
    {
        private int _score;
        private int _bestScore;

        [SerializeField] private int _missCountMax = 3;
        private int _missCount;
        [SerializeField] private float _playTimeMax = 90; //초(s)
        private float _playTime; //초(s)
        private Define.GameState _gameState = Define.GameState.PAUSE;
        private Define.GameState _lastGameState;

        private bool _isNewBestScore = false;
        private bool _startFever = false;

        //UI
        private TextMeshProUGUI _scoreText;
        private TextMeshProUGUI _missCountText;
        private Image _playtimeImage;
        private Image _playtimeIcon;


        private TextMeshProUGUI _finishBestScoreText;
        private TextMeshProUGUI _newBestScoreText;

        public Action BeginEvent = null;
        public Action FinishEvent = null;


        [Header("EffectPrefabs")]
        [SerializeField] private GameObject _pangEffect;
        [SerializeField] private GameObject _missEffect;
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;

                if (_bestScore < _score)
                {
                    _bestScore = _score;

                    _isNewBestScore = true;


                }

                ShowScore();
            }
        }

        public int BestScore
        {
            get
            {
                return _bestScore;
            }

        }

        public int MissCount
        {
            get
            {
                return _missCount;
            }
            set
            {
                _missCount = value;

                if (_missCount <= 0)
                {
                    _missCount = 0;
                    GameState = Define.GameState.FINISH;
                }

                ShowMissCount();
            }
        }

        public float PlayTime
        {
            get
            {
                return _playTime;
            }

        }

        public Define.GameState GameState
        {
            get
            {
                return _gameState;
            }
            set
            {
                _lastGameState = _gameState;
                _gameState = value;

                if (_gameState == Define.GameState.END)
                {
                    FinishGame(); // 게임 끝
                }
            }
        }
        private void Awake()
        {
            _playTime = _playTimeMax;
            _missCount = _missCountMax;
        }

        private void Start()
        {
            _scoreText = UIManager.Instance.GetUI<TextMeshProUGUI>("ScoreText");
            _missCountText = UIManager.Instance.GetUI<TextMeshProUGUI>("MissCountText");

            _newBestScoreText = UIManager.Instance.GetUI<TextMeshProUGUI>("NewBestScoreText");
            _newBestScoreText.enabled = false;
            _newBestScoreText.GetComponent<Animator>().enabled = false;

            _playtimeImage = UIManager.Instance.GetUI<Image>("PlayTimer");
            _playtimeIcon = UIManager.Instance.GetUI<Image>("PlayTimerIcon");


            _finishBestScoreText = UIManager.Instance.GetUI<TextMeshProUGUI>("FinishBestScoreText");

            _missCountText.text = _missCount.ToString();
        }

        private void Update()
        {

            switch (GameState)
            {
                case Define.GameState.BEGIN:
                    BeginEvent?.Invoke();
                    GameState = Define.GameState.PLAY;
                    break;
                case Define.GameState.PLAY:
                    if (_playTime <= 0)
                    {
                        _playTime = 0;
                        _playtimeIcon.GetComponent<Animator>().SetBool("IsBlinking", false);
                        GameState = Define.GameState.FINISH;
                    }
                    else
                    {
                        if (!_startFever && _playTime <= 30)
                        {
                            GameState = Define.GameState.FEVER;
                        }

                        _playTime -= Time.deltaTime;
                        ShowPlayTime();
                    }
                    break;
                case Define.GameState.PAUSE:


                    break;
                case Define.GameState.FEVER:

                    if (!_startFever)
                    {

                        SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_Fever");

                        CanvasGroup feverPanel = UIManager.Instance.GetUI<CanvasGroup>("FeverPanel");
                        feverPanel.SetActiveOfCanvasGroup(true);
                        feverPanel.GetComponent<Animator>().enabled = true;

                        _startFever = true;

                        Invoke("InFever", 0.5f);
                    }


                    break;
                case Define.GameState.FINISH:
                    FinishEvent?.Invoke();

                    SoundManager.Instance.SetPitch(Define.AudioType.BGM, 1f);
                    GameState = Define.GameState.END;
                    break;
            }

        }

        private void FinishGame() //게임 끝났을 때 실행하는 메서드
        {
            SoundManager.Instance.StopAll();
            SoundManager.Instance.Play(Define.AudioType.SFX, "SFX_Open");

            if (_isNewBestScore)
            {
                _newBestScoreText.enabled = true;
                _newBestScoreText.GetComponent<Animator>().enabled = true;
            }

            _finishBestScoreText.text = GameManager.Instance.BestScore.ToString();

            UIManager.Instance.GetUI<CanvasGroup>("FinishPanel").GetComponent<FinishPanel>().OnFinishPanel();
            UIManager.Instance.GetUI<CanvasGroup>("FinishPanel").FadeOfCanvasGroup(1, 0.2f, () =>
            {
                UIManager.Instance.GetUI<CanvasGroup>("FinishPanel").GetComponent<FinishPanel>().FinishAnimaion();
            });

        }

        public GameObject CreateEffect(Define.EffectType type)
        {
            switch (type)
            {
                case Define.EffectType.PANG:

                    return _pangEffect;
                case Define.EffectType.MISS:
                    return _missEffect;
                default:
                    return null;

            }
        }

        private void InFever()
        {

            SoundManager.Instance.SetPitch(Define.AudioType.BGM, 1.2f);
            SoundManager.Instance.Play(Define.AudioType.BGM, "BGM_GamePlay");

            // 스폰 값 변경
            FindObjectOfType<SpawnManager>().SpawnDelay = 0.4f;
            FindObjectOfType<SpawnManager>().HideTime = 1;

            //Time.timeScale = 1;
            GameState = Define.GameState.PLAY;
        }

        public void Pause(bool value)
        {
            if (value)
            {
                GameState = Define.GameState.PAUSE;
            }
            else
            {
                GameState = _lastGameState;
            }
        }

        #region UI

        private void ShowScore()
        {
            _scoreText.transform.ChangeScale(new Vector3(1.2f, 1.2f, 1), 0.2f, true);
            _scoreText.text = _score.ToString();
        }

        private void ShowPlayTime()
        {

            if (20 >= _playTime)
            {
                _playtimeIcon.GetComponent<Animator>().SetBool("IsBlinking", true);
            }
            else
            {
                _playtimeIcon.GetComponent<Animator>().SetBool("IsBlinking", false);
            }

            _playtimeImage.fillAmount = _playTime / _playTimeMax;

        }

        private void ShowMissCount()
        {
            _missCountText.transform.ChangeScale(new Vector3(1.2f, 1.2f, 1), 0.2f, true);
            _missCountText.text = _missCount.ToString();
        }



        #endregion

    }
}

