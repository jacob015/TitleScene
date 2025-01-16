using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Temp
{
    public class GameManager : MonoBehaviour
    {
        public int BackGroundSpriteIndex;
        [SerializeField] RectTransform[] BackGround = new RectTransform[2];
        [SerializeField] Sprite[] BackGroundSprite = new Sprite[8];
        [SerializeField] Player player;
        [SerializeField] GameObject canvas;
        [SerializeField] GameObject[] Stars = new GameObject[3];
        List<RectTransform>[] StarList = new List<RectTransform>[3];
        List<RectTransform> S = new List<RectTransform>();
        List<RectTransform> M = new List<RectTransform>();
        List<RectTransform> L = new List<RectTransform>();
        Dictionary<string, Queue<GameObject>> StarPool = new Dictionary<string, Queue<GameObject>>();
        Queue<GameObject> Spool = new Queue<GameObject>();
        Queue<GameObject> Mpool = new Queue<GameObject>();
        Queue<GameObject> Lpool = new Queue<GameObject>();
        string[] Type = { "Small", "Medium", "Large" };

        [SerializeField] GameObject FuelPrefab;
        List<RectTransform> Fuel = new List<RectTransform>();
        Queue<GameObject> FuelPool = new Queue<GameObject>();
        [SerializeField] AudioClip FuelSound;
        [SerializeField] AudioClip StarClick;

        [SerializeField] GameObject[] Airplanes;
        [SerializeField] GameObject[] Birds;
        [SerializeField] GameObject[] Meteors;
        [SerializeField] GameObject[] Clouds; 
        [SerializeField] GameObject[] Aurora;
        [SerializeField] GameObject[] Stones; 
        [SerializeField] GameObject[] Ballons; 
        [SerializeField] GameObject[] Satellite;
        WaitForSeconds WaitForSeconds = new WaitForSeconds(1);
        IEnumerator Start()
        {
            StarList[0] = S;
            StarList[1] = M;
            StarList[2] = L;
            StarPool["Small"] = Spool;
            StarPool["Medium"] = Mpool;
            StarPool["Large"] = Lpool;
            StartCoroutine(SummonObstacle());
            while (true)
            {
                float PlayerFuel = 1 - player.VarValue(player.NowFuel, player.maxFuel);
                yield return new WaitForSeconds(Random.Range(2f - PlayerFuel, 3.5f));
                SummonFuel();
            }
        }
        IEnumerator SummonObstacle()
        {
            bool Space = false;
            bool Planet1 = false;
            bool Planet2 = false;
            bool Planet3 = false;
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(0.5f, 2f));
                if (BackGroundSpriteIndex < 2)//대류권
                {
                    SummonBird();
                    SummonBallon();
                }
                else if (BackGroundSpriteIndex < 4)//성층권
                {
                    SummonAir();
                    SummonCloud();
                }
                else if (BackGroundSpriteIndex < 6)//열권
                {
                    SummonAurora();
                    SummonCloud();
                }
                else if (BackGroundSpriteIndex < 7)//우주지구사이
                {
                    if (!Space)
                        player.MinusFuelValue = 1f;
                    SummonStone();
                }
                else if (BackGroundSpriteIndex < 14)//우주
                {
                    if (!Planet1)
                        player.MinusFuelValue = 1.4f;
                    if (BackGroundSpriteIndex == 8 && !Planet2)
                        player.MinusFuelValue = 1.8f;
                    else if (BackGroundSpriteIndex == 11 && !Planet3)
                        player.MinusFuelValue = 2.4f;
                    SummonMeteor();
                    SummonSatellite();
                }
            }
        }
        void SummonMeteor()
        {
            if (Random.Range(0, 2) == 0)
            {
                float Direction = 6;
                if (Random.Range(0, 2) == 0)
                    Direction = -6f;
                GameObject OBJ = Instantiate(Meteors[Random.Range(0, Meteors.Length)], new Vector3(Direction, Random.Range(4f, 6f), 0), Quaternion.identity);
                Obstacle obstacle = OBJ.GetComponent<Obstacle>();
                obstacle.SoundVolume = player.SoundSetting.EffectSoundSlider.value / 4f;
            }
        }
        void SummonSatellite()
        {
            if (Random.Range(0, 8) == 0)
            {
                GameObject OBJ = Instantiate(Satellite[Random.Range(0, Satellite.Length)], new Vector3(XRandom(), 6.5f, 0), Quaternion.identity);
                Obstacle obstacle = OBJ.GetComponent<Obstacle>();
                obstacle.SoundVolume = player.SoundSetting.EffectSoundSlider.value / 4f;
            }
        }
        void SummonBallon()
        {
            if (Random.Range(0, 6) == 0)
            {
                GameObject OBJ = Instantiate(Ballons[Random.Range(0, Ballons.Length)], new Vector3(XRandom(), -5f, 0), Quaternion.identity);
                Obstacle obstacle = OBJ.GetComponent<Obstacle>();
                obstacle.SoundVolume = player.SoundSetting.EffectSoundSlider.value / 4f;
            }
        }
        void SummonStone()
        {
            if (Random.Range(0, 5) == 0)
            {
                GameObject OBJ = Instantiate(Stones[Random.Range(0, Stones.Length)], new Vector3(XRandom(), 6.5f, 0), Quaternion.identity);
                Obstacle obstacle = OBJ.GetComponent<Obstacle>();
                obstacle.SoundVolume = player.SoundSetting.EffectSoundSlider.value / 4f;
            }
        }
        void SummonCloud()
        {
            if (Random.Range(0, 3) == 0)
            {
                GameObject OBJ = Instantiate(Clouds[Random.Range(0, Clouds.Length)], new Vector3(XRandom(), Random.Range(-3.5f, 5.5f), 0), Quaternion.identity);
                Obstacle obstacle = OBJ.GetComponent<Obstacle>();
                obstacle.SoundVolume = player.SoundSetting.EffectSoundSlider.value / 4f;
            }
        }
        void SummonAurora()
        {
            if (Random.Range(0, 3) == 0)
            {
                GameObject OBJ = Instantiate(Aurora[Random.Range(0, Aurora.Length)], new Vector3(XRandom(), Random.Range(-3.5f, 5.5f), 0), Quaternion.identity);
                Obstacle obstacle = OBJ.GetComponent<Obstacle>();
                obstacle.SoundVolume = player.SoundSetting.EffectSoundSlider.value / 4f;
            }
        }
        void SummonAir()
        {
            if (Random.Range(0, 10) == 0)
            {
                GameObject OBJ = Instantiate(Airplanes[Random.Range(0, Airplanes.Length)], new Vector3(5, YRandom(), 0), Quaternion.identity);
                Obstacle obstacle = OBJ.GetComponent<Obstacle>();
                obstacle.SoundVolume = player.SoundSetting.EffectSoundSlider.value / 4f;
            }
        }
        void SummonBird()
        {
            if (Random.Range(0, 4) == 0)
            {
                GameObject OBJ = Instantiate(Birds[Random.Range(0, Birds.Length)], new Vector3(5, YRandom(), 0), Quaternion.identity);
                Obstacle obstacle = OBJ.GetComponent<Obstacle>();
                obstacle.SoundVolume = player.SoundSetting.EffectSoundSlider.value / 4f;
            }
        }
        float YRandom()
        {
            return Random.Range(-3.5f, 3.5f);
        }
        float XRandom()
        {
            return Random.Range(-2.3f, 2.3f);
        }
        IEnumerator SummonStar()
        {
            while (true)
            {
                GameObject OBJ = null;
                RectTransform rect = null;
                yield return new WaitForSeconds(Random.Range(0.1f, 2));
                int RandomIndex = Random.Range(0, 3);
                if (StarPool[Type[RandomIndex]].Count > 0)
                {
                    OBJ = StarPool[Type[RandomIndex]].Dequeue();
                    rect = OBJ.GetComponent<RectTransform>();
                }
                else
                {
                    OBJ = Instantiate(Stars[RandomIndex]);
                    rect = OBJ.GetComponent<RectTransform>();
                    EventTrigger eventTrigger = OBJ.GetComponent<EventTrigger>();
                    EventTrigger.Entry entry = new EventTrigger.Entry();
                    entry.eventID = EventTriggerType.PointerDown;
                    if (RandomIndex == 0)
                        entry.callback.AddListener(delegate { SStar(rect); });
                    else if (RandomIndex == 1)
                        entry.callback.AddListener(delegate { MStar(rect); });
                    else if (RandomIndex == 2)
                        entry.callback.AddListener(delegate { LStar(rect); });
                    eventTrigger.triggers.Add(entry);
                    OBJ.GetComponent<AudioSource>().clip = StarClick;
                }
                StarList[RandomIndex].Add(rect);
                OBJ.SetActive(true);
                OBJ.transform.SetParent(canvas.transform);

                rect.anchoredPosition = new Vector2(Random.Range(-500f, 500f), 1050f);
                rect.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        void Update()
        {
            StarMove();
            FuelMove();
            float deltaTime;
            deltaTime = Time.deltaTime;
            for (int i = 0; i < BackGround.Length; i++)
            {
                if (BackGround[i].anchoredPosition.y <= -2415)
                {
                    BackGround[i].anchoredPosition = new Vector2(0, 3405);
                    if (i == 0)
                    {
                        BackGround[1].anchoredPosition = new Vector2(0, 495);
                    }
                    else
                    {
                        BackGround[0].anchoredPosition = new Vector2(0, 495);
                    }
                    BackGround[i].GetComponent<Image>().sprite = BackGroundSprite[BackGroundSpriteIndex];
                    BackGroundSpriteIndex++;
                    if (BackGroundSpriteIndex == 6)
                        StartCoroutine(SummonStar());
                    else if (BackGroundSpriteIndex == 14)
                        BackGroundSpriteIndex = 7;
                    player.Speed += 0.05f;
                }
                BackGround[i].anchoredPosition -= new Vector2(0, 10 * player.Speed * deltaTime);
            }
        }
        void SummonFuel()
        {
            GameObject OBJ = null;
            RectTransform rect = null;
            if (FuelPool.Count > 0)
            {
                OBJ = FuelPool.Dequeue();
                rect = OBJ.GetComponent<RectTransform>();
            }
            else
            {
                OBJ = Instantiate(FuelPrefab);
                rect = OBJ.GetComponent<RectTransform>();
                EventTrigger eventTrigger = OBJ.GetComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                entry.callback.AddListener(delegate { FuelBt(rect); });
                eventTrigger.triggers.Add(entry);
                OBJ.GetComponent<AudioSource>().clip = FuelSound;
            }
            Fuel.Add(rect);
            OBJ.SetActive(true);
            OBJ.transform.SetParent(canvas.transform);

            rect.anchoredPosition = new Vector2(Random.Range(-500f, 500f), 1050f);
            rect.transform.localScale = new Vector3(1, 1, 1);
        }
        void StarMove()
        {
            for (int i = 0; i < StarList.Length; i++)
            {
                for (int j = 0; j < StarList[i].Count; j++)
                {
                    StarList[i][j].anchoredPosition -= new Vector2(0, 20 * player.Speed * Time.deltaTime * 1.3f);
                }
            }

            for (int i = 0; i < StarList.Length; i++)
            {
                for (int j = 0; j < StarList[i].Count; j++)
                {
                    if (StarList[i][j].anchoredPosition.y < -1050f)
                    {
                        GameObject OBJ = StarList[i][j].gameObject;
                        StarPool[Type[i]].Enqueue(OBJ);
                        if (i == 0)
                            S.Remove(StarList[i][j]);
                        else if (i == 1)
                            M.Remove(StarList[i][j]);
                        else if (i == 2)
                            L.Remove(StarList[i][j]);
                        OBJ.SetActive(false);
                        
                    }
                }
            }
        }
        void FuelMove()
        {
            for (int i = 0; i < Fuel.Count; i++)
            {
                Fuel[i].anchoredPosition -= new Vector2(0, 20 * player.Speed * Time.deltaTime);
            }
            for (int i = 0; i < Fuel.Count; i++)
            {
                if (Fuel[i].anchoredPosition.y < -1050f)
                {
                    RectTransform OBJ = Fuel[i];
                    
                    OBJ.gameObject.SetActive(false);
                    FuelPool.Enqueue(OBJ.gameObject);
                    Fuel.Remove(OBJ);
                }
            }
        }
        public void FuelBt(RectTransform OBJ)
        {
            AudioSource audio = OBJ.transform.GetComponent<AudioSource>();
            audio.volume = player.SoundSetting.EffectSoundSlider.value / 4f;
            audio.Play();
            player.NowFuel += 5;
            OBJ.transform.position = new Vector3(1000, 1000, 0);
            StartCoroutine(FuelOff(OBJ));
        }
        IEnumerator FuelOff(RectTransform OBJ)
        {
            yield return WaitForSeconds;
            OBJ.gameObject.SetActive(false);
            FuelPool.Enqueue(OBJ.gameObject);
            Fuel.Remove(OBJ);
        }
        public void SStar(RectTransform OBJ)
        {
            AudioSource audio = OBJ.transform.GetComponent<AudioSource>();
            audio.volume = player.SoundSetting.EffectSoundSlider.value / 4f;
            audio.Play();
            player.Score += 25;
            OBJ.transform.position = new Vector3(1000, 1000, 0);
            StartCoroutine(SStarOFF(OBJ));
        }
        IEnumerator SStarOFF(RectTransform OBJ)
        {
            yield return WaitForSeconds;
            OBJ.gameObject.SetActive(false);
            StarPool["Small"].Enqueue(OBJ.gameObject);
            S.Remove(OBJ);
            if (Random.Range(0, 10) == 0)
                SummonFuel();
        }
        public void MStar(RectTransform OBJ)
        {
            AudioSource audio = OBJ.transform.GetComponent<AudioSource>();
            audio.volume = player.SoundSetting.EffectSoundSlider.value / 4f;
            audio.Play();
            player.Score += 15;
            OBJ.transform.position = new Vector3(1000, 1000, 0);
            StartCoroutine(MStarOFF(OBJ));
        }
        IEnumerator MStarOFF(RectTransform OBJ)
        {
            yield return WaitForSeconds;
            OBJ.gameObject.SetActive(false);
            StarPool["Medium"].Enqueue(OBJ.gameObject);
            M.Remove(OBJ);
            if (Random.Range(0, 50) == 0)
                SummonFuel();
        }
        public void LStar(RectTransform OBJ)
        {
            AudioSource audio = OBJ.GetComponent<AudioSource>();
            audio.volume = player.SoundSetting.EffectSoundSlider.value / 4f;
            audio.Play();
            player.Score += 5;
            OBJ.transform.position = new Vector3(1000, 1000, 0);
            StartCoroutine(LStarOFF(OBJ));
        }
        IEnumerator LStarOFF(RectTransform OBJ)
        {
            yield return WaitForSeconds;
            OBJ.gameObject.SetActive(false);
            StarPool["Large"].Enqueue(OBJ.gameObject);
            L.Remove(OBJ);
            if (Random.Range(0, 100) == 0)
                SummonFuel();
        }
    }
}
