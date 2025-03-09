using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MakingSystem : MonoBehaviour
{
    public Dictionary<string, Matter> matters = new Dictionary<string, Matter>();
    public Dictionary<string, Drink> drinks = new Dictionary<string, Drink>();

    public Inventory inventory;
    public Shop shop;

    public Customer customer;

    public bool LoadEnd = false;

    GameUIManager GameUIManager;
    DrinkMakingManager drinkMakingManager;

    [HideInInspector]
    public Coroutine coroutine;
    void Awake()
    {
        GameUIManager = GameObject.Find("MainCanvas").GetComponent<GameUIManager>();
        drinkMakingManager= GameObject.Find("MakingSystem").GetComponent<DrinkMakingManager>();
    }
    public void AddMatter(string matterName)
    {
        Matter matter = matters[matterName];
        GameUIManager.GetAddedMatters().Add(matter);
        Debug.Log($"추가한 재료{matter.Name}");
        for (int i = 0; i < GameUIManager.GetAddedMatters().Count; i++)
        {
            Debug.Log($"들어있는 재료{GameUIManager.GetAddedMatters()[i].Name}");
        }
    }
    public void ResetMatter()
    {
        drinkMakingManager.ResetMatter();
        GameUIManager.GetAddedMatters().Clear();
        Debug.Log("재료 초기화");
    }
    void Serving(int completeness)
    {
        int num;
        GameUIManager.GuestMenu guest = GameUIManager.GuestMenus[GameUIManager.CurrentNum];
        int satis = customer.EndOrder(1f - guest.nowDelay / guest.needTime);
        GameUIManager.GuestMenus[GameUIManager.CurrentNum].Satisfaction = satis + completeness;
        int score = guest.Satisfaction;
        if (score == 100)
        {
            Debug.Log("대만족");
            num = 2;
            //inventory.money += customer.needDrink.GetPrice() * 1.1f;
            shop.StarPoint += 2;
        }
        else if (score <= 25f)
        {
            num = 4;
            Debug.Log("불만족");
            shop.StarPoint -= 2;
            shop.badCount++;
        }
        else
        {
            num = 3;
            Debug.Log("만족");
            //inventory.money += customer.needDrink.GetPrice();
        }
        coroutine = StartCoroutine(customer.gameObject.GetComponent<OrderUI>().EndGuest(num));
    }
    public void MakeDrink()
    {
        int guestIndex = GameUIManager.CurrentNum;
        GameUIManager.GuestMenu guest = GameUIManager.GuestMenus[guestIndex];
        ref List<Matter> recipe = ref guest.needDrink.RequiredMatter;
        List<Matter> Temp = GameUIManager.GetAddedMatters().ToList();
        GameUIManager.GetAddedMatters().Clear();
        bool Bad = false;
        for (int i = 0; i < recipe.Count; i++)
        {
            if (!Temp.Contains(recipe[i]))
            {
                Debug.Log("부족한 재료 " + recipe[i]);
                Bad = true;
                continue;
            }
            Temp.Remove(recipe[i]);
        }
        ref List<Matter> DrinkPlusNeed = ref guest.needDrink.ExtraMatter;
        List<Matter> plusNeedResult = new List<Matter>();
        List<int[]> MatterFlavorList = new List<int[]>();

        int[] GuestPlusNeed = Array.ConvertAll(customer.guests[GameUIManager.Guests[guestIndex]].GoodFlavor, e => (int)e);
        for (int i = 0; i < DrinkPlusNeed.Count; i++)
        {
            MatterFlavorList.Add(DrinkPlusNeed[i].Flavor);
        }

        bool guestFlavor = false;
        for (int i = 0; i < MatterFlavorList.Count; i++)
        {
            for (int j = 0; j < GuestPlusNeed.Length; j++)
            {
                if (MatterFlavorList[i].Contains(GuestPlusNeed[j]))
                { guestFlavor = true; break; }
            }
            if(guestFlavor)
            {
                plusNeedResult.Add(DrinkPlusNeed[i]);
                guestFlavor = false;
            }
        }

        int needs = 0;
        for (int i = 0; i < plusNeedResult.Count; i++)
        {
            if (!Temp.Contains(plusNeedResult[i]))
            {
                Debug.Log("부족한 재료 " + plusNeedResult[i].Name);
                continue;
            }
            Temp.Remove(plusNeedResult[i]);
            needs++;
        }
        if (Temp.Count > 0 || Bad)
        {
            Debug.Log("추가재료 불만족");
            Serving(0);
        }
        else if (needs == plusNeedResult.Count)
        {

            Debug.Log("추가재료 대만족");
            Serving(50);
        }
        else
        {
            Debug.Log("추가재료 만족");
            Serving(25);
        }
        ResetMatter();
    }
}