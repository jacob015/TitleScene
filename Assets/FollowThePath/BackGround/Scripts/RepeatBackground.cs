using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FollowThePath
{
    public class RepeatBackground : MonoBehaviour
    {
        public GameObject[] backgrounds; // 배경 스프라이트 배열
        public float backgroundHeight; // 배경 스프라이트의 높이
        private Camera _mainCamera; // 메인 카메라

        private float viewHeight; // 카메라 뷰포트 높이

        void Start()
        {
            _mainCamera = Camera.main;

            // 카메라 뷰포트 높이 계산
            viewHeight = 2 * _mainCamera.orthographicSize;
        }

        public void Stay()
        {
            // 카메라의 아래쪽 위치
            float cameraBottomY = _mainCamera.transform.position.y - viewHeight / 2;

            foreach (GameObject background in backgrounds)
            {
                // 배경이 카메라의 아래쪽보다 더 아래에 위치하면 위치 재설정
                if (background.transform.position.y + backgroundHeight / 2 < cameraBottomY)
                {
                    Vector3 newPosition = background.transform.position;
                    newPosition.y += backgrounds.Length * backgroundHeight;
                    background.transform.position = newPosition;
                }
            }
        }
    }
}