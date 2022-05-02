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
	
	//public Question[] questions;

	public GameObject Date;
	public GameObject Score;
	public GameObject ContextMenu;
	public GameObject Creation; 
	public GameObject TargetAudience;
	public GameObject Specialization;
	
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
		
    }

    void Update()
    {
		Clock();
		SetScore();
	
		if(Input.GetKeyUp(KeyCode.Mouse1) && !CheckOnStopTime())
		{
			bool flag = ContextMenu.activeSelf;
			ContextMenu.SetActive(!flag);
			ContextMenu.transform.position = Input.mousePosition;
		}
    }
	
	void Clock()
	{ 
		if(!CheckOnStopTime()){
			if(timer >= secPerDays)
			{
				day++;
				if(day >= MAXDAY)
				{
					week++;
					if(week >= MAXWEEK)
					{
						month++;
						moneyScore -= 4000;
						if(month >= MAXMONTH)
						{
							year++;
							month = 1;
						}
						week = 1;
					}
					day = 1;
				}
				SetDate();
				timer = 0;
			}
			else
			{
				timer += Time.deltaTime;
			}
		}
	}
	
	bool CheckOnStopTime()
	{
		bool isCreationActive = Creation.activeSelf;
		bool isTargetAudienceActive = TargetAudience.activeSelf;
		bool isSpecializationActive = Specialization.activeSelf;

		return isCreationActive || isTargetAudienceActive || isSpecializationActive;
	}
	
	void SetDate()
	{
		Date.GetComponent<TMPro.TextMeshProUGUI>().text = "Дата: Н: "+week+" М: "+month+" Г: "+year;
	}
	
	void SetScore()
	{
		Score.GetComponent<TMPro.TextMeshProUGUI>().text = "Счёт: " + moneyScore + "$";
		/*if(moneyScore <= 0)
		{
			Score.GetComponent<TMPro.TextMeshProUGUI>().color = Color.Red;
		}*/
	}
	
	/*public class Question
	{
		public static String QuestionText { get; };
		public static String AnswerText1 { get; };
		public static String AnswerText2 { get; };
		
		public Question(String questionText, String answerText1, String answerText2){
			QuestionText = questionText;
			AnswerText1 = answerText1;
			AnswerText2 = answerText2;
		}
	}
	*/
}
