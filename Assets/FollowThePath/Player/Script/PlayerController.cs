using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FollowThePath
{
    public class PlayerController : MonoBehaviour
    {
        private Animator _ani;

        [SerializeField] private LayerMask _pathLayer;
        [SerializeField] private float _radius;
        [SerializeField] private float _checkRadius;
        [SerializeField] private int _numberOfPoints;

        [SerializeField] private float _maxSpeed = 2;
        [SerializeField] private float _maxDistance = 50;
        private Vector2 _lastPos;
        private Vector2 _firstPos;
        private Vector2 direction;
        public bool IsDead { get; set; } = false;
        private bool _isWalking = false;

        [SerializeField] private GameObject _touchStartPoint;
        [SerializeField] private GameObject _touchMovePoint;

        public void Init()
        {
            _ani = GetComponent<Animator>();
        }

        public void Stay()
        {
            if (IsDead) 
            {
                GetComponent<Collider2D>().enabled = false;
                SoundManager.Instance.StopLoopingSFX("SFX_Walk");
                return;
            }

            CheckInPath();
            Move();
        }

        private void Move()
        {

            direction = Vector2.zero;
#if UNITY_EDITOR
            UseMouse();
#elif UNITY_ANDROID
           UseTouch();
#endif
            _ani.SetFloat("Speed", Mathf.Abs(direction.magnitude));

           
            transform.Translate(Vector2.up * direction.magnitude * Time.deltaTime);
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y+2, Camera.main.transform.position.z) ;
        }


        private void UseMouse()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _lastPos = Input.mousePosition;   
            }
            else if (Input.GetMouseButton(0))
            {
                //print(direction);
                _firstPos = Input.mousePosition;
                direction = _firstPos - _lastPos;

                if (!_isWalking && direction.magnitude >= _maxDistance / 10)
                {
                    SoundManager.Instance.PlayLoopingSFX("SFX_Walk");
                    _touchStartPoint.SetActive(true);       
                    _isWalking = true;
                }
          
                direction = Vector2.ClampMagnitude(direction, _maxDistance);
                _touchStartPoint.transform.localPosition = ChangePosition(_touchStartPoint.GetComponent<RectTransform>(), _lastPos);
                _touchMovePoint.transform.localPosition = ChangePosition(_touchMovePoint.GetComponent<RectTransform>(), _lastPos + direction);

                direction *= _maxSpeed / _maxDistance;

                transform.up = direction.normalized;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (direction == Vector2.zero && _isWalking)
                {
                    SoundManager.Instance.StopLoopingSFX("SFX_Walk");
                    _touchStartPoint.SetActive(false);
                    _isWalking = false;
                }
            }
        }

        private void UseTouch()
        {
            if (Input.touchCount > 0)
            {

                Touch touch = Input.GetTouch(0);
                // 터치 입력 감지
                if (touch.phase == TouchPhase.Began)
                {
                    _lastPos = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                   
                    _firstPos = Input.mousePosition;
                    direction = _firstPos - _lastPos;

                    if (!_isWalking && direction.magnitude >= _maxDistance / 10)
                    {
                        SoundManager.Instance.PlayLoopingSFX("SFX_Walk");
                        _touchStartPoint.SetActive(true);
                        _isWalking = true;
                    }

                    direction = Vector2.ClampMagnitude(direction, _maxDistance);
                    _touchStartPoint.transform.localPosition = ChangePosition(_touchStartPoint.GetComponent<RectTransform>(), _lastPos);
                    _touchMovePoint.transform.localPosition = ChangePosition(_touchMovePoint.GetComponent<RectTransform>(), _lastPos + direction);

                    direction *= _maxSpeed / _maxDistance;
                    transform.up = direction.normalized;
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    if (direction == Vector2.zero && _isWalking)
                    {
                        SoundManager.Instance.StopLoopingSFX("SFX_Walk");
                        _touchStartPoint.SetActive(false);
                        _isWalking = false;
                    }
                }
            }

        }

        private void CheckInPath()
        {
            float angleStep = 360f / _numberOfPoints;

            for (int i = 0; i < _numberOfPoints; i++)
            {
                float angle = i * angleStep;
                Vector2 pointOnRadius = (Vector2)transform.position + new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * _radius;
                Collider2D collider = Physics2D.OverlapCircle(pointOnRadius, _checkRadius);

                if (collider == null && IsDead == false)
                {
                    SoundManager.Instance.StopBGM();
                    SoundManager.Instance.PlaySFX("SFX_GameOver");

                    transform.position = pointOnRadius;
                    _ani.SetTrigger("isDrop");
                    IsDead = true;
                }

            }
        }
  

        private Vector2 ChangePosition(RectTransform uiElement, Vector2 touchPoint)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                  uiElement.parent as RectTransform,
                  touchPoint,
                  Camera.main,
                  out Vector2 localPoint
              );

            return localPoint;
        }

        private void DieEvent()
        {
            GameManager.Instance.CurGameState = Define.GameState.END;
        }

        // 디버그용으로 OverlapCircle 시각화
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);

            float angleStep = 360f / _numberOfPoints;

            for (int i = 0; i < _numberOfPoints; i++)
            {
                float angle = i * angleStep;
                Vector2 pointOnRadius = (Vector2)transform.position + new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * _radius;
                Gizmos.DrawSphere(pointOnRadius, _checkRadius);
            }
        }
     

    
    }
}