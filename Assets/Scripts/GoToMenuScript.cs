using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenuScript : MonoBehaviour
{
    public void OpenScene()
	{
		SceneManager.LoadScene("MenuScene");
	}
}
