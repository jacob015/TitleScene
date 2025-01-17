using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Anibel
{
    public class SceneChanger : MonoBehaviour
    {
        public void StartButton()
        {
            SceneManager.LoadScene("Anibel Main");
        }
    }
}