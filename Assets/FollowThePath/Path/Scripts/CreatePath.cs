using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FollowThePath
{

    public class CreatePath : MonoBehaviour
    {
        private LineRenderer _line;//라인렌더
        private EdgeCollider2D _edge;//라인렌더

        private float _width;
        public float Width { get { return _width; } set { _width = value; } }

        private float _height;
        public float Height { get { return _height; } set { _height = value; } }

        [SerializeField] private Transform _startPoint;

        private List<Vector2> _points = new List<Vector2>();

        private bool _isSpawned;
        public void Init()
        {
            _line = GetComponent<LineRenderer>();
            _edge = GetComponent<EdgeCollider2D>();
            AddPoint(_startPoint.position);

            for (int i = 0; i < 5; i++)
            {
                Vector2 lastPos = _points[_points.Count - 1];
                AddPoint(new Vector2(lastPos.x, lastPos.y + _height));
            }
            
            for (int i = 0; i < 5; i++)
            {
                AddPoint(RandomPoint());
            }
        }

        public void AddPath() // 길 생성
        {
            Camera cam = Camera.main;
            float half = cam.orthographicSize;
            float value = _points[_points.Count - 1].y - cam.transform.position.y;


            if (value <= half + 1)
            {
                if (!_isSpawned)
                {
                    AddPoint(RandomPoint());
                    _isSpawned = true;
                }
            }
            else
            {
                _isSpawned = false;
            }
        }

        private void AddPoint(Vector2 point) // 포인트 추가
        {
            _points.Add(point);
            _line.positionCount++;
            DrawLine();

            _edge.SetPoints(_points);
        }

        private Vector2 RandomPoint() // 랜덤 포인트
        {
            float halfWidth = _width / 2;
            return new Vector2(Random.Range(-halfWidth, halfWidth), _points[_points.Count - 1].y + _height);
        }

        private void DrawLine() // 선 그리기
        {
            for (int i = 0; i < _points.Count; i++)
            {
                _line.SetPosition(i, _points[i]); // 라인을 설정합니다
            }
        }

    }
}
