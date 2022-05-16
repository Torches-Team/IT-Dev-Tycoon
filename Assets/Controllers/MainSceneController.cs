using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
	public List<Question> questions = new List<Question>();
	public List<Answer> answers = new List<Answer>();
	public List<Product> products = new List<Product>();

	public GameObject Date; //Объект внутриигрового времени
	public GameObject Score; //Объект денежного счёта игрока
	public GameObject ContextMenu; //Контекстное меню
	public GameObject Creation; //Панель создания продукта
	public GameObject TargetAudience; //Панель целевой аудитории
	public GameObject Specialization; //Панель специализации
	public GameObject BottomPanelName; //Название компании на нижней панели
	public GameObject QuestionPanel; //Панель вопроса
	public GameObject QuestionTextPanel; //Текст вопроса
	public GameObject AnswerText1Panel; //Текст ответа 1
	public GameObject AnswerText2Panel; //Текст ответа 2
	public GameObject AnswerButton1; //Кнопка ответа 1
	public GameObject AnswerButton2; //Кнопка ответа 2
	public GameObject PropertyRightButton1;
	public GameObject PropertyRightButton2;
	public GameObject Release; //Панель выпуска продукта
	public GameObject WinGamePanel; //Панель победы
	public GameObject LoseGamePanel; //Панель поражения
	public GameObject InputProductName; //Ввод названия продукта
	public GameObject DarkBackground; //Затемнение фона
	
	public GameObject[] Cells;
	public GameObject[] Grades;

	//Общие переменные
	float secPerDay = 0.2f; //Число реальных секунд приходящихся на один игровой день

	const int MaxDay = 8; //Ограничитель счётчика дней
	const int MaxWeek = 5; //Ограничитель счётчика недель
	const int MaxMonth = 13; //Ограничитель счётчика месяцев

	static int winningScore = 200000;
	static int losingScore = -50000;
	
	//Переменные продукта
	static int creationCost = 25000; //Стоимость создания нового продукта
	static int questionsCount = 5; //Кол-во вопросов задаваемых при создании продукта
	static int questionNumber = 0; //Номер вопроса, задаваемого в данный момент
	
	//Переменные игрока 
	static int moneyScore = 50000; //денежный счёт игрока
	static int experienceScore = 0; //счёт очков опыта игрока
	static int monthlyExpenses = 4000; //месячные затраты 
	
	//Временные переменные 
	static int year = 1;
	static int month = 1;
	static int week = 1;
	static int day = 1;
	float timer = 0;
	
	//Флаги
	bool askNewQuestionFlag = false;
	bool releaseFlag = false;
	bool gameFinishedFlag = false;
	
	//Вспомогательные переменные
	string productName;
	int askedQuestionCount = 0;
	double profitRatio;
	double profit1;
	double profit2;

	void Start()
	{
		SetDate();
		SetScore(0);
		questions.Add(new Question("Мы создаем новое меню для пользовательского интерфейса, на что стоит уделить большее внимание?", "Красота", "Фукционал", 0.85, 1.15));
		questions.Add(new Question("Для разработки продукта в коллектив требуются новые люди. Какой уровень будущих сотрудников будет приемлемым?", "Студенты", "Специалисты", 0.9, 1.4));
		questions.Add(new Question("Презентовать ли будущий продукт на выставке IT-отрасли?", "Да", "Нет", 1, 0.9));
		questions.Add(new Question("Недавно разгорелся скандал из-за утечки данных о пользователях популярного сайта доставки еды. Стоит ли нам лучше поработать над защитой персональных данных?", "Да", "Нет", 1.2, 1));
		questions.Add(new Question("Один из сотрудников компании на недавнем совещании заявил о скором увольнении из-за переезда. Стоит ли нам уже сейчас найти ему замену", "Да", "Нет", 1, 0.8));
		questions.Add(new Question("Один из сотрудников заявил о необходимости найма Scrum-менеджера для более грамотной работы команды. Стоит ли нам вложиться в это?", "Да", "Нет", 1.15, 0.9));
		questions.Add(new Question("Использовать ли новую неопробованную технологию обратной связи с разработчиками на нашем продукте?", "Да", "Нет", 0.95, 1));
		questions.Add(new Question("Уже скоро наступит дата очередного дедлайна для нашей команды, однако темп разработки говорит о нехватке времени для тестирования продукта. Стоит ли закрыть глаза на тесты и сделать работу в срок?", "Да", "Нет", 0.85, 1.2));
		questions.Add(new Question("Скоро наступит лето, большинство сотрудников на это время запланировали свои отпуска. Для успешного завершения проекта нужно либо нанять новых сотрудников на время, либо обратиться к фрилансу. Что мы сделаем?", "Новые люди", "Фриланс", 1, 0.8));
		questions.Add(new Question("В последнее время сотрудники все больше говорят о необходимости переезда в более просторный офис. Послушать ли сотрудников?", "Да", "Нет", 1.4, 0.9));
		questions.Add(new Question("Один из сотрудников жалуется наневыносимую жару в кабинете даже с открытым окном. Поставить ли кондиционеры в офисе?", "Да", "Нет", 1.2, 0.95));
		questions.Add(new Question("Все больше IT-компаний предоставляют своим подчиненным ДМС(Дополнительное Медицинское Страхование), стоит ли нам подхватить эту волну?", "Да", "Нет", 1.3, 1));
		questions.Add(new Question("Недавно PR-менеджер компании предложил прорекламировать будущий продукт, заплатив гонорар известным блогерам. Стоит ли послушать его?", "Да", "Нет", 1.1, 1));
	}

	void Update()
	{
		Clock();
		IOCheck();
	}

	void Clock()
	{
		if (!CheckOnStopTime())
		{
			DarkBackground.SetActive(false);
			if (timer >= secPerDay)
			{
				day++;
				if (day >= MaxDay)
				{
					day = 1;
					week++;
					
					if (week >= MaxWeek)
					{
						week = 1;
						month++;
						
						MonthlyEvents();
						if (month >= MaxMonth)
						{
							month = 1;
							year++;
						}
					}
					WeeklyEvents();
				}
				timer = 0;
			}
			else
			{
				timer += Time.deltaTime;
			}
		}
		else DarkBackground.SetActive(true);
	}
	
	void WeeklyEvents() //События, происходящие каждую новую неделю
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
		SetDate();
	}
	
	void MonthlyEvents() //События, происходящие каждый новый месяц
	{
		SetScore(-monthlyExpenses);
	}

	bool CheckOnStopTime()
	{
		bool isCreationActive = Creation.activeSelf;
		bool isTargetAudienceActive = TargetAudience.activeSelf;
		bool isSpecializationActive = Specialization.activeSelf;
		bool isQuestionActive = QuestionPanel.activeSelf;
		bool isReleaseActive = Release.activeSelf;
		bool isWinGameActive = WinGamePanel.activeSelf;
		bool isLoseGameActive = LoseGamePanel.activeSelf;

		return isCreationActive || isTargetAudienceActive || isSpecializationActive || isQuestionActive || isReleaseActive || isWinGameActive || isLoseGameActive;
	}

	void SetDate()
	{
		Date.GetComponent<TMPro.TextMeshProUGUI>().text = "Н: " + week + " М: " + month + " Г: " + year;
	}

	void SetScore(int deltaScore)
	{
		moneyScore += deltaScore;
		if (moneyScore <= 0) Score.GetComponent<TMPro.TextMeshProUGUI>().color = new Color32(255, 255, 255, 255); //Покрас текста в белый цвет при отрицательном балансе
		else Score.GetComponent<TMPro.TextMeshProUGUI>().color = new Color32(60, 200, 60, 255); //Покрас текста в зеленый цвет при положительном балансе

		if (moneyScore >= winningScore && !gameFinishedFlag)
		{
			gameFinishedFlag = true;
			WinGame();
		}
		
		if (moneyScore <= losingScore && !gameFinishedFlag)
		{
			gameFinishedFlag = true;
			LoseGame();
		}
		
		Score.GetComponent<TMPro.TextMeshProUGUI>().text = moneyScore + "$";
	}
	
	void IOCheck()
	{
		if (Input.GetKeyUp(KeyCode.Mouse1) && !CheckOnStopTime())
		{
			ContextMenu.transform.position = Input.mousePosition;
			ContextMenu.SetActive(!ContextMenu.activeSelf);
		}
	}

	/*public void StartNewGame()
	{
		questions = new List<Question>();
		answers = new List<Answer>();
		products = new List<Product>();
		year = 1;
		month = 1;
		week = 1;
		day = 1;
		timer = 0;
		askedQuestionCount = 0;
		moneyScore = 50000;
		monthlyExpenses = 4000;
		experienceScore = 0;
	}*/
	
	
	public void CreateNewProduct()
	{
		askNewQuestionFlag = true;
		productName = InputProductName.GetComponent<TMP_InputField>().text;
		BottomPanelName.GetComponent<TextMeshProUGUI>().text = productName;
		ResetBottomPanel();
		SetScore(-creationCost);
	}

	public void AskQuestion()
	{
		MarkCurrentQuestion(askedQuestionCount);
		askedQuestionCount++;
		
		var randomQuestion = questions[new System.Random().Next(0, questions.Count)];

		DarkBackground.SetActive(true);
		QuestionTextPanel.GetComponent<TMPro.TextMeshProUGUI>().text = randomQuestion.QuestionText;
		AnswerText1Panel.GetComponent<TMPro.TextMeshProUGUI>().text = randomQuestion.AnswerText1;
		AnswerText2Panel.GetComponent<TMPro.TextMeshProUGUI>().text = randomQuestion.AnswerText2;
		profit1 = randomQuestion.AnswerProfitRatio1;
		profit2 = randomQuestion.AnswerProfitRatio2;
		
		QuestionPanel.SetActive(true);
	}

	public void ReadAnswer()
	{
		MarkDoneQuestion(askedQuestionCount - 1);
		
		if (AnswerButton1.GetComponent<Toggle>().isOn)
		{
			answers.Add(new Answer(QuestionTextPanel.GetComponent<TMPro.TextMeshProUGUI>().text,
				AnswerText1Panel.GetComponent<TMPro.TextMeshProUGUI>().text, profit1));
		}
		else if (AnswerButton2.GetComponent<Toggle>().isOn)
		{
			answers.Add(new Answer(QuestionTextPanel.GetComponent<TMPro.TextMeshProUGUI>().text,
				AnswerText2Panel.GetComponent<TMPro.TextMeshProUGUI>().text, profit2));
		}

		if (askedQuestionCount >= questionsCount)
		{
			askNewQuestionFlag = false;
			releaseFlag = true;
			askedQuestionCount = 0;
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
		var income = 0;
		if (PropertyRightButton1.GetComponent<Toggle>().isOn)
		{
			products.Add(new Product(productName, profitRatio, creationCost, PropertyRights.SOLD_TO_ANOTHER_COMPANY));
			income = (int)(creationCost * profitRatio);
		}
		else if (PropertyRightButton2.GetComponent<Toggle>().isOn)
		{
			products.Add(new Product(productName, profitRatio, creationCost, PropertyRights.BELONGS_TO_OUR_COMPANY));
			income = (int)(creationCost * 0.2 * profitRatio);
		}
		SetScore(income);
	}

	double GetProfitRatio()
	{
		double k = 1;
		foreach (var answer in answers)
		{
			k *= answer.AnswerProfitRatio;
		}
		answers.Clear();
		return k;
	}

	void WinGame()
	{
		WinGamePanel.SetActive(true);
	}
	
	void LoseGame()
	{
		LoseGamePanel.SetActive(true);
	}
	
	void MarkCurrentQuestion(int questionNumber)
	{
		Cells[questionNumber].GetComponent<Image>().color = new Color32(154, 154, 154, 255);
	}
	
	void MarkDoneQuestion(int questionNumber)
	{
		Cells[questionNumber].GetComponent<Image>().color = new Color32(60, 200, 60, 255);
	}
	
	void ResetBottomPanel()
	{
		foreach (var cell in Cells)
		{
			cell.GetComponent<Image>().color = new Color32(204, 204, 204, 255);
		}
		questionNumber = 0;
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
