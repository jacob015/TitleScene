using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace PlanetMerge
{
    public class GenerateManager : MonoBehaviour
    {
        public float xPos;
        public float yPos;
        public float RemoveYPos;
        public bool isDrag;
        public bool isCool;

        public GameObject plants;
        public GameObject pool;
        public PlanetDB.PlanetInfo planetInfo;
        public int RandonValue;
        public int ran;
        private void Start()
        { 
            Rool();
            GameManager.instance.IsPause(true);
            StartCoroutine(GameManager.instance.GameStart());
        }
        void Update()
        {
            if (GameManager.instance.Score >= GameManager.instance.MaxScore) GameManager.instance.Score = GameManager.instance.MaxScore;
            if (GameManager.instance.Score <= GameManager.instance.MinScore) GameManager.instance.Score = GameManager.instance.MinScore;

            GameManager.instance.ScoreText.text = GameManager.instance.Score.ToString();
            GameManager.instance.HighScoreText.text = GameManager.instance.HighScore.ToString();

            GameManager.instance.MenuScoreText.text = GameManager.instance.Score.ToString();
            GameManager.instance.MenuHighScoreText.text = GameManager.instance.HighScore.ToString();

            if (!isCool && !GameManager.instance.isOver)
            {
                GameManager.instance.Timer();
                if (isDrag)
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if(mousePos.y <= yPos) mousePos.y = yPos;
                    mousePos.z = 0;
                    float LeftBorder = -xPos + transform.localScale.x / 2;
                    float RightBorder = xPos - transform.localScale.x / 2;

                    if(mousePos.x < LeftBorder) mousePos.x = LeftBorder;
                    else if(mousePos.x > RightBorder) mousePos.x = RightBorder;

                    transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f);
                }
            }
        }
        void Rool()
        {
            isCool = false;
            #region Ä«¿îÆ®
            if (GameManager.instance.Count == 0)
                GameManager.instance.RanNum = 1;
            if(GameManager.instance.Count >= 1 && GameManager.instance.Count <= 3)
                GameManager.instance.RanNum = 2;
            if(GameManager.instance.Count >= 4 && GameManager.instance.Count <= 6)
                GameManager.instance.RanNum = 3;
            if(GameManager.instance.Count > 6)
            {
                ran = Random.Range(0, RandonValue);
                Debug.Log(ran);
                GameManager.instance.RanNum = 4;
            }
            #endregion
            if(ran != 1)
                planetInfo = PlanetDB.instance.PlanetDatas[Random.Range(0, GameManager.instance.RanNum)];
            else
                planetInfo = PlanetDB.instance.PlanetDatas[10];
            transform.position = new Vector2(0, yPos);
            transform.localScale = new Vector3(planetInfo.Size, planetInfo.Size, planetInfo.Size);
            GetComponent<SpriteRenderer>().sprite = planetInfo.Image;
        }
        public void Drag()
        {
            isDrag = true;
        }
        public void Drop()
        {
            if (isDrag && !isCool && !GameManager.instance.isOver)
            {
                isDrag = false;
                isCool = true;
                GameManager.instance.TimerImage.gameObject.GetComponent<AudioSource>().Stop();
                if (transform.position.y >= RemoveYPos) Trash();
                else
                {
                    GameObject Gme = Instantiate(plants, pool.transform);
                    Gme.transform.position = transform.position;
                    Gme.GetComponent<Planet>().PlanetData = planetInfo;
                }
                GameManager.instance.TimerImage.fillAmount = 1;
                GameManager.instance.TimerImage.color = Color.green;
                GameManager.instance.Count++;
                StartCoroutine(cooltime());
            }
        }
        void Trash()
        {
            GetComponent<AudioSource> ().Play();
            if(planetInfo == PlanetDB.instance.PlanetDatas[10])
            {

            }
            else
            {
                GameManager.instance.Score -= 50;
                GameManager.instance.WrongCount++;
            }
        }
        IEnumerator cooltime()
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(0.5f);
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            Rool();
        }
    }
}
