using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Anibel
{
    public class ScreenTouch : MonoBehaviour
    {
        private void Update()
        {
            MouseClick();
        }

        private void MouseClick()
        {
            if (!StartTheGame.Instance.CanStart)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    return;

                Vector2 mousePos = Input.mousePosition;
                Vector2 point = Camera.main.ScreenToWorldPoint(mousePos);

                if (point.x > 0)
                {
                    // 버려짐
                    AnimalManager.Instance.AnimalsDump();
                }
                else
                {
                    // 쌓임
                    AnimalManager.Instance.AnimalsStack();
                }
            }
        }
    }
}