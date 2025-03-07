using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//GuestDB에 씬변경있음
public class DrinkDB : MonoBehaviour
{
    public static DrinkDB instance;

    public readonly string RANGE = "B3:L";
    public readonly long SHEET_ID = 1379756077;
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
        StartCoroutine(ReadData.instance.LoadData<DrinkInfo>(RANGE, SHEET_ID, dataList =>
        {
            DrinkDatas = dataList;
        }
        ));
    }
    [System.Serializable]
    public class DrinkInfo
    {
        [Header("음료 정보"), Tooltip("음료 이름")]
        public string Name;
        [Space, Tooltip("음료 종류")]
        public DrinkType drinkType;
        [Header("재료"), Tooltip("필수 재료")]
        public Matter[] RequiredMatter;
        [Tooltip("추가 재료 옵션")]
        public Matter[] ExtraMatter;
        [Tooltip("음료 특징"), TextArea(1, 10), Space]
        public string Feature;
        [Tooltip("제조 난이도")]
        public Difficulty difficulty;
        [Tooltip("판매 가격")]
        public int Price;
        [Tooltip("대만족 보너스")]
        public int Bonus;
        [Tooltip("만족도 영향도")]
        public Satisfaction satisfaction;
        [Tooltip("제작 시간")]
        public int ProduceTime;
        public Sprite DrinkSprite;
    }
    public enum DrinkType
    {
        None,
        주스,
        차,
        밀크쉐이크,
        칵테일,
        커피,
        밀크티,
        스무디,
        에이드
    }
    public enum Matter
    {
        None,
        얼음,
        은하水,
        스파이스슈가멜란지,
        우리운하목장우유,
        허니문,
        시트러스산레몬,
        카카오리온,
        스타베리,
        토성녹차
    }
    public enum Difficulty
    {
        None,
        쉬움,
        보통,
        어려움
    }
    public enum Satisfaction
    {
        None,
        낮음,
        평균,
        높음,
        완벽
    }
    [Space, SerializeField]
    public List<DrinkInfo> DrinkDatas = new List<DrinkInfo>();
}
