using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PangPangPang
{
    public class Hole : MonoBehaviour
    {
        public bool IsOn { get; set; } = false;


        private List<Transform> _moles = new List<Transform>();

        private void Awake()
        {

            foreach (Transform mole in transform.Find("HoleMask"))
            {
                _moles.Add(mole);
            }
        }

        public void Execute(Define.ColorType type)
        {
            IsOn = true;

            Transform curMole = _moles[(int)type]; //랜덤 색 두더지 선택
            curMole.GetComponent<Mole>().Raise();
            curMole.GetComponent<Mole>().endHiddenEvent = () => { IsOn = false; };
        }


    }
}
