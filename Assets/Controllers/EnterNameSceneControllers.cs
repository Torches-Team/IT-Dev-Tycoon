using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EnterNameSceneControllers : MonoBehaviour
{
    private string sceneToOpen = "MainScene";
	public TMP_InputField InputProductName;

    public void OpenScene()
    {
		GlobalController.Instance.companyName = InputProductName.text;
		SceneManager.LoadScene(sceneToOpen);
    }
}
