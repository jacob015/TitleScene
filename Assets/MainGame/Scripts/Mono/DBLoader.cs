using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DBLoader : MonoBehaviour
{
    MakingSystem makingSystem;

    [SerializeField] GameObject NoClickLoading;
    [SerializeField] TMP_Text NoClickText;
    [SerializeField] GameObject ReLoadButton;
    [SerializeField] List<TMP_Text> MatterText;
    void Awake()
    {
        makingSystem = GameObject.Find("MakingSystem").GetComponent<MakingSystem>();
        LoadStart();
    }
    void LoadEnd()
    {
        makingSystem.LoadEnd = true;
        NoClickLoading.SetActive(false);
        Time.timeScale = 1;
        Destroy(gameObject);
    }
    void LoadFail(string FailText)
    {
        NoClickText.text = FailText;
        ReLoadButton.SetActive(true);
    }
    public void ReLoad()
    {
        NoClickText.text = "�ε���";
        ReLoadButton.SetActive(false);
        GuestSpeechDB.instance.LoadData();
        GuestDB.instance.LoadData();
        MatterDB.instance.LoadData();
        DrinkDB.instance.LoadData();
        LoadStart();
    }
    void LoadStart()
    {
        Time.timeScale = 0;
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            LoadFail("���ͳ� ���� Ȯ��");
            return;
        }
        StartCoroutine(MatterLoad());
    }
    IEnumerator MatterLoad()
    {
        MatterDB DB = MatterDB.instance;
        float Timer = 0f;
        WaitForSecondsRealtime waitForSeconds = new WaitForSecondsRealtime(0.05f);
        makingSystem.matters.Clear();
        while (Timer < 10f)
        {
            if (DB.MatterDatas.Count > 0)
                break;
            Timer += 0.05f;
            yield return waitForSeconds;
        }
        if (DB.MatterDatas.Count == 0)
        {
            LoadFail("����ð� �ʰ�");
            yield break;
        }
        for (int i = 0; i < DB.MatterDatas.Count; i++)
        {
            MatterDB.MatterInfo info = DB.MatterDatas[i];
            Matter matter = new Matter(info.Name, info.Origin, info.Explanation, Array.ConvertAll(info.Flavor, e => (int)e), null);
            string key = info.Name.Replace(" ", "");
            //matter.name = info.Name;
            makingSystem.matters.Add(key, matter);
            //Debug.Log(key);
            MatterText[i].text = info.Name;
            MatterText[i].transform.parent.name = key;
        }
        yield return waitForSeconds;
        StartCoroutine(DrinkLoad());
    }
    IEnumerator DrinkLoad()
    {
        DrinkDB DB = DrinkDB.instance;
        float Timer = 0f;
        WaitForSecondsRealtime waitForSeconds = new WaitForSecondsRealtime(0.05f);
        makingSystem.drinks.Clear();
        while (Timer < 10f)
        {
            if (DB.DrinkDatas.Count > 0)
                break;
            Timer += 0.05f;
            yield return waitForSeconds;
        }
        if (DB.DrinkDatas.Count == 0)
        {
            LoadFail("����ð� �ʰ�");
            yield break;
        }
        for (int i = 0; i < DB.DrinkDatas.Count; i++)
        {
            DrinkDB.DrinkInfo info = DB.DrinkDatas[i];
            List<Matter> requiredMatters = new List<Matter>();
            List<Matter> extraMatterMatters = new List<Matter>();
            for (int a = 0; a < info.RequiredMatter.Length; a++)
            {
                requiredMatters.Add(makingSystem.matters[info.RequiredMatter[a].ToString()]);
            }
            for (int b = 0; b < info.ExtraMatter.Length; b++)
            {
                extraMatterMatters.Add(makingSystem.matters[info.ExtraMatter[b].ToString()]);
            }

            Drink drink = new Drink(info.Name, info.drinkType, requiredMatters, extraMatterMatters, info.Feature, info.difficulty, info.Price, info.Bonus, info.satisfaction, info.ProduceTime);
            //drink.name = info.Name;
            makingSystem.drinks.Add(info.Name, drink);
        }
        StartCoroutine(guestLoading());
    }
    IEnumerator guestLoading()
    {
        GuestDB DB = GuestDB.instance;
        GuestSpeechDB speechDB = GuestSpeechDB.instance;
        float Timer = 0f;
        WaitForSecondsRealtime waitForSeconds = new WaitForSecondsRealtime(0.05f);
        makingSystem.customer.guests.Clear();
        while (Timer < 10f)
        {
            if (DB.GuestDatas.Count > 0 && speechDB.GuestSpeechDatas.Count > 0)
                break;
            Timer += 0.05f;
            yield return waitForSeconds;
        }
        if (DB.GuestDatas.Count == 0 && speechDB.GuestSpeechDatas.Count == 0)
        {
            LoadFail("����ð� �ʰ�");
            yield break;
        }
        int speechDBIndex = 0;
        for (int i = 0; i < DB.GuestDatas.Count; i++)
        {
            GuestDB.GuestInfo info = DB.GuestDatas[i];
            int NPC_ID = i + 1001;

            // �մ� ��ü ����
            Guest guest = new Guest(info.ID, info.Name, info.Personality, info.GoodFlavor, info.BadFlavor, info.Feature, info.Explanation);
            //Debug.Log($" [guestLoading] �մ� {info.Name} �߰� ��...");
            // �մ� ID�� ��ġ�ϴ� ��縦 ����Ʈ�� �߰�
            for (; speechDBIndex < speechDB.GuestSpeechDatas.Count; speechDBIndex++)
            {
                GuestSpeechDB.GuestSpeechInfo speechInfo = speechDB.GuestSpeechDatas[speechDBIndex];

                if (speechInfo.NPC_ID != NPC_ID)
                    break; // �ٸ� NPC�� ����� �ݺ� ����

                // ��� ������ �߰�
                Guest.speechData sData = new Guest.speechData(speechInfo.speechType, speechInfo.speechCategory, speechInfo.Reaction, speechInfo.Explanation);
                guest.speechDatas.Add(sData);
            }

            // �մ� �̸��� ������ �߰�
            //guest.name = info.Name;
            makingSystem.customer.guests.Add(info.ID, guest);
            //Debug.Log($" [guestLoading] �մ� {info.Name} ��� �Ϸ�.");
        }
        LoadEnd();
    }
}