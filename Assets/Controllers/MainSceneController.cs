using System.Collections;
using System.Collections.Generic;
using System;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
	public float secPerDays = 0.5f;
	
	private string date;

	public GameObject DateText;
	public GameObject MoneyScoreText;
	public GameObject ContextMenu;
	
	public static int moneyScore = 50000;
	public static int year = 1;
	public static int month = 1;
	public static int week = 1;
	public static int day = 1;
	
	public const int MAXDAY = 8;
	public const int MAXWEEK = 5;
	public const int MAXMONTH = 13;
	
	float timer = 0;
	
	
    void Start()
    {
		MoneyScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "Счёт: " + moneyScore;
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Mouse1))
		{
			bool flag = ContextMenu.activeSelf;
			ContextMenu.SetActive(!flag);
		}
		
		if(timer >= secPerDays)
		{
			day++;
			if(day >= MAXDAY)
			{
				week++;
				if(week >= MAXWEEK)
				{
					month++;
					if(month >= MAXMONTH)
					{
						year++;
						
						month = 1;
					}
					
					week = 1;
				}
				
				day = 1;
			}
			
			SetTimeDateString();
			
			timer = 0;
		}
		else
		{
			timer += Time.deltaTime;
		}
    }
	
	void SetTimeDateString()
	{
		DateText.GetComponent<TMPro.TextMeshProUGUI>().text = "Н: "+week+" М: "+month+" Г: "+year;
	}	
}
