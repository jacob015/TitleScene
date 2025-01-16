using PlanetMerge;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] private RectTransform UIHandleRectTransform;

    Toggle toggle;

    Vector2 HandlePosition;
    [Header("»ç¿îµå")]
    [SerializeField] string sound;
    private void Awake()
    {
        sound = transform.name;

        toggle = GetComponent<Toggle>();

        HandlePosition = UIHandleRectTransform.anchoredPosition;

        toggle.onValueChanged.AddListener(OnSwitch);

        OnSwitch(toggle.isOn);
    }
    public void OnSwitch(bool on)
    {
        gameObject.GetComponent<Image>().color = on ? GameManager.instance.ActiveBackGroundColor : GameManager.instance.DefaultBackGroundColor;
        UIHandleRectTransform.gameObject.GetComponent<Image>().color = on ? GameManager.instance.ActiveHandleColor : GameManager.instance.DefaultHandleColor;
        UIHandleRectTransform.anchoredPosition = on ? HandlePosition * -1 : HandlePosition;
        GameManager.instance.mixer.SetFloat(sound, Mathf.Log10(on ? 1 : 0.001f) * 20);
    }
    public void OnDestroter()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
