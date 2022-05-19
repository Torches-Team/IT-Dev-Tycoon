using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIController : MonoBehaviour
{
    public GameObject ProductNameText;
	public GameObject InputProductName;

    public void SetProductName()
	{
		ProductNameText.GetComponent<TMPro.TextMeshProUGUI>().text = InputProductName.GetComponent<TMPro.TextMeshProUGUI>().text;
	}
	
	public void BackToMenu()
	{
		SceneManager.LoadScene("MenuScene");
	}
	
	public void OpenStatistic()
	{
		SceneManager.LoadScene("StatisticScene");
	}
	
	public void OpenTree()
	{
		SceneManager.LoadScene("TreeScene");
	}
}
