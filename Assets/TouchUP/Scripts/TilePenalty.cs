using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace TouchUp
{
    public enum PenaltyState
    {
        Idle,
        Fade,
        Rotate
    }

    public class TilePenalty : MonoBehaviour
    {
        private PenaltyState penalty;
        private Image _image;
        private Sequence fade;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Start()
        {
            fade = DOTween.Sequence().SetAutoKill(false).SetLoops(-1, LoopType.Restart)
                .Append(_image.DOFade(0, 0.8f))
                .Append(_image.DOFade(1.0f, 0.8f).SetDelay(1.0f).SetEase(Ease.InQuart));

            penalty = PenaltyState.Idle;
            PenaltyAct();
        }

        private void PenaltyAct()
        {
            switch (penalty)
            {
                case PenaltyState.Idle:
                    fade.Rewind();
                    transform.DORewind();
                    break;
                case PenaltyState.Fade:
                    fade.Restart();
                    break;
                case PenaltyState.Rotate:
                    transform.DORotate(new Vector3(0, 0, 180), Random.Range(0.5f, 1.0f))
                        .SetLoops(-1, LoopType.Incremental)
                        .SetEase(Ease.Linear);
                    break;
            }
        }

        public void PenaltySet(PenaltyState state)
        {
            penalty = state;
            PenaltyAct();
        }
    }
}