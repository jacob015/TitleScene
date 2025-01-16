using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FollowThePath 
{
    public class ShootingStar : PoolObject
    {
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;
        private float _speed;
        [SerializeField] private float _range;
        private GameObject _target;
        public GameObject Target { set => _target = value; }

        private Vector2 _targetPoint;
        private Vector2 _direction;

        [SerializeField] private Transform _headStar;
        [SerializeField] private GameObject _starParticle;

        [SerializeField] private float _endTime = 15f;
        private float _timer = 0;
        public bool IsEnd { get; set; } = false;
        public void Enter()
        {
            _targetPoint = (Vector2)_target.transform.position + Random.insideUnitCircle * _range;

            _direction = _targetPoint - (Vector2)transform.position;
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
 
            transform.rotation = Quaternion.Euler(0,0,angle-90);
            transform.ChangeScale(Vector3.one, 0.5f, false, () => { _starParticle.SetActive(true); });

            _speed = Random.Range(_minSpeed, _maxSpeed);
           
        }

        // Update is called once per frame
        public void Stay()
        {
            _headStar.Rotate(0, 0, Time.deltaTime * 50);
            transform.Translate(Vector2.up * _speed * Time.deltaTime );

            if (_timer >= _endTime)
            {
                IsEnd = true;
            }
            else
            {
                _timer += Time.deltaTime;
            }
        }

        public void Exit()
        {
            _starParticle.SetActive(false); 
            pool.ReturnObject(gameObject);
            IsEnd = false;
            _timer = 0;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (GameManager.Instance.CurGameState != Define.GameState.END)
                {
                    SoundManager.Instance.StopBGM();
                    SoundManager.Instance.PlaySFX("SFX_GameOver");

                    collision.GetComponent<Animator>().SetTrigger("isDie");
                    collision.GetComponent<PlayerController>().IsDead = true;

                    transform.localScale = Vector3.one;
                    _starParticle.SetActive(false);
                    pool.ReturnObject(gameObject);
                }
               
            }
        }
    }
}