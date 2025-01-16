using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PangPangPang
{
    public class MissEffect : MonoBehaviour
    {
        private void Update()
        {
            transform.Translate(0, 0.02f, 0);
        }

        private void DestroyEffect()
        {
            Destroy(gameObject);
        }
    }
}
