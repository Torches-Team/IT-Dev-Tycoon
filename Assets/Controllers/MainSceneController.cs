using System.Collections;
using System.Collections.Generic;
using System;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
	public List<Question> questions;
	public List<Answer> answers;
	public List<Product> products;

	public GameObject Date; //Объект отображения внутриигрового времени
	public GameObject Score; //Объект отображения денежного счёта игрока
	public GameObject ContextMenu; //Объект контекстной менюшки, открываемый при нажатии ПКМ 
	public GameObject Creation; //Объект панели создания
	public GameObject TargetAudience; //Объект панели целевой аудитории
	public GameObject Specialization; //Объект панели специализации
	public GameObject QuestionPanel; //Объект панели вопроса
	public GameObject QuestionTextPanel;
	public GameObject AnswerText1Panel;
	public GameObject AnswerText2Panel;
	public GameObject AnswerButton1;
	public GameObject AnswerButton2;
	public GameObject PropertyRightButton1;
	public GameObject PropertyRightButton2;
	public GameObject Release;
	public GameObject WinGamePanel;
	public GameObject LoseGamePanel;

	public float secPerDays = 0.5f; //Число реальных секунд приходящихся на один игровой день

	const int MaxDay = 8; //Ограничитель счётчика дней
	const int MaxWeek = 5; //Ограничитель счётчика недель
	const int MaxMonth = 13; //Ограничитель счётчика месяцев

	static int year = 1;
	static int month = 1;
	static int week = 1;
	static int day = 1;
	float timer = 0;

	public string productName = "NameCop";
	public bool askNewQuestionFlag = false;
	public bool releaseFlag = false;
	public bool gameFinishedFlag = false;
	public int askedQuestionNumber = 0;
	public double profitRatio;
	public static int moneyScore = 50000; //денежный счёт игрока
	public static int monthlyExpenses = 4000; //месячные затраты
	public static int experienceScore = 0; //счёт очков опыта игрока

	void Start()
	{
		SetScore();
		questions.Add(new Question("Что бы вы сделали?", "Поел", "Поспал", 0.7f, 1.3f));
		questions.Add(new Question("Что бы вы съели?", "Пельмени", "Вареники", 1.4f, 0.9f));
		questions.Add(new Question("Где бы вы спали?", "Диван", "Кровать", 1, 0.1f));
		questions.Add(new Question("Что бы предпочитаете в качестве соуса?", "Майонез", "Кетчуп", 0.5f, 1.5f));
	}

	void Update()
	{
		Clock();
		InputOutputCheck();
	}

	void Clock()
	{
		if (!CheckOnStopTime())
		{
			if (timer >= secPerDays)
			{
				day++;
				if (day >= MaxDay)
				{
					if (releaseFlag)
					{
						Release.SetActive(true);
						releaseFlag = false;
						askNewQuestionFlag = false;
					}

					if (askNewQuestionFlag)
					{
						AskQuestion();
						askNewQuestionFlag = false;
					}

					week++;
					if (week >= MaxWeek)
					{
						month++;
						moneyScore -= monthlyExpenses;
						SetScore();
						if (month >= MaxMonth)
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
		bool isQuestionActive = QuestionPanel.activeSelf;
		bool isWinGameActive = WinGamePanel.activeSelf;
		bool isLoseGameActive = LoseGamePanel.activeSelf;

		return isCreationActive || isTargetAudienceActive || isSpecializationActive || isQuestionActive || isWinGameActive || isLoseGameActive;
	}

	void SetDate()
	{
		Date.GetComponent<TMPro.TextMeshProUGUI>().text = "Дата: Н: " + week + " М: " + month + " Г: " + year;
	}

	void SetScore()
	{
		Score.GetComponent<TMPro.TextMeshProUGUI>().text = "Счёт: " + moneyScore + "$";
		Score.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0, 1, 0, 1);
		if (moneyScore <= 0)
		{
			Score.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 0, 0, 1);
		}

		if (moneyScore >= 200000 && !gameFinishedFlag)
		{
			gameFinishedFlag = true;
			WinGame();
		}
		
		if (moneyScore <= -50000 && !gameFinishedFlag)
		{
			gameFinishedFlag = true;
			LoseGame();
		}
	}

	public void SetAskNewQuestion()
	{
		askNewQuestionFlag = true;
	}

	public void StartNewGame()
	{
		questions = new List<Question>();
		answers = new List<Answer>();
		products = new List<Product>();
		year = 1;
		month = 1;
		week = 1;
		day = 1;
		timer = 0;
		askedQuestionNumber = 0;
		moneyScore = 50000;
		monthlyExpenses = 4000;
		experienceScore = 0;
	}

	void InputOutputCheck()
	{
		if (Input.GetKeyUp(KeyCode.Mouse1) && !CheckOnStopTime())
		{
			bool flag = ContextMenu.activeSelf;
			ContextMenu.SetActive(!flag);
			ContextMenu.transform.position = Input.mousePosition;
		}
	}

	public void AskQuestion()
	{
		askedQuestionNumber++;
		var randomQuestion = questions[new System.Random().Next(0, questions.Count)];

		QuestionTextPanel.GetComponent<TMPro.TextMeshProUGUI>().text = randomQuestion.QuestionText;
		AnswerText1Panel.GetComponent<TMPro.TextMeshProUGUI>().text = randomQuestion.AnswerText1;
		AnswerText2Panel.GetComponent<TMPro.TextMeshProUGUI>().text = randomQuestion.AnswerText2;

		QuestionPanel.SetActive(true);
	}

	public void ReadAnswer()
	{
		if (AnswerButton1.GetComponent<Toggle>().isOn)
		{
			answers.Add(new Answer(QuestionTextPanel.GetComponent<TMPro.TextMeshProUGUI>().text,
				AnswerText1Panel.GetComponent<TMPro.TextMeshProUGUI>().text, 1.2));
		}
		else if (AnswerButton2.GetComponent<Toggle>().isOn)
		{
			answers.Add(new Answer(QuestionTextPanel.GetComponent<TMPro.TextMeshProUGUI>().text,
				AnswerText2Panel.GetComponent<TMPro.TextMeshProUGUI>().text, 0.8));
		}

		if (askedQuestionNumber >= 5)
		{
			askNewQuestionFlag = false;
			releaseFlag = true;
			askedQuestionNumber = 0;
		}
		else
		{
			askNewQuestionFlag = true;
		}

		QuestionPanel.SetActive(false);
	}

	public void ReleaseProduct()
	{
		profitRatio = GetProfitRatio();
		if (PropertyRightButton1.activeSelf)
		{
			products.Add(new Product(productName, profitRatio, 10000, PropertyRights.SOLD_TO_ANOTHER_COMPANY));
			moneyScore += (int) (25000 * profitRatio);
			SetScore();
		}
		else if (PropertyRightButton2.activeSelf)
		{
			products.Add(new Product(productName, profitRatio, 10000, PropertyRights.BELONGS_TO_OUR_COMPANY));
			moneyScore += (int) (5000 * profitRatio);
			SetScore();
		}
	}

	double GetProfitRatio()
	{
		double a = 1;
		foreach (var answer in answers)
		{
			a *= answer.AnswerProfitRatio;
		}

		answers.Clear();
		return a;
	}

	public void CreateNewProduct()
	{
		SetAskNewQuestion();
		moneyScore -= 25000;
		SetScore();
	}

	public void WinGame()
	{
		WinGamePanel.SetActive(true);
	}
	
	public void LoseGame()
	{
		LoseGamePanel.SetActive(true);
	}
}

public class Question
{
	public string QuestionText { get; }
	public string AnswerText1 { get; }
	public string AnswerText2 { get; }
	public double AnswerProfitRatio1 { get; }
	public double AnswerProfitRatio2 { get; }

	public Question(string QuestionText, string AnswerText1, string AnswerText2, double AnswerProfitRatio1, double AnswerProfitRatio2)
	{
		this.QuestionText = QuestionText;
		this.AnswerText1 = AnswerText1;
		this.AnswerText2 = AnswerText2;
		this.AnswerProfitRatio1 = AnswerProfitRatio1;
		this.AnswerProfitRatio2 = AnswerProfitRatio2;
	}
}

public class Answer
{
	public string QuestionText;
	public string AnswerText;
	public double AnswerProfitRatio; //Коэффицент прибыльности выбранного ответа

	public Answer(string QuestionText, string AnswerText, double AnswerProfitRatio)
	{
		this.QuestionText = QuestionText;
		this.AnswerText = AnswerText;
		this.AnswerProfitRatio = AnswerProfitRatio;
	}
}

public class Product
{
	public string ProductName;
	public double ProductProfitRatio;
	public int ProductCost;
	public PropertyRights PropertyRight;

	public Product(string ProductName, double ProductProfitRatio, int ProductCost, PropertyRights PropertyRight)
	{
		this.ProductName = ProductName;
		this.ProductProfitRatio = ProductProfitRatio;
		this.ProductCost = ProductCost;
		this.PropertyRight = PropertyRight;
	}
}

public enum PropertyRights
{
	SOLD_TO_ANOTHER_COMPANY,
	BELONGS_TO_OUR_COMPANY
}
