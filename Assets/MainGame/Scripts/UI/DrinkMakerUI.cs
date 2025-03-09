using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class DrinkMakingManager : MonoBehaviour
{
    [Header("UI")]
    public Image GuestTimerGauge;
    public Image DrinkTimerGauge;
    [Header("기능")]
    public Image[] drinkImages; // 드래그 가능한 재료 배열
    public BoxCollider2D dropArea; // 드랍 영역의 BoxCollider2D
    public GameObject MatterArea;

    private List<string> droppedMatter = new List<string>(); // 드랍된 재료 이름을 저장할 리스트

    MakingSystem MakingSystem;
    GameUIManager GameUIManager;

    //private GameObject currentImage; // 드래그 중인 재료
    [SerializeField] GameObject MatterMouseCursor;

    Camera mainCam;
    void Awake()
    {
        MakingSystem = GameObject.Find("MakingSystem").GetComponent<MakingSystem>();
        GameUIManager = GameObject.Find("MainCanvas").GetComponent<GameUIManager>();
        mainCam = Camera.main;
    }
    IEnumerator Start()
    {
        // 각 이미지에 드래그 이벤트를 추가합니다.
        foreach (var image in drinkImages)
        {
            var eventTrigger = image.gameObject.AddComponent<EventTrigger>();
            AddDragEvent(eventTrigger, image);
        }
        yield return new WaitWhile(() => MakingSystem.LoadEnd == false);
        StartCoroutine(GameUIManager.PatienceTimer(GuestTimerGauge));
    }

    private void AddDragEvent(EventTrigger eventTrigger, Image image)
    {
        EventTrigger.Entry entryBeginDrag = new EventTrigger.Entry();
        entryBeginDrag.eventID = EventTriggerType.BeginDrag;
        entryBeginDrag.callback.AddListener((data) => { OnBeginDrag(image); });
        eventTrigger.triggers.Add(entryBeginDrag);

        EventTrigger.Entry entryDrag = new EventTrigger.Entry();
        entryDrag.eventID = EventTriggerType.Drag;
        entryDrag.callback.AddListener((data) => { OnDrag(); });
        eventTrigger.triggers.Add(entryDrag);

        EventTrigger.Entry entryEndDrag = new EventTrigger.Entry();
        entryEndDrag.eventID = EventTriggerType.EndDrag;
        entryEndDrag.callback.AddListener((data) => { OnEndDrag(); });
        eventTrigger.triggers.Add(entryEndDrag);
    }

    public void OnBeginDrag(Image image)
    {/*
        // 드래그할 이미지의 원본 속성을 그대로 복사합니다.
        currentImage = new GameObject("DraggedImage");
        var imageComponent = currentImage.AddComponent<Image>();
        imageComponent.sprite = image.sprite; // 드래그할 이미지의 스프라이트 복사

        // 이미지 이름 변경
        currentImage.name = image.gameObject.name; // 원본 이미지의 이름으로 설정

        // 현재 이미지를 화면에 표시할 수 있도록 설정
        currentImage.transform.SetParent(transform, false); // 부모 설정
        currentImage.transform.position = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치로 설정
        */
        MatterMouseCursor.SetActive(true);
        Image cursorImage = MatterMouseCursor.GetComponent<Image>();
        cursorImage.sprite = image.sprite;
        MatterMouseCursor.name = image.name;
        MatterMouseCursor.transform.position = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnDrag()
    {
        MatterMouseCursor.transform.position = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치로 이동
    }

    public void OnEndDrag()
    {
        /*
        if (currentImage != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // BoxCollider2D로 드랍 영역 확인
            if (dropArea.OverlapPoint(mousePos))
            {
                droppedMatter.Add(currentImage.name); // 드랍된 재료 이름을 리스트에 저장
                //Debug.Log($"드랍 성공: {currentImage.name}"); // 재료 이름 출력
                // 현재 droppedMatter 리스트의 모든 요소를 출력
                LogDroppedMat();
            }
            else
            {
                //Debug.Log("드랍 실패"); // 드랍 실패 로그
            }

            //Destroy(currentImage); // 드래그한 재료 삭제
        }
        */
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // BoxCollider2D로 드랍 영역 확인
        if (dropArea.OverlapPoint(mousePos))
        {
            if(GameUIManager.GetAddedMatters().Count <= 3)
            {
                MatterArea.transform.GetChild(GameUIManager.GetAddedMatters().Count).GetComponent<Image>().sprite = MatterMouseCursor.GetComponent<Image>().sprite;
                MakingSystem.AddMatter(MatterMouseCursor.name);
            }
            else
            {
                Debug.Log($"현재 통이 가득 차서 {MatterMouseCursor.name}은 넣지 못함니다");
            }
        }
        MatterMouseCursor.SetActive(false);
    }
    public void ResetMatter()
    {
        for (int i = 0; i < GameUIManager.GetAddedMatters().Count + 1; i++)
            MatterArea.transform.GetComponentsInChildren<Image>()[i].sprite = null;
    }
    /*
    // 배열 초기화 메서드
    public void ResetDroppedMat()
    {
        droppedMatter.Clear(); // 리스트 초기화
        Debug.Log("드랍된 재료 리스트가 초기화되었습니다.");
    }

    // 드랍된 재료 리스트를 문장으로 출력하는 메서드
    private void LogDroppedMat()
    {
        Debug.Log("현재 드랍된 재료 리스트: " + string.Join(", ", droppedMatter));
    }
    */
}
