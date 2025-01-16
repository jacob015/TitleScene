using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FollowThePath
{
    public class MonsterController : MonoBehaviour
    {
        public Define.MonsterState curMoveCameState;
        public float speed = 1;

        private Transform _player;
        public float MoveLength { get; set; } = 0;

        void Start()
        {
            curMoveCameState = Define.MonsterState.PAUSE;
            _player = GameObject.Find("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
            switch (curMoveCameState)
            {
                case Define.MonsterState.MOVE:
                    float last = transform.position.y;
                    transform.Translate(Vector2.up * speed * Time.deltaTime);
                    transform.position = new Vector3(_player.position.x, transform.position.y, transform.position.z);
                    //transform.position = _player.position;
                    MoveLength += transform.position.y - last;
                    break;
                case Define.MonsterState.PAUSE:
                    break;
            }

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (collision.GetComponent<PlayerController>().IsDead == false)
                {
                    SoundManager.Instance.StopBGM();
                    SoundManager.Instance.PlaySFX("SFX_GameOver");
                    collision.GetComponent<Animator>().SetTrigger("isDie");
                    collision.GetComponent<PlayerController>().IsDead = true;
                }
            }
        }
    }

}
