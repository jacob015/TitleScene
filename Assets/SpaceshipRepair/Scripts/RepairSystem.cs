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
        public GameObject[] parts; // 수리할 부품들

        void Start()
        {
            brokenParts = Random.Range(1, parts.Length + 1); // 임의로 고장난 부위 수 설정
            UpdateBrokenPartsText();
            EnableBrokenParts();
        }

        void UpdateBrokenPartsText()
        {
            brokenPartsText.text = "고장난 부위: " + brokenParts.ToString();
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

        // 새롭게 추가된 메서드
        public int GetBrokenPartsCount()
        {
            return brokenParts; // 현재 고장난 부품의 개수를 반환
        }

        void ShowRepairResult()
        {
            // 수리 성공 여부를 판단하는 로직 추가
            bool isRepairSuccessful = GetBrokenPartsCount() == 0; // 모든 부품이 수리되었는지 확인

            if (isRepairSuccessful)
            {
                Debug.Log("수리 성공!");
                FindObjectOfType<TimerAndScore>().AddScore(10); // 점수 추가
            }
            else
            {
                Debug.Log("수리 실패!");
                // 실패 시 추가적인 로직 (예: 점수 차감, 게임 상태 변경 등)
            }
        }
    }
}