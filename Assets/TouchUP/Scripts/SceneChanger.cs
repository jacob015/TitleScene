using UnityEngine;
using UnityEngine.SceneManagement;

namespace TouchUp
{
    public class SceneChanger : MonoBehaviour
    {
        public void ChangeScene()
        {
            SceneManager.LoadScene("TouchUP/Scenes/Main");
        }
    }
}