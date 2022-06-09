using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSceneControllers : MonoBehaviour
{
    private string sceneToOpen = "WelcomeScene";
	public Button ContinueButton;

	public void Start()
	{
		if(!GlobalController.Instance.gameExist) ContinueButton.interactable = false;
	}

    public void OpenScene()
    {
		SceneManager.LoadScene(sceneToOpen);
    }
	
	public void OpenMainScene()
	{
		SceneManager.LoadScene("MainScene");
	}

    public void QuitGame()
    {
		Application.Quit();
		Debug.Log("Quit complete!");
    }
}
