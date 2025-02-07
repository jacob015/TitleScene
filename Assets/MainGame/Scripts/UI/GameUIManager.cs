using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using static UnityEngine.Rendering.DebugUI;

public class GameUIManager : MonoBehaviour
{

    [Header("게임 UI")]
    public GameObject OrderUIObjectALL;
    public GameObject MakingDrinkUIObject;
    public CanvasGroup Popup;

    [Header("영업 시간")]
    public TMP_Text CurrentTime;
    public float maxtime = 300; //5분
    private int StartHour = 9, EndHour = 18;
    private float GameMinutesPerSecond;

    [Header("손님")]
    public Customer customer;
    public GameObject GuestList;
    public Color[] GuestinfoColors;
    public GameObject ArrowButtons;
    private GameObject leftArrow;
    private GameObject rightArrow;
    public int MaxGuest;
    public int CurrentNum = 0;
    public int[] Guests;

    [System.Serializable]
    public class GuestMenu
    {
        public Drink needDrink;
        /*
        guestset
        0 : none
        1 : come
        2 : order
        3 : end 
        4 : out
        */
        public int guestset;
        public float needTime;
        public float nowDelay;
        /*
        시간에 따른 판정 / 배점 - 50점]

        주어진 주문 시간을 초과하면 - 불만족(0점)
        주어진 주문 시간의 80% 이내에 완료 - 보통(25점)
        주어진 주문 시간의 20% 이내에 완료 - 만족(50점)

        [주문 적합도에 따른 판정/ 배점 - 50점]

        필수 재료가 빠진 레시피 - 불만족(0점)
        필수 재료를 만족하는 레시피	- 보통(25점)
        필수 재료 + 추가 재료 - 만족(50점)
        */
        public int Satisfaction; // 만족도 100 대만족, 50 ~ 75 만족, 0 ~ 25 불만족
        public GuestMenu(Drink needDrink, float needTime, float nowDelay, int guestset)
        {
            this.needDrink = needDrink;
            this.needTime = needTime;
            this.nowDelay = nowDelay;
            this.guestset = guestset;
        }
        public List<Matter> addedMatter = new List<Matter>();
        public bool iscook = false;
    }
    [SerializeField]
    public List<GuestMenu> GuestMenus = new List<GuestMenu>();

    public int? hint = null;

