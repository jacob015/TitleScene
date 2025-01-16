using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
namespace ColorMatch
{
    public class Meteorite : MonoBehaviour
    {
        bool isboom;
        public float Speed;
        public MeteoriteColor color;
        int colornum;
        int modnum;
        SpriteRenderer Spriterenderer;
        Spawner spawner;
        GameController gameController;
        public bool Bigger;
        public bool BiggerSpawn;
        public AudioSource boom;
        // Start is called before the first frame update
        private void Awake()
        {
            gameController = FindObjectOfType<GameController>();
            Spriterenderer = GetComponent<SpriteRenderer>();
            spawner = FindObjectOfType<Spawner>();
        }
        private void OnEnable()
        {
            GetComponent<SphereCollider>().enabled = true;
            transform.localScale = Bigger ? new Vector3(0.14f, 0.14f, 0.3f) : new Vector3(0.08f, 0.08f, 0.16f);
            Spriterenderer = GetComponent<SpriteRenderer>();
            modnum = gameController.GameHardAble ? 5 : 3;
            colornum = Random.Range(0, modnum);
            color = (MeteoriteColor)colornum;
            if (BiggerSpawn)
            {
                Debug.Log("Da");
                gameController.color.Insert(0, color);
            }
            else
                gameController.color.Add(color);
            transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = color.ToString();
            transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = color.ToString();
            MeteoriteSprite();
        }
        private void OnDisable()
        {
            Bigger = BiggerSpawn = false;
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            if (!isboom)
                transform.localPosition += Vector3.down * Time.deltaTime * Speed;
        }
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("DamageLine"))
            {
                gameController.color.RemoveAt(0);
                gameController.NextLine.RemoveAt(0);
                gameController.Damage(1);
                StartCoroutine(Boom());
            }
            if (collision.CompareTag("Laser"))
            {
                StartCoroutine(Boom());
                if (Bigger)
                {
                    GameObject obj = ObjectPool.instance.objectPoolList[0].Dequeue();
                    obj.transform.localPosition = transform.position;
                    obj.GetComponent<Meteorite>().BiggerSpawn = true;
                    obj.SetActive(true);
                    gameController.Score += 2;
                }
                else
                {
                    gameController.Score += 1;
                }
            }
        }
        IEnumerator Boom()
        {
            isboom = true;
            boom.Play();
            GetComponent<SphereCollider>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<Animator>().SetBool("IsBoom", true);
            yield return new WaitForSeconds(0.5f);
            isboom = false;
            GetComponent<Animator>().SetBool("IsBoom", false);
            transform.GetChild(0).gameObject.SetActive(true);
            MeteoriteSprite();
            ObjectPool.instance.objectPoolList[0].Enqueue(gameObject);
            gameObject.SetActive(false);
        }
        void MeteoriteSprite()
        {
            int ran = Random.Range(0, 3);
            Debug.Log(ran);
            switch (ran)
            {
                case 0:
                    ran = Random.Range(0, modnum);
                    Spriterenderer.sprite = spawner.MeteoriteSprite1[ran];
                    break;
                case 1:
                    ran = Random.Range(0, modnum);
                    Spriterenderer.sprite = spawner.MeteoriteSprite2[ran];
                    break;
                case 2:
                    ran = Random.Range(0, modnum);
                    Spriterenderer.sprite = spawner.MeteoriteSprite3[ran];
                    break;
            }
        }
    }
}