using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ColorMatch
{
    public class Laser : MonoBehaviour
    {
        public float Speed;
        // Start is called before the first frame update
        void Start()
        {
            Speed = 3;
        }

        // Update is called once per frame
        void Update()
        {
            transform.localPosition += Vector3.up * Time.deltaTime * Speed;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Meteorite"))
            {
                gameObject.SetActive(false);
                ObjectPool.instance.objectPoolList[1].Enqueue(gameObject);
            }
        }
    }
}