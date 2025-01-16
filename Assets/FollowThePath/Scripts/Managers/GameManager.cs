using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FollowThePath
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

        private float _record = 0;
        public float Record 
        {
            get 
            {
                return _record; 
            }
            set
            {
                _record = value;

                if (Mathf.Floor(_record) > _bestRecord)
                {
                    _bestRecord = _record;
                }

                UIManager.Instance.GetUI<TextMeshProUGUI>("InGameRecordText").text = Mathf.Floor(_record).ToString();
                UIManager.Instance.GetUI<TextMeshProUGUI>("RecordText").text = Mathf.Floor(_record).ToString();
                UIManager.Instance.GetUI<TextMeshProUGUI>("BestRecordText").text = Mathf.Floor(_bestRecord).ToString();
            }
        }
        private float _bestRecord = 0;



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

        

        #region UI

       /* private void ShowScore()
        {
            _scoreText.transform.ChangeScale(new Vector3(1.2f, 1.2f, 1), 0.2f, true);
            _scoreText.text = _score.ToString();
        }*/

      
        #endregion

    }
}