    MakingSystem MakingSystem;
    private void Awake()
    {
        customer = GameObject.Find("Customer").GetComponent<Customer>();
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        MakingSystem = GameObject.Find("MakingSystem").GetComponent<MakingSystem>();
        yield return new WaitWhile(() => MakingSystem.LoadEnd == false);
        leftArrow = ArrowButtons.transform.GetChild(0).gameObject;
        rightArrow = ArrowButtons.transform.GetChild(1).gameObject;
        foreach (Transform child in GuestList.transform)
        {
            child.gameObject.SetActive(false);
            child.GetComponent<Image>().color = GuestinfoColors[0];
        }
        GameMinutesPerSecond = 540f / maxtime;
        SelectGuest();
        ActiveArrow();
        StartCoroutine(BusinessHours());
        StartCoroutine(UpdateGuestTimers());
        StartCoroutine(CooldownRoutine());
        GuestColorChange();
    }
    public ref List<Matter> GetAddedMatters()
    {
        return ref GuestMenus[CurrentNum].addedMatter;
    }
    IEnumerator BusinessHours()
    {
        while (true)
        {
            float elapsedTime = Time.time;
            float gameMinutes = elapsedTime * GameMinutesPerSecond;
            int gameHour = StartHour + (int)(gameMinutes / 60);
            int gameMinute = (int)(gameMinutes % 60);

            if (gameHour >= EndHour)
            {
                gameHour = EndHour;
                gameMinute = 0;
            }

            gameMinute = (gameMinute / 10) * 10;
            CurrentTime.text = string.Format("{0:D2}:{1:D2}", gameHour, gameMinute);

            yield return new WaitForSeconds(1f); // 1초마다 갱신
        }
    }
    public void GuestColorChange()
    {
        for (int i = 0; i < MaxGuest; i++)
        {
            GuestList.transform.GetChild(i).GetComponent<Image>().color = GuestinfoColors[GuestMenus[i].guestset];
        }
    }
    void SelectGuest()
    {
        MaxGuest = Random.Range(0, 5) + 1;
        Guests = new int[MaxGuest];
        List<string> values = new List<string>(customer.makingSystem.drinks.Keys);
        for (int i = 0; i < MaxGuest; i++)
        {
            Guests[i] = Random.Range(1001, 1000 + GuestDB.instance.GuestDatas.Count) + 1;
            GuestList.transform.GetChild(i).gameObject.SetActive(true);
            GuestMenu menu = new GuestMenu(customer.makingSystem.drinks[values[Random.Range(0, values.Count)]], 60, 0, 0);
            GuestMenus.Add(menu);
            //손님가면 리스트에서 지워주거나 배열로하는게 좋을듯
        }
    }
    void ActiveArrow()
    {
        leftArrow.SetActive(CurrentNum > 0);
        rightArrow.SetActive(CurrentNum < MaxGuest - 1);
    }
    public void NextGuest(bool isNext)
    {
        if (isNext)
        {
            if (CurrentNum < MaxGuest - 1)
            {
                CurrentNum++;
            }
        }
        else
        {
            if (CurrentNum > 0)
            {
                CurrentNum--;
            }
        }
        ActiveArrow();
    }
    private int count = 0;
    IEnumerator CooldownRoutine()
    {
        int cooldownTime = 3;
        while (count < MaxGuest)
        {
            Debug.Log($"Cooldown started: {cooldownTime} seconds");

            yield return new WaitForSeconds(cooldownTime);
            GuestMenus[count].guestset = 1;
            GuestColorChange();
            customer.gameObject.GetComponent<OrderUI>().LoadGuest();
            count++;
            Debug.Log($"Cooldown ended. Count increased: {count}");
            cooldownTime = Random.Range(10, 21);
            if (count >= MaxGuest)
                break;
        }
    }
    IEnumerator UpdateGuestTimers()
    {
        while (true)
        {
            foreach (var guest in GuestMenus)
            {
                if (guest.guestset == 1 || guest.guestset == 2 && guest.nowDelay < guest.needTime)
                {
                    guest.nowDelay += Time.deltaTime;
                    if (guest.nowDelay > guest.needTime)
                    {
                        guest.nowDelay = guest.needTime;
                        if (guest.guestset == 1)
                        {
                            guest.guestset = 4;
                            GuestColorChange();
                            customer.gameObject.GetComponent<OrderUI>().LoadGuest();
                        }
                    }
                }
            }
            yield return null;
        }
    }
    public IEnumerator PatienceTimer(Image TimerGauge)
    {
        float size = 1;
        while (size > 0)
        {
            yield return null;
            size = 1 - GuestMenus[CurrentNum].nowDelay / GuestMenus[CurrentNum].needTime;
            TimerGauge.fillAmount = size;
        }
    }
    public IEnumerator PopupAlpha()
    {
        Popup.transform.GetChild(0).GetComponent<TMP_Text>().text = GuestMenus[CurrentNum].needDrink.Name + "\n<재료>";
        for (int i = 0; i < GuestMenus[CurrentNum].needDrink.RequiredMatter.Count; i++)
            Popup.transform.GetChild(0).GetComponent<TMP_Text>().text += "\n" + GuestMenus[CurrentNum].needDrink.RequiredMatter[i].Name;

        Popup.alpha = 1;
        yield return new WaitForSeconds(3f);
        while (Popup.alpha > 0)
        {
            Popup.alpha -= Time.deltaTime;
            yield return null;
        }
        Popup.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
    }
}