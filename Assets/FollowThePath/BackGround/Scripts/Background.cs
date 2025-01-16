using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FollowThePath
{
    public class Background : MonoBehaviour
    {
        private Camera _cam;

        [SerializeField] private List<Color> _colorList = new List<Color>();

        private Color _startColor;
        private Color _endColor;

        [SerializeField] private float _changeDelay; // 색 변환 간격
        private float _timer = 0f;
        [SerializeField] private float _duration = 1.0f; // 색 변환 시간 
        private float _timeElapsed = 0f;

        public void Init()
        {
            _cam = Camera.main;

            _startColor = _cam.backgroundColor;
            _endColor = GetRandomColor();
           
        }

        public void Stay()
        {
            if (_timer >= _changeDelay)
            {
                _timeElapsed += Time.deltaTime;
                Color currentColor = Color.Lerp(_startColor, _endColor, _timeElapsed / _duration);
                _cam.backgroundColor = currentColor;

                if (_cam.backgroundColor == _endColor)
                {
                    _startColor = _endColor;
                    _endColor = GetRandomColor();

                    _timer = 0;
                    _timeElapsed = 0;
                }
            }
            else
            {
                _timer += Time.deltaTime;
            }
        }

        private Color GetRandomColor()
        {
            Color curColor = _colorList[Random.Range(0, _colorList.Count)];
            while (_endColor == curColor)
            {
                curColor = _colorList[Random.Range(0, _colorList.Count)];
            }
            return curColor;
        }
    }
}
