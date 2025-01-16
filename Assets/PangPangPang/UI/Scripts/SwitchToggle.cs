using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PangPangPang
{
    public class SwitchToggle : MonoBehaviour
    {
        [SerializeField] private RectTransform _uiHandleRectTransform;
        [SerializeField] private Sprite _onSprite;
        [SerializeField] private Sprite _offSprite;

        private Image _backgroundImage, _handleImage;

        private Toggle _toggle;

        private Vector2 _handlePosition;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _handlePosition = _uiHandleRectTransform.anchoredPosition;

            _backgroundImage = _uiHandleRectTransform.parent.GetComponent<Image>();
            _handleImage = _uiHandleRectTransform.GetComponent<Image>();

            _toggle.onValueChanged.AddListener(OnSwitch);

            if (_toggle.isOn)
            {
                OnSwitch(true);
            }
        }

        void OnSwitch(bool on)
        {
            _uiHandleRectTransform.anchoredPosition = on ? _handlePosition : _handlePosition * -1;
            _backgroundImage.sprite = on ? _onSprite : _offSprite;

        }

        private void OnDestroy()
        {
            _toggle.onValueChanged.AddListener(OnSwitch);
        }
    }
}