using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Anibel
{
	public class GameExit : MonoBehaviour
	{
		public void GameExitButton()
		{
			SceneManager.LoadScene("LobbyMain/Scenes/MainScene");
		}
	}
}