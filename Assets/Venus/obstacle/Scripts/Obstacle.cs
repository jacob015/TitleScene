using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Temp
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField]
        public enum Type
        {
            Bird, Meteor, Satellite, Stone, Cloud, Balloon, Aurora
        }
        public Type type;
        delegate void MoveType();
        MoveType move;
        int LR;
        [SerializeField] float speed;

        Vector2 ClickPos;
        Camera cam;
        Vector3 Direction;
        [SerializeField] AudioClip ClickSound;
        [SerializeField] AudioClip DragSound;
        [SerializeField] AudioClip OnEnableSound;
        AudioSource AudioSource;
        public float SoundVolume;
        IEnumerator Start()
        {
            AudioSource = GetComponent<AudioSource>();
            AudioSource.volume = SoundVolume;
            if(OnEnableSound != null){
                AudioSource.clip = OnEnableSound;
                AudioSource.Play();
            }
            cam = Camera.main;
            LR = Random.Range(0, 2);
            if (LR == 0)
                LR = -1;
            if (type == Type.Bird || type == Type.Cloud)
                move = birdMove;
            else if (type == Type.Meteor)
                move = meteorMove;
            else if (type == Type.Aurora)
                move = null;
            else if (type == Type.Balloon)
                move = BalloonMove;
            else
            {
                move = StraighMove;
            }
            transform.position = new Vector3(transform.position.x * -(LR), transform.position.y, 0);
            if (LR == 1)
                GetComponent<SpriteRenderer>().flipX = true;
            if (type == Type.Cloud || type == Type.Aurora)
            {
                yield return new WaitForSeconds(1);
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(20);
            Destroy(gameObject);
        }
        void Update()
        {
            if (move != null)
                move();
        }
        void OnDisable()
        {
            StopAllCoroutines();
        }
        void birdMove()
        {
            transform.position += new Vector3(LR * speed * Time.deltaTime, 0, 0);
        }
        void meteorMove()
        {
            transform.position += new Vector3(LR * speed * Time.deltaTime, -1 * speed * Time.deltaTime, 0);
        }
        void StraighMove()
        {
            transform.position += new Vector3(0, -1 * speed * Time.deltaTime, 0);
        }
        void BalloonMove()
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
        public void CloudClick()
        {
            StopAllCoroutines();
            move = null;
            transform.position = Vector3.zero;
            transform.localScale = new Vector3(10, 10, 10);
            transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.8f);
            if(ClickSound != null)
            {
                AudioSource.clip = ClickSound;
                AudioSource.Play();
            }
            Destroy(gameObject, 0.5f);
        }
        public void PointerDown()
        {
            ClickPos = cam.ScreenToWorldPoint(Input.mousePosition);
            if (ClickSound != null)
            {
                AudioSource.clip = ClickSound;
                AudioSource.Play();
            }
        }
        public void PointerDrag()
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(ClickPos, mousePos) > 0.5f)
            {
                Direction = (mousePos - ClickPos).normalized;
                move = Drag;
                if (DragSound != null)
                {
                    AudioSource.clip = DragSound;
                    AudioSource.Play();
                }
            }
        }
        void Drag()
        {
            transform.position += Direction * 10 * Time.deltaTime;
            Destroy(gameObject, 2);
        }
    }
}
