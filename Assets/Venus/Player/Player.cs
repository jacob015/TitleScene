using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Temp
{
    public class Player : MonoBehaviour
    {
        [SerializeField] GameObject fuelBar;
        public float maxFuel;
        [SerializeField] TextMeshProUGUI ScoreText;
        [SerializeField] GameManager GM;
        [SerializeField] GameObject GameOverOBJ;
        public float Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
                ScoreText.text = value.ToString();
            }
        }
        float score;
        public float Speed = 10;
        public float NowFuel
        {
            get
            {
                return nowFuel;
            }
            set
            {
                if(nowFuel != value)
                {
                    if (value <= 0)
                    {
                        value = 0;
                        GameOver();
                    }
                    else if (value > maxFuel)
                        value = maxFuel;
                    nowFuel = value;
                    UsedFuel();
                }
            }
        }
        [SerializeField] float nowFuel;
        [SerializeField] int BestScore;
        [SerializeField] int PlayerBestScore;
        public SoundSetting SoundSetting;
        [SerializeField] AudioClip GameOverSound;
        [SerializeField] AudioClip HitSound;
        [SerializeField] Sprite HitSprite;
        [SerializeField] Sprite NormalSprite;
        [SerializeField] Image PlayerImage;
        WaitForSeconds WaitForSeconds = new WaitForSeconds(0.5f);
        public float MinusFuelValue;
        void Awake()
        {
            MinusFuelValue = 0.6f;
            maxFuel *= 2;
            nowFuel = maxFuel;
            Application.targetFrameRate = 60;
        }
        void Start()
        {
            StartCoroutine(GoUp());
            StartCoroutine(scoreUp());
            GameObject OBJ = GameObject.Find("BestScoreToss");
            if (OBJ != null)
            {
                BestScore = Mathf.RoundToInt(OBJ.transform.position.x);
                Destroy(OBJ);
            }
            GameObject OBJ1 = GameObject.Find("PlayerBestScoreToss");
            if (OBJ1 != null)
            {
                PlayerBestScore = Mathf.RoundToInt(OBJ1.transform.position.x);
                Destroy(OBJ1);
            }
        }
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Obstacle"))
            {
                NowFuel -= MinusFuelValue;
                Vibration.Vibrate(300);
                Obstacle obstacle = collision.GetComponent<Obstacle>();
                if (obstacle.type == Obstacle.Type.Bird || obstacle.type == Obstacle.Type.Balloon)
                {
                    SoundSetting.EffectAudio.clip = HitSound;
                    SoundSetting.EffectAudio.Play();
                }
                StartCoroutine(SpriteChangeToOrigin());
                PlayerImage.sprite = HitSprite;
                Destroy(collision.gameObject);
            }
        }
        IEnumerator SpriteChangeToOrigin()
        {
            yield return WaitForSeconds;
            PlayerImage.sprite = NormalSprite;
        }
        void GameOver()
        {
            GameOverOBJ.SetActive(true);
            GameOverOBJ.transform.SetAsLastSibling();
            GameOverOBJ.transform.GetChild(0).Find("PlayerRank").GetComponent<TextMeshProUGUI>().text = Score.ToString();
            GameOverOBJ.transform.GetChild(0).Find("Rank1").GetComponent<TextMeshProUGUI>().text = BestScore.ToString();
            GameOverOBJ.transform.GetChild(0).Find("PlayerTopRank").GetComponent<TextMeshProUGUI>().text = PlayerBestScore.ToString();
            Time.timeScale = 0;
            SoundSetting.EffectAudio.clip = GameOverSound;
            SoundSetting.EffectAudio.Play();
        }
        IEnumerator scoreUp()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(1.5f);
            while (true)
            {
                yield return waitForSeconds;
                Score++;
            }
        }
        IEnumerator GoUp()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);
            while (true)
            {
                yield return waitForSeconds;
                NowFuel -= 0.2f;
                if (Speed < 100)
                    Speed += 0.05f;
            }
        }
        void UsedFuel()
        {
            fuelBar.transform.localScale = new Vector3(VarValue(NowFuel, maxFuel), 1, 1);
        }
        public float VarValue(float Now = 1, float Max = 1)
        {
            return Now / Max;
        }
        public void GameEnd()
        {
            GameObject obj = new GameObject("ScoreToss");
            obj.transform.position = new Vector3(Score, 0, 0);
            DontDestroyOnLoad(obj);
            GameObject obj1 = new GameObject("GameSoundToss");
            obj1.transform.position = new Vector3(SoundSetting.GameSoundSlider.value, 0, 0);
            DontDestroyOnLoad(obj1);
            GameObject obj2 = new GameObject("EffectSoundToss");
            obj2.transform.position = new Vector3(SoundSetting.EffectSoundSlider.value, 0, 0);
            DontDestroyOnLoad(obj2);
            Time.timeScale = 1;
            SceneManager.LoadScene("TempMain");
        }
    }
}