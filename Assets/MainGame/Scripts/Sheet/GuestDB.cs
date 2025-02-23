using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GuestDB : MonoBehaviour
{
    public static GuestDB instance;

    public readonly string RANGE = "B4:I";
    public readonly long SHEET_ID = 0;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("MainGame");
    }
    void Start()
    {
        LoadData();
    }
    public void LoadData()
    {
        StartCoroutine(ReadData.instance.LoadData<GuestInfo>(RANGE, SHEET_ID, dataList =>
        {
            GuestDatas = dataList;
        }
        ));
    }
    [System.Serializable]
    public class GuestInfo
    {
        public int ID;
        [Header("NPC 정보"), Tooltip("NPC 이름")]
        public string Name;
        [Space, Tooltip("NPC 성격"), TextArea(1, 10)]
        public string Personality;
        [Tooltip("NPC 선호 맛")]
        public Flavor[] GoodFlavor;
        [Tooltip("NPC 비선호 맛")]
        public Flavor BadFlavor;
        [Tooltip("NPC 특징"), TextArea(2, 10)]
        public string Feature;
        [Tooltip("NPC 설명"), TextArea(3, 10)]
        public string Explanation;
        public Sprite GuestSprite;
    }
    public enum Flavor
    {
        None,
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
    public List<GuestInfo> GuestDatas = new List<GuestInfo>();
}