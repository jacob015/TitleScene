using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

public class ReadData : MonoBehaviour
{
    public readonly string ADDRESS = "https://docs.google.com/spreadsheets/d/1e4CxgbnSyZ-lYy9odzRAzBbd0Cf5u1aNiWHneAzMUkI";

    public static ReadData instance;
    private void Awake()
    {
        instance = this;
    }
    public static string GetAddress(string address, string range, long sheetID)
    {
        return $"{address}/export?format=tsv&range={range}&gid={sheetID}";
    }
    public IEnumerator LoadData<T>(string RANGE, long SHEET_ID, Action<List<T>> onDataLoaded)
    {
        UnityWebRequest www = UnityWebRequest.Get(GetAddress(ADDRESS, RANGE, SHEET_ID));
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Failed to load data: {www.error}");
            yield break;
        }

        List<T> dataList = GetDatas<T>(www.downloadHandler.text);
        onDataLoaded?.Invoke(dataList);

    }
    T GetData<T>(string[] datas)
    {
        object data = Activator.CreateInstance(typeof(T));

        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        for (int i = 0; i < datas.Length; i++)
        {
            try
            {
                Type type = fields[i].FieldType;

                if (string.IsNullOrEmpty(datas[i])) continue;

                if (type == typeof(int))
                    fields[i].SetValue(data, int.Parse(datas[i]));

                else if (type == typeof(float))
                    fields[i].SetValue(data, float.Parse(datas[i]));

                else if (type == typeof(bool))
                    fields[i].SetValue(data, bool.Parse(datas[i]));

                else if (type == typeof(string))
                    fields[i].SetValue(data, datas[i]);

                #region enum 데이터 받기
                else if (type.IsEnum)
                    fields[i].SetValue(data, Enum.Parse(type, datas[i].Replace(" ","")));

                else if(type.IsArray && type.GetElementType().IsEnum)
                {
                    Type enumType = type.GetElementType();
                    string[] enumValues = datas[i].Split(','); // 데이터 구분 (예: "상큼한맛, 신맛")
                    Array enumArray = Array.CreateInstance(enumType, enumValues.Length);

                    for (int j = 0; j < enumValues.Length; j++)
                    {
                        enumArray.SetValue(Enum.Parse(enumType, enumValues[j].Trim().Replace(" ", "")), j);
                    }

                    fields[i].SetValue(data, enumArray);
                }
                #endregion

                #region sprite 데이터 받기
                else if (type == typeof(Sprite))
                    fields[i].SetValue(data, Resources.Load<Sprite>(datas[i].Trim()));

                else if (type.IsArray && type.GetElementType() == typeof(Sprite))
                {
                    Type spriteType = type.GetElementType();
                    string[] spriteValues = datas[i].Split(','); // "1001_1, 1001_2" -> ["1001_1", "1001_2"]
                    Array spriteArray = Array.CreateInstance(spriteType, spriteValues.Length);

                    for (int j = 0; j < spriteValues.Length; j++)
                    {
                        string spriteName = spriteValues[j].Trim();
                        Sprite sprite = Resources.Load<Sprite>(spriteName);

                        if (sprite == null)
                        {
                            Debug.LogWarning($"Sprite '{spriteName}'을(를) 찾을 수 없습니다. 경로를 확인하세요.");
                        }

                        spriteArray.SetValue(sprite, j);
                    }

                    fields[i].SetValue(data, spriteArray);
                }
                #endregion
            }

            catch (Exception e)
            {
                Debug.LogError($"SpreadSheet Error : {e.Message}");
            }
        }

        return (T)data;
    }
    List<T> GetDatas<T>(string data)
    {
        List<T> returnList = new List<T>();
        string[] splitedData = data.Split('\n');

        foreach (string s in splitedData)
        {
            string[] datas = s.Split("\t");
            returnList.Add(GetData<T>(datas));
        }

        return returnList;
    }
}
