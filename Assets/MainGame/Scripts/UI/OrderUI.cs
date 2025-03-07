using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject OrderUIGuest;
    public GameObject OrderUIButton;
    [Header("기능")]
    public Image OrderTimerGauge;
    public Image GuestImage;
    public TMP_Text OrderMenu;
    public TMP_Text GuestName;
    public Button OrderBtn;
    public Button HintBtn;

    MakingSystem MakingSystem;
    GameUIManager GameUIManager;
    void Awake()
    {
        MakingSystem = GameObject.Find("MakingSystem").GetComponent<MakingSystem>();
        GameUIManager = GameObject.Find("MainCanvas").GetComponent<GameUIManager>();
    }
    IEnumerator Start()
    {
        yield return new WaitWhile(() => MakingSystem.LoadEnd == false);
        LoadGuest();
        StartCoroutine(GameUIManager.PatienceTimer(OrderTimerGauge));
    }
    public void Hint()
    {
        StartCoroutine(GameUIManager.PopupAlpha());
        GameUIManager.hint = GameUIManager.CurrentNum;
    }
    public void Accept()
    {
        OrderBtn.transform.GetChild(0).GetComponent<TMP_Text>().text = "주방으로";
        bool ismake = GameUIManager.GuestMenus[GameUIManager.CurrentNum].guestset == 2 ? true : false;
        GameUIManager.MakingDrinkUIObject.gameObject.SetActive(ismake);
        GameUIManager.OrderUIObjectALL.gameObject.SetActive(!ismake);
        GameUIManager.GuestMenus[GameUIManager.CurrentNum].iscook = true;
        if (!ismake)
        {
            GameUIManager.GuestMenus[GameUIManager.CurrentNum].guestset = 2;
            GameUIManager.GuestMenus[GameUIManager.CurrentNum].nowDelay = 0;
        }
    }
    public void MovePlace()
    {
        if (MakingSystem.coroutine != null)
        {
            StopCoroutine(MakingSystem.coroutine);
            MakingSystem.coroutine = null;
        }
        if(GameUIManager.hint != null)
            HintBtn.gameObject.SetActive(GameUIManager.hint == GameUIManager.CurrentNum);
        OrderBtn.transform.GetChild(0).GetComponent<TMP_Text>().text = GameUIManager.GuestMenus[GameUIManager.CurrentNum].guestset == 2 ? "주방으로" : "주문 수락";
        GameUIManager.MakingDrinkUIObject.gameObject.SetActive(GameUIManager.GuestMenus[GameUIManager.CurrentNum].iscook);
        GameUIManager.OrderUIObjectALL.gameObject.SetActive(!GameUIManager.GuestMenus[GameUIManager.CurrentNum].iscook);
        bool isnum = GameUIManager.GuestMenus[GameUIManager.CurrentNum].guestset >= 1 && GameUIManager.GuestMenus[GameUIManager.CurrentNum].guestset <= 2;
        OrderUIGuest.SetActive(isnum);
        OrderUIButton.SetActive(isnum);
    }
    public IEnumerator EndGuest(int num)
    {
        GameUIManager.GuestMenus[GameUIManager.CurrentNum].guestset = 3;
        back();
        GameUIManager.GuestColorChange();
        OrderUIGuest.SetActive(true);
        OrderMenu.text = GameUIManager.customer.guests[GameUIManager.Guests[GameUIManager.CurrentNum]].speechDatas[num].Reaction + $"\n({GameUIManager.customer.guests[GameUIManager.Guests[GameUIManager.CurrentNum]].speechDatas[num].speechType.ToString()} 대사 출력)";
        yield return new WaitForSeconds(2f);
        OrderUIGuest.SetActive(false);
    }
    public void back()
    {
        GameUIManager.GuestMenus[GameUIManager.CurrentNum].iscook = false;
        MovePlace();
    }
    public void LoadGuest()
    {
        MovePlace();
        GuestImage.sprite = GameUIManager.customer.guests[GameUIManager.Guests[GameUIManager.CurrentNum]].GuestSprite;
        GuestName.text = GameUIManager.customer.guests[GameUIManager.Guests[GameUIManager.CurrentNum]].Name;
        OrderMenu.text = GameUIManager.customer.guests[GameUIManager.Guests[GameUIManager.CurrentNum]].speechDatas[0].Reaction + $"\n({GameUIManager.GuestMenus[GameUIManager.CurrentNum].needDrink.Name})";
    }
}
