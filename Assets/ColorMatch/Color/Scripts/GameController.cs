using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;
namespace ColorMatch
{
    public enum MeteoriteColor
    {
        Red,
        Blue,
        Yellow,
        Green,
        Purple
    }
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        public List<MeteoriteColor> color;
        public List<int> NextLine;
        [Header("Time")]
        [SerializeField]
        Image GameTimer;
        public float timer;
        float colorTime;
        Color color1 = Color.green;
        Color color2 = Color.yellow;
        public bool isPause;
        [Header("HP")]
        [SerializeField]
        GameObject GameHP;
        [SerializeField] int HP;
        [SerializeField] Image DamageImage;
        [Header("Score")]
        [SerializeField]
        TextMeshProUGUI ScoreText;
        public int Score;
        [Header("End")]
        [SerializeField]
        public GameObject EndGameUI;
        [SerializeField]
        TextMeshProUGUI TitleText;
        [SerializeField]
        TextMeshProUGUI NewScoreText;
        [SerializeField]
        TextMeshProUGUI BestScoreText;
        [Header("Camera")]
        [SerializeField]
        Transform cam;
        float shakeTime;
        float shakeIntensity;
        Coroutine shakeCamRoutineCoroutine;
        [Header("aim")]
        [SerializeField]
        Transform Aim;
        [Header("Game")]
        [SerializeField]
        TextMeshProUGUI countdownText;
        [SerializeField]
        Animator MainCon;
        [SerializeField]
        Animator GameCon;
        [SerializeField]
        GameObject ButtonCover;
        [SerializeField]
        GameObject WarningObject;
        [SerializeField]
        GameObject Set;
        public bool GameHardAble;
        public GameObject pool;
        [Header("Sound")]
        public AudioSource LaserClip;
        public AudioSource DamageClip;

        public AudioClip gameoveraudio;
        public AudioClip gameendaudio;

        void Start()
        {
            colorTime = 0;
            HP = 3;
            StartCoroutine(GameStart());
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Timer();
            TimerColor();
            ScoreText.text = Score.ToString("D3");
            if (HP <= 0)
            {
                Dead();
            }
            for (int i = 0; i < 3; i++)
            {
                if (HP > i)
                    GameHP.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                else
                    GameHP.transform.GetChild(i).GetComponent<Image>().color = Color.black;
            }
        }

        public void ColorButton(int colornum)
        {
            if (!EndGameUI.activeSelf && !countdownText.gameObject.activeSelf && !WarningObject.activeSelf && !DamageImage.gameObject.activeSelf)
            {
                if (colornum == ((int)color[0]))
                {
                    LaserClip.Play();
                    GameObject obj = ObjectPool.instance.objectPoolList[1].Dequeue();
                    obj.transform.localPosition = Aim.GetChild(NextLine[0]).transform.position;
                    obj.SetActive(true);
                    color.RemoveAt(0);
                    NextLine.RemoveAt(0);
                }
                else
                {
                    Damage(1);
                }
            }
        }

        public void IsPause()
        {
            isPause = !isPause;
            if (isPause)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        public void Damage(int damage)
        {
            DamageClip.Play();
            if (HP <= 0)
                Dead();
            else
            {
                HP -= damage;
                if (HP <= 0)
                    Dead();
                StartCoroutine(DamageEffect());
                if (HP > 0)
                    ShakeCam(0.2f, 0.1f);
            }
        }
        IEnumerator DamageEffect()
        {
            DamageImage.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(0.1f);
            DamageImage.gameObject.SetActive(false);
        }
        void Dead()
        {
            HP = 0;
            pool.SetActive(false);
            StartCoroutine(SwichCon());
            EndGameUI.GetComponent<AudioSource>().clip = gameoveraudio;
            EndGameUI.SetActive(true);
            TitleText.text = "GAME OVER";
            NewScoreText.text = Score.ToString("D3");
            BestScoreText.text = Score.ToString("D3");
        }
        void END()
        {
            pool.SetActive(false);
            StartCoroutine(SwichCon());
            EndGameUI.GetComponent<AudioSource>().clip = gameendaudio;
            EndGameUI.SetActive(true);
            TitleText.text = "GAME END";
            NewScoreText.text = Score.ToString("D3");
            BestScoreText.text = Score.ToString("D3");
        }
        void Timer()
        {
            GameTimer.fillAmount -= 1 * Time.deltaTime / timer;
            if (GameTimer.fillAmount <= 0.3f && !GameHardAble && HP > 0)
                StartCoroutine(GameHard());
            if (GameTimer.fillAmount <= 0)
                END();
        }
        void TimerColor()
        {
            colorTime += Time.deltaTime / timer;
            if (GameTimer.color == Color.yellow)
            {
                color1 = Color.yellow;
                color2 = Color.red;
                if (colorTime >= 0.5f)
                    colorTime = 0f;
            }
            GameTimer.color = Color.Lerp(color1, color2, colorTime * 2);
        }
        public void ShakeCam(float _shakeTime, float _shakeIntensity)
        {
            shakeTime = _shakeTime;
            shakeIntensity = _shakeIntensity;

            if (shakeCamRoutineCoroutine != null) StopCoroutine(shakeCamRoutineCoroutine);
            shakeCamRoutineCoroutine = StartCoroutine(ShakeCamRoutine());
        }

        IEnumerator ShakeCamRoutine()
        {
            while (shakeTime > 0)
            {
                if (Time.timeScale == 0)
                    break;
                cam.localPosition += UnityEngine.Random.insideUnitSphere * shakeIntensity;

                shakeTime -= Time.deltaTime;

                yield return null;
            }
            cam.localPosition = new Vector3(0, 0, -10);
        }
        IEnumerator GameStart()
        {
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(GameCon.GetCurrentAnimatorStateInfo(0).length);
            countdownText.gameObject.SetActive(true);
            countdownText.text = "3";
            yield return new WaitForSecondsRealtime(1);
            countdownText.text = "2";
            yield return new WaitForSecondsRealtime(1);
            countdownText.text = "1";
            yield return new WaitForSecondsRealtime(1);
            countdownText.text = "GO!";
            yield return new WaitForSecondsRealtime(1);
            color.Clear();
            countdownText.gameObject.SetActive(false);
            Time.timeScale = 1f; // 게임 시작
        }
        IEnumerator GameHard()
        {
            GameHardAble = true;
            Time.timeScale = 0f;
            ButtonCover.GetComponent<Animator>().SetTrigger("OnHardMod");
            WarningObject.SetActive(true);
            yield return new WaitForSecondsRealtime(ButtonCover.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            yield return new WaitForSecondsRealtime(WarningObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            WarningObject.SetActive(false);
            ButtonCover.SetActive(false);
            Time.timeScale = 1f;
        }
        public IEnumerator SwichCon()
        {
            if(Time.timeScale == 0f)
            {
                MainCon.Play("CoverDown");
                GameCon.Play("CoverUp");
                yield return new WaitForSecondsRealtime(GameCon.GetCurrentAnimatorStateInfo(0).length);
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
                GameCon.Play("CoverDown");
                MainCon.Play("CoverUp");
            }
        }
        public void SetButton()
        {
            if(!countdownText.gameObject.activeSelf && !WarningObject.activeSelf && !EndGameUI.activeSelf)
            {
                Set.SetActive(!Set.activeSelf);
                StartCoroutine(SwichCon());
            }
        }
    }
}