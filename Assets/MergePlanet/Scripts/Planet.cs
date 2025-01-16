using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetMerge
{
    public class Planet : MonoBehaviour
    {
        public GameObject ScoreTextObject;
        public ParticleSystem Effect;
        public PlanetDB.PlanetInfo PlanetData;
        public bool isMarge;
        Rigidbody rigid;
        SphereCollider sphere;
        public float DeadTime;
        void Start()
        {
            rigid = GetComponent<Rigidbody>();
            sphere = GetComponent<SphereCollider>();
            changedPlanet();
        }
        private void OnEnable()
        {

        }
        private void OnCollisionStay(Collision collision)
        {
            if(collision.gameObject.CompareTag("Planet"))
            {
                Planet other = collision.gameObject.GetComponent<Planet>();
                if (other.PlanetData.Level == PlanetData.Level && PlanetData.Level < 10 && !isMarge && !other.isMarge)
                {
                    if(PlanetData.Level == 9)
                    {
                        GameManager.instance.Score += PlanetData.score;
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        float meX = transform.position.x;
                        float meY = transform.position.y;
                        float otherX = other.transform.position.x;
                        float otherY = other.transform.position.y;
                        if(meY < otherY || (meY == otherY && meX > otherX))
                        {
                            other.Hide(transform.position);
                            LevelUP();
                        }
                    }
                }
            }
        }
        public void Hide(Vector3 targetPos)
        {
            isMarge = true;

            rigid.isKinematic = true;
            sphere.enabled = false;

            StartCoroutine(HideRoutine(targetPos));
        }
        public IEnumerator HideRoutine(Vector3 targetPos)
        {
            int framecount = 0;
            while(framecount < 20)
            {
                framecount++;
                transform.position = Vector3.Lerp(transform.position, targetPos, 0.5f);
                yield return null;
            }
            isMarge = false;
            gameObject.SetActive(false);
        }
        public void LevelUP()
        {
            isMarge = true;
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;

            StartCoroutine(LevelUPRoutine());
        }
        IEnumerator LevelUPRoutine()
        {
            yield return null;
            PlanetData = PlanetDB.instance.PlanetDatas[PlanetData.Level + 1];
            changedPlanet();
            PointUP();
            OnEffect();
            isMarge = false;
        }
        public void changedPlanet()
        {
            sphere.enabled = true;
            rigid.isKinematic = false;
            transform.localScale = new Vector3(PlanetData.Size, PlanetData.Size, PlanetData.Size);
            GetComponent<SpriteRenderer>().sprite = PlanetData.Image;
            //GetComponent<SpriteRenderer>().sprite = PlanetData.Image;
        }
        public void PointUP()
        {
            GameManager.instance.Score += PlanetData.score;
            GetComponent<AudioSource>().Play();
            GameObject dmgt = Instantiate(ScoreTextObject, transform.parent);
            dmgt.transform.position = transform.position + new Vector3(0, transform.localScale.x / 2 + 0.5f, 0);
            dmgt.GetComponent<ScoreText>().score = PlanetData.score;
        }
        public void OnEffect()
        {
            Effect.Play();
        }
        void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("DeadLine"))
            {
                DeadTime += Time.deltaTime;
                gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, DeadTime / 3);
                if(DeadTime >= 3) 
                {
                    GameManager.instance.isOver = true;
                }
                if(DeadTime >= 4)
                {
                    StartCoroutine(GameManager.instance.GameOver());
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("DeadLine"))
            {
                DeadTime = 0;
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

}
