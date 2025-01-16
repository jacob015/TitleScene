using UnityEngine;

namespace Anibel
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance => _instance;

        private void Awake()
        {
            _instance = GetComponent<T>();
            OnAwake();
        }

        protected virtual void OnAwake()
        {
        }
    }
}