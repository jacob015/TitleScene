using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICheckBox : MonoBehaviour
{
    public Button checkBtn;
    public GameObject onCheckBtn;
    private bool _isOn;

    void Start()
    {
        this.checkBtn.onClick.AddListener(() =>
        {
            if(_isOn == false)
            {
                this.OnCheckBox();
                Debug.Log("CheckBoxOn");                
            }
            else
            {
                this.OffCheckBox();
                Debug.Log("CheckBoxOff");                
            }
        });
    }

    public void OnCheckBox()
    {
        this.onCheckBtn.SetActive(true);
        this._isOn = true;
    }

    public void OffCheckBox()
    {
        this.onCheckBtn.SetActive(false);
        this._isOn = false;
    }
}
