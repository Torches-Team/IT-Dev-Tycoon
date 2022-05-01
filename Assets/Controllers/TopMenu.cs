using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopMenu : MonoBehaviour
{
	[SerializeField] private string menuScene = "MenuScene";

    void Start()
    {
        
    }

    void Update()
    {
        
    }
	
	public void BackToMenu()
	{
		SceneManager.LoadScene(menuScene);
	}
}
