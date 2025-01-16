using UnityEngine;

namespace TouchUp
{
   public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
   {
      private static T _instance;

      public static T Instance => _instance;

      private void Awake()
      {
         /*if (Instance != null)
         {
            Destroy(gameObject);
            return;
         }*/

         _instance = GetComponent<T>();
         //DontDestroyOnLoad(gameObject);
         OnAwake();
      }

      protected virtual void OnAwake()
      {
      }
   }
}