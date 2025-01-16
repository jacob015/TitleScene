using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TouchUp;

namespace TouchUp
{
    public class TileManager : SingletonMono<TileManager>
    {
        public GameObject backGround;

        public List<TileData> tileDatas = new List<TileData>();
        public List<GameObject> showTileObjects = new List<GameObject>();
        public List<GameObject> touchTileObjects = new List<GameObject>();

        public GridLayoutGroup layoutUP;
        public GridLayoutGroup layoutDown;
        public GaugeBar gauge;
        public int score;

        private int stage; // 현재 스테이지
        private int maxTile;
        private int currentTileCount; // 현재 맞추어야 할 타일의 순서
        private bool isMiss; // 스테이지에서 미스가 있었는 지?

        private List<int> finalTile = new List<int>();

        private void Start()
        {
            stage = 1;
            currentTileCount = 0;
            score = 0;
            SetStage();
        }

        private void SetShowTile(int count) // 한 줄에 몇개까지 정렬할 것인지
        {
            layoutUP.constraintCount = count;
            layoutUP.spacing = new Vector2(30, 30);
        }

        private void SetShowTile(int count, int spacing)
        {
            layoutUP.constraintCount = count;
            layoutUP.spacing = new Vector2(spacing, 30);
        }

        private void SetTouchTile(int row) // 몇 줄로 정렬할 것인지
        {
            layoutDown.constraintCount = row;
        }

        private void SetTouchTile(int row, int spacing) // 몇 줄로 정렬할 것인지
        {
            layoutDown.constraintCount = row;
            layoutDown.spacing = new Vector2(spacing, 30);
        }

        private void SetStage()
        {
            backGround.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

            switch (StageValue())
            {
                case 1:
                    SetShowTile(3);
                    SetTouchTile(2);

                    ActiveShowTile(3);
                    ActiveTouchTile(6);
                    break;
                case 2:
                    SetShowTile(4);
                    SetTouchTile(2);

                    ActiveShowTile(4);
                    ActiveTouchTile(6);
                    break;
                case 3:
                    SetShowTile(5, 10);
                    SetTouchTile(2);

                    ActiveShowTile(5);
                    ActiveTouchTile(8);

                    ActivePenalty("Rotate");
                    break;
                case 4:
                    SetShowTile(3);
                    SetTouchTile(2);

                    ActiveShowTile(6);
                    ActiveTouchTile(8);
                    ActivePenalty("UpsideDown");
                    break;
                case 5:
                    SetShowTile(4);
                    SetTouchTile(2, 10);

                    ActiveShowTile(8);
                    ActiveTouchTile(10);
                    ActivePenalty("Fade");
                    break;
                case 6:
                    SetShowTile(5, 10);
                    SetTouchTile(3);

                    ActiveShowTile(10);
                    ActiveTouchTile(12);
                    ActivePenalty("Mix");
                    break;
                case 0:
                    break;
            }
        }

        private int StageValue()
        {
            if (stage is >= 1 and <= 5)
            {
                return 1;
            }
            else if (stage is >= 6 and <= 10)
            {
                return 2;
            }
            else if (stage is >= 11 and <= 15)
            {
                return 3;
            }
            else if (stage is >= 16 and <= 20)
            {
                return 4;
            }
            else if (stage is >= 21 and <= 25)
            {
                return 5;
            }
            else if (stage is >= 26 and <= 30)
            {
                return 6;
            }
            else
            {
                return 0;
            }
        }

        public void TileTouch(TileData data) // 타일을 터치하였을 때,
        {
            if (showTileObjects[currentTileCount].GetComponent<Data>()._TileData == data)
            {
                showTileObjects[currentTileCount].GetComponent<TileOnlyShow>().RightTile();
                currentTileCount++;
            }
            else
            {
                // 현재 선택한 타일이 현재 순서의 타일과 다를 경우,
                isMiss = true;
                foreach (var tile in showTileObjects)
                {
                    if (!tile.activeSelf)
                        break;
                    tile.GetComponent<TileOnlyShow>().WrongTile();
                }

                currentTileCount = 0;
                Debug.Log("틀렸습니다!");
            }

            if (currentTileCount >= maxTile)
            {
                if (!isMiss)
                {
                    gauge.PlusTime(10);
                }

                stage++;
                isMiss = false;
                currentTileCount = 0;
                score += 10;
                SetStage();
                Debug.Log("스테이지 클리어!");
            }
        }

        private void ActiveTouchTile(int count) // 터치타일 배치
        {
            foreach (var tile in touchTileObjects)
            {
                if (tile.activeSelf)
                {
                    tile.SetActive(false);
                }

            }

            int[] a = new int[count];
            int random = 0;
            for (int i = 0; i < a.Length;)
            {
                random = Random.Range(0, 12);
                if (!a.Contains(random))
                {
                    a[i] = random;
                    if (!finalTile.Contains(a[i]))
                    {
                        finalTile.Add(a[i]);
                    }

                    if (finalTile.Count >= a.Length)
                    {
                        Debug.Log($"{finalTile.Count} : {a.Length}");
                        break;
                    }

                    i++;
                }
            }

            Shuffle(finalTile);

            for (int i = 0; i < a.Length; i++)
            {
                touchTileObjects[i].GetComponent<Data>()._TileData = tileDatas[finalTile[i]];
                touchTileObjects[i].SetActive(true);
            }

            finalTile.Clear();
        }

        private void ActiveShowTile(int count) // 보이는 타일 배치
        {
            foreach (var tile in showTileObjects)
            {
                if (tile.activeSelf)
                {
                    tile.SetActive(false);
                }

                tile.GetComponent<TilePenalty>().PenaltySet(PenaltyState.Idle);
            }

            int[] a = new int[count];
            int random = 0;
            for (int i = 0; i < a.Length; i++)
            {
                random = Random.Range(0, 12);
                a[i] = random;
                if (!finalTile.Contains(a[i]))
                {
                    finalTile.Add(a[i]);
                }
            }

            for (int i = 0; i < a.Length; i++)
            {
                showTileObjects[i].GetComponent<Data>()._TileData = tileDatas[a[i]];
                showTileObjects[i].SetActive(true);
            }

            maxTile = count;
        }

        private void ActivePenalty(string state)
        {
            switch (state)
            {
                case "Fade":
                    ApplyPenalty(PenaltyState.Fade);
                    break;
                case "Rotate":
                    ApplyPenalty(PenaltyState.Rotate);
                    break;
                case "UpsideDown":
                    backGround.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                    break;
                case "Mix":
                    ApplyPenalty(PenaltyState.Fade);
                    ApplyPenalty(PenaltyState.Rotate);
                    backGround.transform.rotation =
                        Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 2) == 0 ? 0 : 180));
                    break;
                default:
                    Debug.LogError("존재하지 않는 Penalty 입니다!");
                    break;
            }
        }

        private void ApplyPenalty(PenaltyState state)
        {
            foreach (var tile in showTileObjects)
            {
                if (!tile.activeSelf)
                    return;

                tile.GetComponent<TilePenalty>().PenaltySet(Random.Range(0, 2) == 0 ? PenaltyState.Idle : state);
            }
        }

        private void Shuffle(List<int> list) // 타일 섞기
        {
            for (int i = 0; i < list.Count; i++)
            {
                int temp = list[i];
                int randomIndex = Random.Range(i, list.Count);
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }
    }
}