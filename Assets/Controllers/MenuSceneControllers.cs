using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSceneControllers : MonoBehaviour
{
    private string sceneToOpen = "WelcomeScene";
	public Button ContinueButton;
	public Toggle EducationToggle;

	public void Start()
	{
		if(!GlobalController.Instance.gameExist) ContinueButton.interactable = false;
		EducationToggle.isOn = GlobalController.Instance.education;	
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
	
	public void ChangeEducationState()
	{
		GlobalController.Instance.education = EducationToggle.isOn;
		GlobalController.Instance.eduWelcome = EducationToggle.isOn;
		GlobalController.Instance.eduContext = EducationToggle.isOn;
		GlobalController.Instance.eduCreation = EducationToggle.isOn;
		GlobalController.Instance.eduTheme = EducationToggle.isOn;
		GlobalController.Instance.eduTechChoose = EducationToggle.isOn;
		GlobalController.Instance.eduRelease = EducationToggle.isOn;
		GlobalController.Instance.eduBank = EducationToggle.isOn;
		GlobalController.Instance.eduAnalytics = EducationToggle.isOn;
	}
}
