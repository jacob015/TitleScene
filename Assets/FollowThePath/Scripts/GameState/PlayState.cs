using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FollowThePath
{
    public class PlayState : GameState
    {
        private MonsterController _monster;
        //private RepeatBackground _repeatBackground; 
        private CreatePath _path;
        private PlayerController _player;
        private Background _background;

        private ObjectPool _pool;
        private float _spawnTimer = 0;
        private float _spawnDelay = 10;
        private int _spawnCount = 1;
        private int _maxSpawnCount = 3;
        private int _spawnUpgradeCount = 0;
        private Vector2 _SpawnTime = new Vector2(4, 10);
        private List<Transform> _spawnPoints = new List<Transform>();
        private List<ShootingStar> _stars = new List<ShootingStar>();
        private List<ShootingStar> _removeStars = new List<ShootingStar>();

        // x : width, y : height
        private Vector2 _minPath = new Vector2(4,3);
        private Vector2 _maxPath = new Vector2(6, 1.5f);

        // x : min, y : max
        private Vector2 _monsterSpeedRange = new Vector2(1f, 1.5f);

        private float _maxChangeTime = 100;

        public override void Init()
        {
            _panel = UIManager.Instance.GetUI<CanvasGroup>("PlayPanel");

            _monster = GameObject.Find("Monster").GetComponent<MonsterController>();
            _path = GameObject.Find("Path").GetComponent<CreatePath>();
            _player = GameObject.Find("Player").GetComponent<PlayerController>();
            _background = GameObject.Find("GameBackground").GetComponent<Background>();
            _pool = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();

            foreach (Transform sp in GameObject.Find("SpawnPoints").transform)
            {
                _spawnPoints.Add(sp);
            }

            _path.Width = _minPath.x;
            _path.Height = _minPath.y;

            _monster.speed = _monsterSpeedRange.x;

            _player.Init();
            _background.Init();
            _path.Init();
        }

        public override void Enter()
        {
            base.Enter();

            _monster.curMoveCameState = Define.MonsterState.MOVE;
        }

        public override void Exit()
        {
            base.Exit();
            _monster.curMoveCameState = Define.MonsterState.PAUSE;
        }

        public override void Stay()
        {
            if (!_player.IsDead)
            {
                GameManager.Instance.Record += Time.deltaTime;
            }

            ChangeDifficulty(); //난이도 조절

            _player.Stay();
            _background.Stay();
            //_repeatBackground.Stay();

            _path.AddPath();

            if (_spawnTimer >= _spawnDelay)
            {
                List<Transform> tempSpawnPoint = _spawnPoints.ToList();
                for (int i = 0; i < _spawnCount; i++)
                {
                    GameObject shootingStar = _pool.GetObject("ShootingStar");
                    Transform sp = tempSpawnPoint[Random.Range(0, tempSpawnPoint.Count - 1)];
                    shootingStar.transform.position = sp.position;
                    shootingStar.GetComponent<ShootingStar>().Target = _player.gameObject;
                    shootingStar.GetComponent<ShootingStar>().Enter();
                    _stars.Add(shootingStar.GetComponent<ShootingStar>());
                    tempSpawnPoint.Remove(sp);
                }
                
                _spawnTimer = 0;

                if (_spawnCount < _maxSpawnCount)
                {
                    _spawnUpgradeCount++;

                    if (_spawnUpgradeCount == 5)
                    {
                        _spawnCount++;
                        _spawnUpgradeCount = 0;
                    }

                }

            }
            else
            {
                _spawnTimer += Time.deltaTime;
            }

            foreach (ShootingStar star in _stars)
            {
                star.Stay();

                if (star.IsEnd)
                {
                    _removeStars.Add(star);
                }
            }

            if (_removeStars.Count > 0)
            {
                foreach (ShootingStar star in _removeStars)
                {
                    star.Exit();
                    _stars.Remove(star);
                }

                _removeStars.Clear();
            }

          
        }

        private void ChangeDifficulty()
        {
            if (GameManager.Instance.Record >= _maxChangeTime)
            {
                _path.Width = _maxPath.x;
                _path.Height = _maxPath.y;

                _monster.speed = _monsterSpeedRange.y;
                return;
            }

            float temp = (GameManager.Instance.Record / _maxChangeTime);
           

            _path.Width = Mathf.Lerp(_minPath.x, _maxPath.x, temp);
            _path.Height = Mathf.Lerp(_minPath.y, _maxPath.y, temp);

            _monster.speed = Mathf.Lerp(_monsterSpeedRange.x, _monsterSpeedRange.y, temp);

            _spawnDelay = Mathf.Lerp(_SpawnTime.y, _SpawnTime.x, temp);
        }
    }
}