using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMng : MonoBehaviour
{
    public static GameMng Instance;

    [SerializeField] Image starImage;
    [SerializeField] Image teacherImage;
    [SerializeField] Sprite[] teacherSprites;
    [SerializeField] Sprite[] changStarImages;
    [SerializeField] Animator animator;
    [SerializeField] Slider gage;
    [SerializeField] Text inGameScore;
    [SerializeField] GameObject pause;

    float[] notDanceTime = new float[2];
    float danceTime;
    float starRandomTime;
    float starChangeTime;
    float dancingTime;
    float endTime;
    [HideInInspector] public float lifeTime = 15;
    float score;
    float gameTime;

    [HideInInspector] public int buttonCount;
    public int randomStar;

    [HideInInspector] public bool isClick;
    [HideInInspector] public bool isDead;
    bool isDance;
    bool isCanDance;
    bool isEnd;
    bool isChange;
    bool isStop;
    bool isMusic;

    int point;

    float stopTime;
    float starMinTime;
    float starMaxTime;
    float notDanceTimeMin;
    float notDanceTimeMax;
    float danceTimeMin;
    float danceTimeMax;
    float targetTurnTime;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        Manager();
    }

    void Manager()
    {
        if(pause.activeSelf == false)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0.0f;
        }

        Dead();
        inGameScore.text = (int)score + "점";
        ScoreMng.Instance.score = score;
        if (isDead == false)
        {
            gameTime += Time.deltaTime;
            ImageChange();
            Dance();
            Difficulty();
            if(isStop == false)
            {
                animator.SetBool("isDance", isClick);
            }
            else
            {
                animator.SetBool("isDance", false);
            }
        }
        else if(isEnd == false)
        {
            if (isMusic == false)
            {
                SoundMng.Instance.MusicPlay("GameOver");
                isMusic = true;
            }
            teacherImage.sprite = teacherSprites[0];
            animator.SetBool("isDance", false);
            if (endTime < 2)
            {
                endTime += Time.deltaTime;
            }
            else
            {
                isEnd = true;
                SceneManager.LoadScene("Result");
            }
        }

        gage.value = lifeTime;
    }

    void Difficulty()
    {
        int difficulty = (int)(gameTime * 0.1f);
        if (difficulty == 0)
        {
            difficulty = 1;
        }
        switch (difficulty)
        {
            case 1:
                point = 1;
                starMinTime = 4;
                starMaxTime = 6;
                targetTurnTime = 1;
                notDanceTimeMin = 2;
                notDanceTimeMax = 4;
                danceTimeMin = 3;
                danceTimeMax = 5;
                break;
            case 2:
                point = 1;
                starMinTime = 3;
                starMaxTime = 5;
                targetTurnTime = 1;
                notDanceTimeMin = 2;
                notDanceTimeMax = 4;
                danceTimeMin = 3;
                danceTimeMax = 4;
                break;
            case 3:
                point = 2;
                starMinTime = 2;
                starMaxTime = 4;
                targetTurnTime = 0.7f;
                notDanceTimeMin = 2;
                notDanceTimeMax = 4;
                danceTimeMin = 2;
                danceTimeMax = 4;
                break;
            case 4:
                point = 2;
                starMinTime = 2;
                starMaxTime = 4;
                targetTurnTime = 0.7f;
                notDanceTimeMin = 2;
                notDanceTimeMax = 4;
                danceTimeMin = 1;
                danceTimeMax = 4;
                break;
            case 5:
                point = 3;
                starMinTime = 2;
                starMaxTime = 3;
                targetTurnTime = 0.7f;
                notDanceTimeMin = 1;
                notDanceTimeMax = 3;
                danceTimeMin = 1;
                danceTimeMax = 3;
                break;
            case 6:
                point = 4;
                starMinTime = 1;
                starMaxTime = 3;
                targetTurnTime = 0.5f;
                notDanceTimeMin = 1;
                notDanceTimeMax = 3;
                danceTimeMin = 1;
                danceTimeMax = 3;
                break;
            case 7:
                point = 5;
                starMinTime = 1;
                starMaxTime = 2;
                targetTurnTime = 0.5f;
                notDanceTimeMin = 1;
                notDanceTimeMax = 3;
                danceTimeMin = 1;
                danceTimeMax = 3;
                break;
        }
    }

    void ImageChange()
    {
        if(starChangeTime < starRandomTime)
        {
            starChangeTime += Time.deltaTime;
            if(starChangeTime > starRandomTime - 1f)
            {
                if(isChange == false)
                {
                    StartCoroutine(ChangeStarImage());
                    isChange = true;
                }
            }
        }
        else
        {
            randomStar = Random.Range(1, 4);
            starChangeTime = 0;
            starRandomTime = Random.Range(starMinTime, starMaxTime);
        }

        if (randomStar == 0)
        {
            starImage.sprite = changStarImages[randomStar];
        }
        else
        {
            starImage.sprite = changStarImages[randomStar - 1];
        }

        if(isCanDance == false)
        {
            teacherImage.sprite = teacherSprites[1];
        }
        else
        {
            teacherImage.sprite = teacherSprites[0];
        }
    }

    void Dance()
    {
        if(notDanceTime[0] < notDanceTime[1])
        {
            notDanceTime[0] += Time.deltaTime;
        }
        else if(isDance == false)
        {
            isDance = true;
            notDanceTime[1] = Random.Range(notDanceTimeMin, notDanceTimeMax);
            danceTime = Random.Range(danceTimeMin, danceTimeMax);
        }

        if(isDance == true)
        {
            if (dancingTime < danceTime)
            {
                isCanDance = true;
                dancingTime += Time.deltaTime;
                if (isClick == true && isStop == false)
                {
                    if(randomStar == buttonCount)
                    {
                        score += Time.deltaTime * point;
                        if (lifeTime < 30)
                        {
                            lifeTime += Time.deltaTime * 2;
                        }
                        else
                        {
                            lifeTime = 30;
                        }
                    }
                    else
                    {
                        isStop = true;
                    }
                }

                if(dancingTime > danceTime - targetTurnTime)
                {
                    teacherImage.sprite = teacherSprites[2];
                }
            }
            else
            {
                isCanDance = false;
                isDance = false;
                notDanceTime[0] = 0;
                dancingTime = 0;
            }
        }

        if(isStop == true)
        {
            if (stopTime < 2)
            {
                stopTime += Time.deltaTime;
                Debug.Log("Stop");
            }
            else
            {
                isStop = false;
                stopTime = 0;
            }
        }
    }

    void Dead()
    {
        if(lifeTime > 0 )
        {
            if(isClick == false && randomStar != buttonCount)
            {
                lifeTime -= Time.deltaTime;
            }
        }
        else
        {
            isDead = true;
        }

        if(isClick == true && isCanDance == false)
        {
            isDead = true;
        }
    }

    IEnumerator ChangeStarImage()
    {
        yield return new WaitForSeconds(0.25f);
        starImage.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(0.25f);
        starImage.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.25f);
        starImage.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(0.25f);
        starImage.color = new Color(1, 1, 1, 1);
        isChange = false;
    }
}
