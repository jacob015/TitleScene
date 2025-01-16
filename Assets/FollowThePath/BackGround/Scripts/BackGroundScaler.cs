using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FollowThePath
{
    public class BackGroundScaler : MonoBehaviour
    {

        private GameObject _mainCanvas;

        void Awake()
        {
            _mainCanvas = GameObject.Find("MainCanvas");

            //Default �ػ� ����
            float fixedAspectRatio = 9f / 16f;

            //���� �ػ��� ����
            float currentAspectRatio = (float)Screen.width / (float)Screen.height;

            //���� �ػ� ���� ������ �� �� ���
            if (currentAspectRatio > fixedAspectRatio) _mainCanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
            //���� �ػ��� ���� ������ �� �� ���
            else if (currentAspectRatio < fixedAspectRatio) _mainCanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
        }

    }
}
