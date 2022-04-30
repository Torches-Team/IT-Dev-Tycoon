using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneControllers : MonoBehaviour
{
    [SerializeField] private string sceneToOpen = "MainScene";

    void Start()
    {
        
    }

    void Update()
    {
        
    }

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
