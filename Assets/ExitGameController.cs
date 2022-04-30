using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameController : MonoBehaviour
{
    	public void QuitGame()
	{
		Application.Quit();
		Debug.Log("Quit complete!");
	}
}
