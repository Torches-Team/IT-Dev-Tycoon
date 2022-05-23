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
	public GameObject SpecializationPanel; //Панель специализации
	public GameObject BottomPanelName; //Название компании на нижней панели
	public GameObject QuestionPanel; //Панель вопроса
	public GameObject QuestionTextPanel; //Текст вопроса
	public GameObject AnswerText1Panel; //Текст ответа 1
	public GameObject AnswerText2Panel; //Текст ответа 2
	public GameObject Release; //Панель выпуска продукта
	public GameObject WinGamePanel; //Панель победы
	public GameObject LoseGamePanel; //Панель поражения
	public GameObject InputProductName; //Ввод названия продукта
	public GameObject DarkBackground; //Затемнение фона
	public GameObject ProjectSizeText;
	public GameObject AgeText;
	public GameObject GenderText;
	
	public List<GameObject> BottomIcons;
	public List<Button> AnswerButtons;
	public GameObject[] ProjectSizeButtons;
	public GameObject[] SpecializationButtons;
	public GameObject[] AgeAudienceButtons;
	public GameObject[] GenderAudienceButtons;
	public GameObject[] PropertyRightButtons;
	public GameObject[] Cells;
	public GameObject[] Grades;
	
	//public PropertyRight propertyRight;
	//public Specialization specialization;

	//Общие переменные
	float secPerDay = 0.2f; //Число реальных секунд приходящихся на один игровой день

	const int MaxDay = 8; //Ограничитель счётчика дней
	const int MaxWeek = 5; //Ограничитель счётчика недель
	const int MaxMonth = 13; //Ограничитель счётчика месяцев

	static int winningScore = 200000;
	static int losingScore = -50000;
	
	//Переменные продукта
	static int creationCost; //Стоимость создания нового продукта
	static int questionsCount; //Кол-во вопросов задаваемых при создании продукта
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
	int productCost;
	int askedQuestionCount = 0;
	double profitRatio;
	double profit1;
	double profit2;
	
	public Sprite Icon;
	public Sprite Plus;
	public Sprite Minus;
	public Sprite Children;
	public Sprite EveryAge;
	public Sprite Adult;
	public Sprite Men;
	public Sprite EveryGender;
	public Sprite Women;

	void Start()
	{
		SetDate();
		SetScore(0);
		AnswerButtons[0].onClick.AddListener(() => ReadAnswer(0));
		AnswerButtons[1].onClick.AddListener(() => ReadAnswer(1));

		
		//script = GetComponent<StatisticController>();
		//script.AddNewProduct();
		
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
		else 
		{
			AgeAudienceCheck();
			GenderAudienceCheck();
			ProjectSizeCheck();
			CheckOnDisableButton();
			DarkBackground.SetActive(true);
		}
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
		bool isSpecializationActive = SpecializationPanel.activeSelf;
		bool isQuestionActive = QuestionPanel.activeSelf;
		bool isReleaseActive = Release.activeSelf;
		bool isWinGameActive = WinGamePanel.activeSelf;
		bool isLoseGameActive = LoseGamePanel.activeSelf;

		return isCreationActive || isTargetAudienceActive || isSpecializationActive || isQuestionActive || isReleaseActive || isWinGameActive || isLoseGameActive;
	}

	void SetDate()
	{
		Date.GetComponent<TMPro.TextMeshProUGUI>().text = "Дата: Н: " + week + " М: " + month + " Г: " + year;
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
		
		if (moneyScore >= 100000) Score.GetComponent<TMPro.TextMeshProUGUI>().text = moneyScore / 1000 + "." + moneyScore % 1000 + "$";
		else Score.GetComponent<TMPro.TextMeshProUGUI>().text = moneyScore + "$";
	}
	
	void IOCheck()
	{
		if (Input.GetKeyUp(KeyCode.Mouse1) && !CheckOnStopTime())
		{
			ContextMenu.transform.position = Input.mousePosition;
			ContextMenu.SetActive(!ContextMenu.activeSelf);
		}
	}	
	
	public void CreateNewProduct()
	{
		askNewQuestionFlag = true;
		
		productName = InputProductName.GetComponent<TMP_InputField>().text;
		BottomPanelName.GetComponent<TextMeshProUGUI>().text = productName;
		ResetBottomPanel();
		
		ProjectSize projectSize = ProjectSizeCheck();
		Specialization specialization = SpecializationCheck();
		AgeAudience ageAudience = AgeAudienceCheck();
		GenderAudience genderAudience = GenderAudienceCheck();
		PropertyRight propertyRight = PropertyRightCheck();
		
		SetScore(-productCost);
		Product product = new Product(productName,
									  1,
									  productCost,
									  projectSize,
									  specialization,
									  ageAudience,
									  genderAudience,
									  propertyRight);
	}
	
	ProjectSize ProjectSizeCheck()
	{
		if (ProjectSizeButtons[0].GetComponent<Toggle>().isOn)
		{
			HideGrades();
			ProjectSizeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Стоимость разработки: 25К";
			creationCost = 25000;
			questionsCount = 3;
			return ProjectSize.MINOR;
		}
		else if (ProjectSizeButtons[1].GetComponent<Toggle>().isOn)
		{
			ProjectSizeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Стоимость разработки: 200К";
			creationCost = 200000;
			questionNumber = 5;
			return ProjectSize.MAJOR;
		}
		else return ProjectSize.MINOR;
	}
	
	Specialization SpecializationCheck()
	{
		if (SpecializationButtons[0].GetComponent<Toggle>().isOn)
		{
			return Specialization.WEBSITE;
		}
		else if (ProjectSizeButtons[1].GetComponent<Toggle>().isOn)
		{
			return Specialization.GAME;
		}
		else return Specialization.WEBSITE;
	}
	
	AgeAudience AgeAudienceCheck()
	{
		if (AgeAudienceButtons[0].GetComponent<Toggle>().isOn)
		{
			AgeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Возраст: Дети";
			BottomIcons[1].GetComponent<Image>().sprite = Children;
			return AgeAudience.CHILDREN;
		}
		else if (AgeAudienceButtons[1].GetComponent<Toggle>().isOn)
		{
			AgeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Возраст: Все";
			BottomIcons[1].GetComponent<Image>().sprite = EveryAge;
			return AgeAudience.EVERYONE;
		}
		else if (AgeAudienceButtons[2].GetComponent<Toggle>().isOn)
		{
			AgeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Возраст: Взрослые";
			BottomIcons[1].GetComponent<Image>().sprite = Adult;
			return AgeAudience.ADULT;
		}
		else return AgeAudience.EVERYONE;
	}
	
	GenderAudience GenderAudienceCheck()
	{
		if (GenderAudienceButtons[0].GetComponent<Toggle>().isOn)
		{
			GenderText.GetComponent<TMPro.TextMeshProUGUI>().text = "Пол: Мужчины";
			BottomIcons[0].GetComponent<Image>().sprite = Men;
			return GenderAudience.MALE;
		}
		else if (GenderAudienceButtons[1].GetComponent<Toggle>().isOn)
		{
			GenderText.GetComponent<TMPro.TextMeshProUGUI>().text = "Пол: Все";
			BottomIcons[0].GetComponent<Image>().sprite = EveryGender;
			return GenderAudience.EVERYONE;
		}
		else if (GenderAudienceButtons[2].GetComponent<Toggle>().isOn)
		{
			GenderText.GetComponent<TMPro.TextMeshProUGUI>().text = "Пол: Женщины";
			BottomIcons[0].GetComponent<Image>().sprite = Women;
			return GenderAudience.FEMALE;
		}
		else return GenderAudience.EVERYONE;
	}
	
	PropertyRight PropertyRightCheck()
	{
		if (PropertyRightButtons[0].GetComponent<Toggle>().isOn)
		{
			return PropertyRight.SOLD_TO_ANOTHER_COMPANY;
		}
		else if (PropertyRightButtons[1].GetComponent<Toggle>().isOn)
		{
			return PropertyRight.BELONGS_TO_OUR_COMPANY;
		}
		else return PropertyRight.SOLD_TO_ANOTHER_COMPANY;
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

	public void ReadAnswer(int option)
	{	
		if (option == 0)
		{
			answers.Add(new Answer(QuestionTextPanel.GetComponent<TMPro.TextMeshProUGUI>().text,
				AnswerText1Panel.GetComponent<TMPro.TextMeshProUGUI>().text, profit1));
				Debug.Log("First Button");
		}
		else if (option == 1)
		{
			answers.Add(new Answer(QuestionTextPanel.GetComponent<TMPro.TextMeshProUGUI>().text,
				AnswerText2Panel.GetComponent<TMPro.TextMeshProUGUI>().text, profit2));
				Debug.Log("Second Button");
		}
		Debug.Log(answers.Count);
		MarkDoneQuestion(askedQuestionCount - 1);
		
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
		if (PropertyRightButtons[0].GetComponent<Toggle>().isOn)
		{
			//products.Add(new Product(productName, profitRatio, creationCost, PropertyRight.SOLD_TO_ANOTHER_COMPANY));
			income = (int)(creationCost * profitRatio);
		}
		else if (PropertyRightButtons[1].GetComponent<Toggle>().isOn)
		{
			//roducts.Add(new Product(productName, profitRatio, creationCost, PropertyRight.BELONGS_TO_OUR_COMPANY));
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
	
	void CheckOnDisableButton()
	{
		if(moneyScore < 150000) ProjectSizeButtons[1].GetComponent<Toggle>().interactable = false;
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
		if(answers[questionNumber].AnswerProfitRatio < 1) Grades[questionNumber].GetComponent<Image>().sprite = Minus;
		else if(answers[questionNumber].AnswerProfitRatio >= 1) Grades[questionNumber].GetComponent<Image>().sprite = Plus;
	}
	
	void HideGrades()
	{
		Cells[3].GetComponent<Image>().color = new Color32(100, 100, 100, 255);
		Cells[4].GetComponent<Image>().color = new Color32(100, 100, 100, 255);
		Grades[3].GetComponent<Image>().color = new Color32(100, 100, 100, 255);
		Grades[4].GetComponent<Image>().color = new Color32(100, 100, 100, 255);
	}
	
	void ResetBottomPanel()
	{
		foreach (var cell in Cells)
		{
			cell.GetComponent<Image>().color = new Color32(204, 204, 204, 255);
		}
		foreach (var grade in Grades)
		{
			grade.GetComponent<Image>().sprite = Icon;
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
	public string productName;
	public double productProfitRatio;
	public int productCost;
	
	public ProjectSize projectSize;
	public Specialization specialization;
	public AgeAudience ageAudience;
	public GenderAudience genderAudience;
	public PropertyRight propertyRight;

	public Product(string productName, 
				   double productProfitRatio, 
				   int productCost, 
				   ProjectSize projectSize, 
				   Specialization specialization, 
				   AgeAudience ageAudience, 
				   GenderAudience genderAudience, 
				   PropertyRight propertyRight)
	{
		this.productName = productName;
		this.productProfitRatio = productProfitRatio;
		this.productCost = productCost;
		
		this.projectSize = projectSize;
		this.specialization = specialization;
		this.ageAudience = ageAudience;
		this.genderAudience = genderAudience;
		this.propertyRight = propertyRight;
	}
}

public enum ProjectSize
{
	MINOR,
	MAJOR
}

public enum Specialization
{
	GAME,
	WEBSITE
}

public enum AgeAudience
{
	CHILDREN,
	EVERYONE,
	ADULT
}

public enum GenderAudience
{
	MALE,
	EVERYONE,
	FEMALE
}

public enum PropertyRight
{
	SOLD_TO_ANOTHER_COMPANY,
	BELONGS_TO_OUR_COMPANY
}

