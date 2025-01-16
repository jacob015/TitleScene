using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Temp
{
    public class UI : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;
        public void SceneLoad(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            Time.timeScale = 1.0f;
        }
        public void OBJOnOff(GameObject OBJ)
        {
            OBJ.SetActive(!OBJ.activeSelf);
        }
        public void musicPlay()
        {
            audioSource.Play();
        }
    }
}
