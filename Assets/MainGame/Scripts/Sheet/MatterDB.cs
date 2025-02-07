using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//GuestDB에 씬변경있음
public class MatterDB : MonoBehaviour
{
    public static MatterDB instance;

    public readonly string RANGE = "B3:E";
    public readonly long SHEET_ID = 1343722466;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        LoadData();
    }
    public void LoadData()
    {
        StartCoroutine(ReadData.instance.LoadData<MatterInfo>(RANGE, SHEET_ID, dataList =>
        {
            MatterDatas = dataList;
        }
        ));
    }
    [System.Serializable]
    public class MatterInfo
    {
        [Header("재료 정보"), Tooltip("재료 이름")]
        public string Name;
        [Space, Tooltip("재료 원산지"), TextArea(1, 10)]
        public string Origin;
        [Space, Tooltip("설명"), TextArea(2, 10)]
        public string Explanation;
        [Space, Tooltip("주요 맛")]
        public Flavor[] Flavor;
    }
    public enum Flavor
    {
        none,
        신맛,
        쓴맛,
        상큼한맛,
        달콤한맛,
        부드러운맛,
        깊은맛,
        매운맛,
        떫은맛
    }
    [Space, SerializeField]
    public List<MatterInfo> MatterDatas = new List<MatterInfo>();
}
