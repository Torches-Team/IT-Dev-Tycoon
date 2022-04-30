using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InvokeNewScene : MonoBehaviour
{
	[SerializeField] private string sceneToInvoke = "MenuScene";

	public void InvokeNewSceneS()
	{
		SceneManager.LoadScene(sceneToInvoke);
	}
}
