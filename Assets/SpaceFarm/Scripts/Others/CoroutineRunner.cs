using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceFarm
{
    public class CoroutineRunner : Singleton<CoroutineRunner>
    {
        public void RunCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}
