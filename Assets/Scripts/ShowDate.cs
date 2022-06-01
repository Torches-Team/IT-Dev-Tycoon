using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDate : MonoBehaviour
{
	public static ShowDate Instance;
	public GameObject dateText;
	
    void Awake()
    {
		if(Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
    }
 
    void Update()
    {
		SetText();
    }
	
	void SetText()
	{
		dateText.GetComponent<TMPro.TextMeshProUGUI>().text = "Н: " + GlobalController.Instance.week + " М: " + GlobalController.Instance.month + " Г: " + GlobalController.Instance.year;
	}
}
