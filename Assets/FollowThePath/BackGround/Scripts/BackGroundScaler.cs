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

            //Default 해상도 비율
            float fixedAspectRatio = 9f / 16f;

            //현재 해상도의 비율
            float currentAspectRatio = (float)Screen.width / (float)Screen.height;

            //현재 해상도 가로 비율이 더 길 경우
            if (currentAspectRatio > fixedAspectRatio) _mainCanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
            //현재 해상도의 세로 비율이 더 길 경우
            else if (currentAspectRatio < fixedAspectRatio) _mainCanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
        }

    }
}
