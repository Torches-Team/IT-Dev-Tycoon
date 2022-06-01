using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowScore : MonoBehaviour
{
    public static ShowScore Instance;
	public GameObject moneyScoreText;
	public GameObject experienceScoreText;
	
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
		moneyScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "$" + GlobalController.Instance.moneyScore;
		experienceScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = GlobalController.Instance.experienceScore + " ОО";
	}
}
