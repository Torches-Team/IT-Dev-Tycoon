using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeScript : MonoBehaviour
{
    public GameObject PanelToClose;
	
	void Update()
	{
		if(Input.GetKeyUp(KeyCode.Escape) && PanelToClose.activeSelf)
		{
			PanelToClose.SetActive(false);
		}
	}
}
