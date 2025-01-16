using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Anibel
{
    public class AnimalMove : MonoBehaviour
    {
        private AnimalData data;

        public AnimalData Data
        {
            set => data = value;
            get => data;
        }

        private SpriteRenderer _spriteRenderer;
        private float duration = 0.3f;
        public bool isSurprised;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetData()
        {
            if (currentPos > 3)
            {
                _spriteRenderer.sprite = data.NormalSprite;
            }
            else
            {
                _spriteRenderer.sprite = data.NervousSprite;
            }

            if (isSurprised)
            {
                _spriteRenderer.sprite = data.SurprisedSprite;
            }
        }

        private int currentPos;

        public int CurrentPos
        {
            get { return currentPos; }
            set { currentPos = value; }
        }

        private bool isMove;

        public bool IsMove
        {
            get { return isMove; }
        }

        public void Move()
        {
            isMove = true;
            transform.DOMoveY(AnimalManager.Instance.pos[currentPos].position.y, duration).OnComplete(CompleteMove);
        }

        public void Dump()
        {
            Sequence test = DOTween.Sequence();
            Vector2 dumpPos = new Vector2(3.14f, 2.45f);
            Vector3 rotate = new Vector3(0, 0, -100f);
            _spriteRenderer.sprite = data.DropSprite;
            test.Append(transform.DOMove(dumpPos, duration));
            test.Join(transform.DORotate(rotate, duration));
            test.Play().OnComplete(CompleteDump);
        }

        private void CompleteDump()
        {
            currentPos = 0;
            transform.position = AnimalManager.Instance.pos[0].position;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            Data = WeightedRandomUtility.GetWeightedRandom(AnimalManager.Instance._datas);
            isMove = false;
        }

        private void CompleteMove()
        {
            isMove = false;
        }
    }
}