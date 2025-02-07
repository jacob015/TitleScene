using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DataUse : MonoBehaviour
{
    [SerializeField]private TMP_Text DataText;
    [SerializeField]private TMP_Text NumberText;

    public int num;
    public void Update()
    {
        NumberText.text = num.ToString();
        if (num <= 0)
            num = 0;
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0)
        {
            num++;
        }
        else if (wheelInput < 0)
        {
            if (num <= 0)
                num = 0;
            else
                num--;
        }
    }
    public void OnLoadGuest()
    {
        DataText.text = "이름 : " + GuestDB.instance.GuestDatas[num].Name;
        DataText.text += "\n성격 : " + GuestDB.instance.GuestDatas[num].Personality;
        DataText.text += "\n선호 : ";
        for (int i = 0; i < GuestDB.instance.GuestDatas[num].GoodFlavor.Length; i++)
            DataText.text += GuestDB.instance.GuestDatas[num].GoodFlavor[i].ToString() + " ";
        DataText.text += "\n비 선호 : ";
        DataText.text += GuestDB.instance.GuestDatas[num].BadFlavor.ToString();
    }
    public void OnLoadGuestExplanation()
    {
        DataText.text = GuestDB.instance.GuestDatas[num].Explanation;
    }
    public void OnLoadDrink()
    {
        DataText.text = "음료 : " + DrinkDB.instance.DrinkDatas[num].Name;

        DataText.text += "\n음료 종류 : " + DrinkDB.instance.DrinkDatas[num].drinkType;

        DataText.text += "\n필수 재료 : ";
        for (int i = 0; i < DrinkDB.instance.DrinkDatas[num].RequiredMatter.Length; i++)
            DataText.text += DrinkDB.instance.DrinkDatas[num].RequiredMatter[i].ToString() + " ";
        DataText.text += "\n추가 재료 : ";
        for (int i = 0; i < DrinkDB.instance.DrinkDatas[num].ExtraMatter.Length; i++)
            DataText.text += DrinkDB.instance.DrinkDatas[num].ExtraMatter[i].ToString() + " ";
    }
    public void OnLoadMatter()
    {
        DataText.text = "재료 : " + MatterDB.instance.MatterDatas[num].Name;

        DataText.text += "\n원산지 : " + MatterDB.instance.MatterDatas[num].Origin;

        DataText.text += "\n맛 : " + MatterDB.instance.MatterDatas[num].Flavor;
    }
}
