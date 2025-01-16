using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FollowThePath
{
    public class RepeatBackground : MonoBehaviour
    {
        public GameObject[] backgrounds; // ��� ��������Ʈ �迭
        public float backgroundHeight; // ��� ��������Ʈ�� ����
        private Camera _mainCamera; // ���� ī�޶�

        private float viewHeight; // ī�޶� ����Ʈ ����

        void Start()
        {
            _mainCamera = Camera.main;

            // ī�޶� ����Ʈ ���� ���
            viewHeight = 2 * _mainCamera.orthographicSize;
        }

        public void Stay()
        {
            // ī�޶��� �Ʒ��� ��ġ
            float cameraBottomY = _mainCamera.transform.position.y - viewHeight / 2;

            foreach (GameObject background in backgrounds)
            {
                // ����� ī�޶��� �Ʒ��ʺ��� �� �Ʒ��� ��ġ�ϸ� ��ġ �缳��
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