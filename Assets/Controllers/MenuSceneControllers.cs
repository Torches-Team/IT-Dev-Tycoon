using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneControllers : MonoBehaviour
{
    private string sceneToOpen = "WelcomeScene";

    public void OpenScene()
    {
		SceneManager.LoadScene(sceneToOpen);
    }

    public void QuitGame()
    {
		Application.Quit();
		Debug.Log("Quit complete!");
    }
}
