using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShipRepair
{
    public class RepairSystem : MonoBehaviour
    {
        public Text brokenPartsText;
        private int brokenParts;
        public GameObject[] parts; // ������ ��ǰ��

        void Start()
        {
            brokenParts = Random.Range(1, parts.Length + 1); // ���Ƿ� ���峭 ���� �� ����
            UpdateBrokenPartsText();
            EnableBrokenParts();
        }

        void UpdateBrokenPartsText()
        {
            brokenPartsText.text = "���峭 ����: " + brokenParts.ToString();
        }

        void EnableBrokenParts()
        {
            for (int i = 0; i < brokenParts; i++)
            {
                int index = Random.Range(0, parts.Length);
                parts[index].SetActive(true);
            }
        }

        public void RepairPart()
        {
            if (brokenParts > 0)
            {
                brokenParts--;
                UpdateBrokenPartsText();
                if (brokenParts == 0)
                {
                    ShowRepairResult();
                }
            }
        }

        // ���Ӱ� �߰��� �޼���
        public int GetBrokenPartsCount()
        {
            return brokenParts; // ���� ���峭 ��ǰ�� ������ ��ȯ
        }

        void ShowRepairResult()
        {
            // ���� ���� ���θ� �Ǵ��ϴ� ���� �߰�
            bool isRepairSuccessful = GetBrokenPartsCount() == 0; // ��� ��ǰ�� �����Ǿ����� Ȯ��

            if (isRepairSuccessful)
            {
                Debug.Log("���� ����!");
                FindObjectOfType<TimerAndScore>().AddScore(10); // ���� �߰�
            }
            else
            {
                Debug.Log("���� ����!");
                // ���� �� �߰����� ���� (��: ���� ����, ���� ���� ���� ��)
            }
        }
    }
}