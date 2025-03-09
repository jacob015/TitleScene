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
    [Header("���")]
    public Image[] drinkImages; // �巡�� ������ ��� �迭
    public BoxCollider2D dropArea; // ��� ������ BoxCollider2D
    public GameObject MatterArea;

    private List<string> droppedMatter = new List<string>(); // ����� ��� �̸��� ������ ����Ʈ

    MakingSystem MakingSystem;
    GameUIManager GameUIManager;

    //private GameObject currentImage; // �巡�� ���� ���
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
        // �� �̹����� �巡�� �̺�Ʈ�� �߰��մϴ�.
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
        // �巡���� �̹����� ���� �Ӽ��� �״�� �����մϴ�.
        currentImage = new GameObject("DraggedImage");
        var imageComponent = currentImage.AddComponent<Image>();
        imageComponent.sprite = image.sprite; // �巡���� �̹����� ��������Ʈ ����

        // �̹��� �̸� ����
        currentImage.name = image.gameObject.name; // ���� �̹����� �̸����� ����

        // ���� �̹����� ȭ�鿡 ǥ���� �� �ֵ��� ����
        currentImage.transform.SetParent(transform, false); // �θ� ����
        currentImage.transform.position = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��ġ�� ����
        */
        MatterMouseCursor.SetActive(true);
        Image cursorImage = MatterMouseCursor.GetComponent<Image>();
        cursorImage.sprite = image.sprite;
        MatterMouseCursor.name = image.name;
        MatterMouseCursor.transform.position = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnDrag()
    {
        MatterMouseCursor.transform.position = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��ġ�� �̵�
    }

    public void OnEndDrag()
    {
        /*
        if (currentImage != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // BoxCollider2D�� ��� ���� Ȯ��
            if (dropArea.OverlapPoint(mousePos))
            {
                droppedMatter.Add(currentImage.name); // ����� ��� �̸��� ����Ʈ�� ����
                //Debug.Log($"��� ����: {currentImage.name}"); // ��� �̸� ���
                // ���� droppedMatter ����Ʈ�� ��� ��Ҹ� ���
                LogDroppedMat();
            }
            else
            {
                //Debug.Log("��� ����"); // ��� ���� �α�
            }

            //Destroy(currentImage); // �巡���� ��� ����
        }
        */
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // BoxCollider2D�� ��� ���� Ȯ��
        if (dropArea.OverlapPoint(mousePos))
        {
            if(GameUIManager.GetAddedMatters().Count <= 3)
            {
                MatterArea.transform.GetChild(GameUIManager.GetAddedMatters().Count).GetComponent<Image>().sprite = MatterMouseCursor.GetComponent<Image>().sprite;
                MakingSystem.AddMatter(MatterMouseCursor.name);
            }
            else
            {
                Debug.Log($"���� ���� ���� ���� {MatterMouseCursor.name}�� ���� ���Դϴ�");
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
    // �迭 �ʱ�ȭ �޼���
    public void ResetDroppedMat()
    {
        droppedMatter.Clear(); // ����Ʈ �ʱ�ȭ
        Debug.Log("����� ��� ����Ʈ�� �ʱ�ȭ�Ǿ����ϴ�.");
    }

    // ����� ��� ����Ʈ�� �������� ����ϴ� �޼���
    private void LogDroppedMat()
    {
        Debug.Log("���� ����� ��� ����Ʈ: " + string.Join(", ", droppedMatter));
    }
    */
}
