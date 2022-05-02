using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeSecondScript : MonoBehaviour
{
	public GameObject PanelToClose;
    public GameObject PanelToOpen;

	void Update()
	{
		if(Input.GetKeyUp(KeyCode.Escape) && PanelToClose.activeSelf)
		{
			PanelToClose.SetActive(false);
			PanelToOpen.SetActive(true);
		}
	}
}
