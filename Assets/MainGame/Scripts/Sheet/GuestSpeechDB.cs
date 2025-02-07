using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//GuestDB에 씬변경있음
public class GuestSpeechDB : MonoBehaviour
{
    public static GuestSpeechDB instance;

    public readonly string RANGE = "B3:F";
    public readonly long SHEET_ID = 1134970709;
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
        StartCoroutine(ReadData.instance.LoadData<GuestSpeechInfo>(RANGE, SHEET_ID, dataList =>
        {
            GuestSpeechDatas = dataList;
        }
        ));
    }
    [System.Serializable]
    public class GuestSpeechInfo
    {
        [Header("NPC 대사 정보")]
        public int NPC_ID;
        [Tooltip("NPC 대화 유형")]
        public SpeechType speechType;
        [Tooltip("NPC 대사 카테고리")]
        public SpeechCategory speechCategory;
        [Tooltip("NPC 내용(주문/반응)"), TextArea(2, 10)]
        public string Reaction;
        [Space, Header("설명"), TextArea(1, 10)]
        public string Explanation;
    }
    public enum SpeechType
    {
        None,
        기본주문대사,
        힌트주문대사,
        대만족반응,
        만족반응,
        불만족반응
    }
    public enum SpeechCategory
    {
        None,
        주문,
        반응
    }
    [SerializeField]
    [ArrayElementTitle("NPC_ID")]
    public List<GuestSpeechInfo> GuestSpeechDatas = new List<GuestSpeechInfo>();
}
