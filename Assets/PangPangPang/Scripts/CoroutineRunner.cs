using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PangPangPang
{
    public class CoroutineRunner : Singleton<CoroutineRunner>
    {
        public void RunCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}
