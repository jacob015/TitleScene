using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace SpaceFarm
{
    public class GameManager : Singleton<GameManager>
    {
        private Dictionary<Define.GameState, GameState> _gameStateStorage = new Dictionary<Define.GameState, GameState>();
        private Define.GameState _curGameState = Define.GameState.START;
        public Define.GameState CurGameState
        {
            get
            {
                return _curGameState;
            }
            set
            {
                _gameStateStorage[_curGameState]?.Exit();
                _curGameState = value;
                _gameStateStorage[_curGameState]?.Enter();
            }
        }

        private float _score;
        public float Score
        {
            get 
            {
                return _score;
            }
            set
            {
                _score = value;
                if (_score > _bestScore)
                {
                    _bestScore = _score;                   
                }

                UIManager.Instance.GetUI<TextMeshProUGUI>("ScoreText").text = _score.ToString();
                UIManager.Instance.GetUI<TextMeshProUGUI>("EndScoreText").text = "Score : " + _score.ToString();
                UIManager.Instance.GetUI<TextMeshProUGUI>("EndBestScoreText").text = "Best Score : " + _bestScore.ToString();

                UIManager.Instance.GetUI<TextMeshProUGUI>("ScoreText").transform.ChangeScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f, true);
            }
        }
        private float _bestScore;

        private void Awake()
        {
            _gameStateStorage.Add(Define.GameState.START, new StartState());
            _gameStateStorage.Add(Define.GameState.PLAY, new PlayState());
            _gameStateStorage.Add(Define.GameState.END, new EndState());
            _gameStateStorage.Add(Define.GameState.PAUSE, new PauseState());
        }

        private void Start()
        {

            foreach (var gs in _gameStateStorage)
            {
                gs.Value.Init();
            }

            _gameStateStorage[_curGameState]?.Enter();
        }

        private void Update()
        {
            _gameStateStorage[_curGameState]?.Stay();

        }
    }
}