using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI ProductNameText;
	public TMPro.TextMeshProUGUI InputProductName;
	public Animator GUIAnimator;
	public Animator ContextPanelAnimator;
	public List<Button> buttons;
	
	void Start()
	{
		buttons[0].onClick.AddListener(() => OpenScene("MenuScene"));
		buttons[1].onClick.AddListener(() => OpenScene("TreeScene"));
		buttons[2].onClick.AddListener(() => OpenScene("StatisticScene"));
	}

    public void SetProductName()
	{
		ProductNameText.text = InputProductName.text;
	}
	
	public void OpenScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
	
	public void OpenPanel()
	{
		if(GUIAnimator != null)
		{
			var isOpen = GUIAnimator.GetBool("Open");
			
			GUIAnimator.SetBool("Open", !isOpen);
		}
	}
	
	public void ShowContextMenu()
	{
		if(ContextPanelAnimator != null)
		{
			var isShow = ContextPanelAnimator.GetBool("Show");
			
			ContextPanelAnimator.SetBool("Show", !isShow);
		}
	}
}
