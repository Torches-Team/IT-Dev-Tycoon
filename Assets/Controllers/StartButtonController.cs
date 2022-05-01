using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartButtonController : MonoBehaviour
{
	public GameObject ProductNameText;
	public GameObject InputProductName;

    public void SetProductName()
	{
		ProductNameText.GetComponent<TMPro.TextMeshProUGUI>().text = InputProductName.GetComponent<TMPro.TextMeshProUGUI>().text;
	}
}
