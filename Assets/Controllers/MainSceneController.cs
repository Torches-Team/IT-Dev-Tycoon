using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using TMPro;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
	public GameObject techPrefab;
	public GameObject firstTechPrefab;
	public Transform TechnologiesList;
	public ToggleGroup toggleGroup;
	
	public Dictionary<int, Category> gameCreationStages;
	
	public GlobalController globalController;
	public GUIController userInterface;
	public BankController bankController;
	public ShowDate showDateScript;
	
	public List<Question> generalQuestions = new List<Question>();
	public List<Question> childrenQuestions = new List<Question>();
	public List<Question> adultQuestions = new List<Question>();
	public List<Question> maleQuestions = new List<Question>();
	public List<Question> femaleQuestions = new List<Question>();
	public List<Question> askedQuestions;
	public List<Question> questionsPool;
	
	public List<Answer> answers = new List<Answer>();
	public List<Product> products;
	public List<GameObject> toggleObjects;
	public List<Technology> techonologiesList;

	public TMPro.TextMeshProUGUI Date; //Объект внутриигрового времени
	public GameObject Score; //Объект денежного счёта игрока
	public GameObject ContextMenu; //Контекстное меню
	public GameObject Creation; //Панель создания продукта
	public GameObject TargetAudience; //Панель целевой аудитории
	public GameObject SpecializationPanel; //Панель специализации
	public GameObject BottomPanelName; //Название компании на нижней панели
	public GameObject QuestionPanel; //Панель вопроса
	public GameObject TechnologyPanel;
	public GameObject QuestionTextPanel; //Текст вопроса
	public GameObject AnswerText1Panel; //Текст ответа 1
	public GameObject AnswerText2Panel; //Текст ответа 2
	public GameObject Release; //Панель выпуска продукта
	public GameObject WinGamePanel; //Панель победы
	public GameObject LoseGamePanel; //Панель поражения
	public GameObject InputProductName; //Ввод названия продукта
	public GameObject ProjectSizeText;
	public GameObject AgeText;
	public GameObject GenderText;
	public GameObject AnalyticsPanel;
	public Specialization specialization;
	
	public List<GameObject> BottomIcons;
	public List<Button> AnswerButtons;
	public List<Toggle> PlayButtonsToggle;
	public List<GameObject> DarkSpeedButtons;
	public List<GameObject> Backgrounds;
	public GameObject[] ProjectSizeButtons;
	public GameObject[] SpecializationButtons;
	public GameObject[] AgeAudienceButtons;
	public GameObject[] GenderAudienceButtons;
	public GameObject[] PropertyRightButtons;
	public GameObject[] Cells;
	public GameObject[] Grades;
	
	public Product product;

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
	static int questionNumber; //Номер вопроса, задаваемого в данный момент
	
	//Переменные игрока 
	public int moneyScore; //денежный счёт игрока
	public int experienceScore ; //счёт очков опыта игрока
	static int monthlyExpenses = 4000; //месячные затраты 
	
	//Временные переменные 
	static int day = 1;
	float timer = 0;
	int analyticsWeeks = 0;
	
	//Флаги
	bool askNewQuestionFlag = false;
	bool releaseFlag = false;
	bool gameFinishedFlag = false;
	bool created = false;
	bool isDefaultExist = false;
	
	//Вспомогательные переменные
	string productName;
	int productCost;
	int askedQuestionCount = 0;
	double profitRatio;
	double expRatio;
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
	
	void Awake()
	{
	}

	void Start()
	{	
		AnswerButtons[0].onClick.AddListener(() => ReadAnswer(0));
		AnswerButtons[1].onClick.AddListener(() => ReadAnswer(1));
		
		generalQuestions.Add(new Question("Мы создаем новое меню для пользовательского интерфейса, на что стоит уделить большее внимание?", "Красота", "Фукционал", 0.8, 1.4));
		generalQuestions.Add(new Question("Для разработки продукта в коллектив требуются новые люди. Какой уровень будущих сотрудников будет приемлемым?", "Студенты", "Специалисты", 0.75, 1.4));
		generalQuestions.Add(new Question("Презентовать ли будущий продукт на выставке IT-отрасли?", "Да", "Нет", 1, 0.9));
		generalQuestions.Add(new Question("Недавно разгорелся скандал из-за утечки данных о пользователях популярного сайта доставки еды. Стоит ли нам лучше поработать над защитой персональных данных?", "Да", "Нет", 1.4, 1));
		generalQuestions.Add(new Question("Один из сотрудников компании на недавнем совещании заявил о скором увольнении из-за переезда. Стоит ли нам уже сейчас найти ему замену", "Да", "Нет", 1.3, 0.8));
		generalQuestions.Add(new Question("Один из сотрудников заявил о необходимости найма Scrum-менеджера для более грамотной работы команды. Стоит ли нам вложиться в это?", "Да", "Нет", 1.3, 0.9));
		generalQuestions.Add(new Question("Использовать ли новую неопробованную технологию обратной связи с разработчиками на нашем продукте?", "Да", "Нет", 0.9, 1.2));
		generalQuestions.Add(new Question("Уже скоро наступит дата очередного дедлайна для нашей команды, однако темп разработки говорит о нехватке времени для тестирования продукта. Стоит ли закрыть глаза на тесты и сделать работу в срок?", "Да", "Нет", 0.85, 1.3));
		generalQuestions.Add(new Question("Скоро наступит лето, большинство сотрудников на это время запланировали свои отпуска. Для успешного завершения проекта нужно либо нанять новых сотрудников на время, либо обратиться к фрилансу. Что мы сделаем?", "Новые люди", "Фриланс", 1.5, 0.8));
		generalQuestions.Add(new Question("В последнее время сотрудники все больше говорят о необходимости переезда в более просторный офис. Послушать ли сотрудников?", "Да", "Нет", 1.4, 0.9));
		generalQuestions.Add(new Question("Один из сотрудников жалуется наневыносимую жару в кабинете даже с открытым окном. Поставить ли кондиционеры в офисе?", "Да", "Нет", 1.2, 0.95));
		generalQuestions.Add(new Question("Все больше IT-компаний предоставляют своим подчиненным ДМС(Дополнительное Медицинское Страхование), стоит ли нам подхватить эту волну?", "Да", "Нет", 1.3, 1));
		generalQuestions.Add(new Question("Недавно PR-менеджер компании предложил прорекламировать будущий продукт, заплатив гонорар известным блогерам. Стоит ли послушать его?", "Да", "Нет", 1.5, 1));
		
		childrenQuestions.Add(new Question("Для привлечения внимания детской аудитории маркетологи советуют разнообразить цветовую палитру продукта, однако это может негативно сказаться на других аспектах, стоит ли нам попробовать?", "Да", "Нет", 1.3, 0.8));
		childrenQuestions.Add(new Question("В последнем выпуске популярного журнала по психологии высказывалось мнение о негативном влиянии компьютерных игра на психику детей. В качестве решения было предложено ввести ограничение по времени для игровой сессии, стоит ли нам применить данное решение?", "Да", "Нет", 0.7, 1.2));
		childrenQuestions.Add(new Question("Стоит ли нам разработать специальный фильтр нецензурной лексики для нашего продукта?", "Да", "Нет", 1.3, 0.4));
		childrenQuestions.Add(new Question("Родители часто жалуются на необдуманные покупки своих детей в играх и сети, называя главной прчиной - жадность самих разработчиков. Откажемся ли мы от внутриигровых покупок или попросим самих родителей найти другие способы огрничить действия детей?", "Откажемся", "Дело родителей", 1.2, 0.9));
		childrenQuestions.Add(new Question("У нас есть прекрасная возможность добавить образовательную функцию в наш продукт, стоит ли нам этим занятьься?", "Да", "Нет", 1.1, 0.9));
		
		adultQuestions.Add(new Question("Стоит ли нам разработать специальный фильтр нецензурной лексики для нашего продукта?", "Да", "Нет", 0.8, 1.2));
		adultQuestions.Add(new Question("Взрослые люди более мотивированы найти определенную полезную информацию для себя, множетсво отвлекающих компонентов продукта могут помешать этому. Стоит ли нам упростить дизайн?", "Да", "Нет", 1.3, 0.9));
		//adultQuestions.Add(new Question("?", "Да", "Нет", 0.8, 1.2));
		
		maleQuestions.Add(new Question("?", "Да", "Нет", 0.8, 1.2));
		
		femaleQuestions.Add(new Question("?", "Да", "Нет", 0.8, 1.2));
		
		gameCreationStages = new Dictionary<int, Category>();
		gameCreationStages.Add(0, Category.GAME_ENGINE);
		gameCreationStages.Add(1, Category.GAMEPLAY);
		gameCreationStages.Add(2, Category.GRAPHIC);
		gameCreationStages.Add(3, Category.SOUND);
		gameCreationStages.Add(4, Category.PLOT_QUESTS);
		
		SpeedButtonChange();
	}

	void Update()
	{
		if(PlayButtonsToggle[0].isOn)
		{
			Backgrounds[1].SetActive(true);
		}
		else if (PlayButtonsToggle[1].isOn)
		{
			secPerDay = 0.5f;
			Clock();
			IOCheck();
		}
		else if (PlayButtonsToggle[2].isOn)
		{
			secPerDay = 0.2f;
			Pause();
			Clock();
			IOCheck();
		}
	}
	
	public void SpeedButtonChange()
	{
		if(PlayButtonsToggle[0].isOn)
		{
			DarkSpeedButtons[0].SetActive(true);
			DarkSpeedButtons[1].SetActive(false);
			DarkSpeedButtons[2].SetActive(false);
		}
		else if (PlayButtonsToggle[1].isOn)
		{
			DarkSpeedButtons[0].SetActive(false);
			DarkSpeedButtons[1].SetActive(true);
			DarkSpeedButtons[2].SetActive(false);
		}
		else if (PlayButtonsToggle[2].isOn)
		{
			DarkSpeedButtons[0].SetActive(false);
			DarkSpeedButtons[1].SetActive(false);
			DarkSpeedButtons[2].SetActive(true);
		}
	}

	void Clock()
	{
		if (!CheckOnStopTime())
		{
			Backgrounds[0].SetActive(false);
			Backgrounds[1].SetActive(false);
			if (timer >= secPerDay)
			{
				day++;
				if (day >= MaxDay)
				{
					day = 1;
					GlobalController.Instance.week++;
					
					if (GlobalController.Instance.week >= MaxWeek)
					{
						GlobalController.Instance.week = 1;
						GlobalController.Instance.month++;
						
						MonthlyEvents();
						if (GlobalController.Instance.month >= MaxMonth)
						{
							GlobalController.Instance.month = 1;
							GlobalController.Instance.year++;
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
			Backgrounds[0].SetActive(true);
		}
	}
	
	void WeeklyEvents() //События, происходящие каждую новую неделю
	{		
		if (releaseFlag)
		{
			AskQuestion();
			releaseFlag = false;
			askNewQuestionFlag = false;
		}
		
		if (askNewQuestionFlag)
		{
			//AskQuestion();
			if(specialization == Specialization.WEBSITE) AskTechnology(gameCreationStages[askedQuestionCount], GlobalController.Instance.websiteTechs);
			if(specialization == Specialization.GAME) AskTechnology(gameCreationStages[askedQuestionCount], GlobalController.Instance.gameTechs);
			askNewQuestionFlag = false;					
		}
		
		if(analyticsWeeks != 0)
		{
			analyticsWeeks++;
			if(analyticsWeeks >= 4)
			{
				analyticsWeeks = 0;
				AnalyticsPanel.SetActive(true);
			}
		}
	}
	
	void MonthlyEvents() //События, происходящие каждый новый месяц
	{
		Dividends();
		if(GlobalController.Instance.flag) bankController.Payment();
		SetScore(-monthlyExpenses, 0);
	}

	bool CheckOnStopTime()
	{
		bool isCreationActive = Creation.activeSelf;
		bool isTargetAudienceActive = TargetAudience.activeSelf;
		bool isSpecializationActive = SpecializationPanel.activeSelf;
		bool isQuestionActive = QuestionPanel.activeSelf;
		bool isTechnologyActive = TechnologyPanel.activeSelf;
		bool isReleaseActive = Release.activeSelf;
		bool isWinGameActive = WinGamePanel.activeSelf;
		bool isLoseGameActive = LoseGamePanel.activeSelf;
		return isCreationActive || isTargetAudienceActive || isSpecializationActive || isQuestionActive || isTechnologyActive || isReleaseActive || isWinGameActive || isLoseGameActive;
	}

	void SetScore(int deltaMoney, int deltaExperience)
	{
		GlobalController.Instance.moneyScore += deltaMoney;
		GlobalController.Instance.experienceScore += deltaExperience;

		/*if (GlobalController.Instance.moneyScore >= winningScore && !gameFinishedFlag)
		{
			gameFinishedFlag = true;
			WinGame();
		}
		
		if (GlobalController.Instance.moneyScore <= losingScore && !gameFinishedFlag)
		{
			gameFinishedFlag = true;
			LoseGame();
		}*/
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
		specialization = SpecializationCheck();
		AgeAudience ageAudience = AgeAudienceCheck();
		GenderAudience genderAudience = GenderAudienceCheck();
		askedQuestions = new List<Question>();
		productCost = 0;
	
		SetScore(-creationCost, 0);
		product = new Product(productName,
									  1,
									  productCost,
									  projectSize,
									  specialization,
									  ageAudience,
									  genderAudience,
									  PropertyRight.SOLD_TO_ANOTHER_COMPANY,
									  0, 0);
									  
		var agePool = new List<Question>();
		
		if(product.ageAudience == AgeAudience.CHILDREN) agePool = childrenQuestions;
		else if (product.ageAudience == AgeAudience.ADULT) agePool = adultQuestions;
		else agePool = null;
		
		var genderPool = new List<Question>();
		
		if(product.genderAudience == GenderAudience.MALE) genderPool = maleQuestions;
		else if (product.genderAudience == GenderAudience.FEMALE) genderPool = femaleQuestions;
		else genderPool = null;
		
		if(agePool == null && genderPool == null) questionsPool = generalQuestions;
		else if(agePool == null && genderPool != null) questionsPool = generalQuestions.Concat(genderPool).ToList();
		else if(agePool != null && genderPool == null) questionsPool = generalQuestions.Concat(agePool).ToList();
		else questionsPool = generalQuestions.Concat(agePool.Concat(genderPool)).ToList();
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
		if (ProjectSizeButtons[1].GetComponent<Toggle>().isOn)
		{
			ProjectSizeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Стоимость разработки: 200К";
			creationCost = 200000;
			questionNumber = 5;
			return ProjectSize.MAJOR;
		}
		return ProjectSize.MINOR;
	}
	
	Specialization SpecializationCheck()
	{
		if (SpecializationButtons[0].GetComponent<Toggle>().isOn)
		{
			return Specialization.WEBSITE;
		}
		if (SpecializationButtons[1].GetComponent<Toggle>().isOn)
		{
			return Specialization.GAME;
		}
		return Specialization.GAME;
	}
	
	AgeAudience AgeAudienceCheck()
	{
		if (AgeAudienceButtons[0].GetComponent<Toggle>().isOn)
		{
			AgeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Возраст: Дети";
			BottomIcons[1].GetComponent<Image>().sprite = Children;
			return AgeAudience.CHILDREN;
		}
		if (AgeAudienceButtons[1].GetComponent<Toggle>().isOn)
		{
			AgeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Возраст: Все";
			BottomIcons[1].GetComponent<Image>().sprite = EveryAge;
			return AgeAudience.EVERYONE;
		}
		if (AgeAudienceButtons[2].GetComponent<Toggle>().isOn)
		{
			AgeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Возраст: Взрослые";
			BottomIcons[1].GetComponent<Image>().sprite = Adult;
			return AgeAudience.ADULT;
		}
		return AgeAudience.EVERYONE;
	}
	
	GenderAudience GenderAudienceCheck()
	{
		if (GenderAudienceButtons[0].GetComponent<Toggle>().isOn)
		{
			GenderText.GetComponent<TMPro.TextMeshProUGUI>().text = "Пол: Мужчины";
			BottomIcons[0].GetComponent<Image>().sprite = Men;
			return GenderAudience.MALE;
		}
		if (GenderAudienceButtons[1].GetComponent<Toggle>().isOn)
		{
			GenderText.GetComponent<TMPro.TextMeshProUGUI>().text = "Пол: Все";
			BottomIcons[0].GetComponent<Image>().sprite = EveryGender;
			return GenderAudience.EVERYONE;
		}
		if (GenderAudienceButtons[2].GetComponent<Toggle>().isOn)
		{
			GenderText.GetComponent<TMPro.TextMeshProUGUI>().text = "Пол: Женщины";
			BottomIcons[0].GetComponent<Image>().sprite = Women;
			return GenderAudience.FEMALE;
		}
		return GenderAudience.EVERYONE;
	}
	
	PropertyRight PropertyRightCheck()
	{
		if (PropertyRightButtons[0].GetComponent<Toggle>().isOn){ return PropertyRight.SOLD_TO_ANOTHER_COMPANY; }
		if (PropertyRightButtons[1].GetComponent<Toggle>().isOn){ return PropertyRight.BELONGS_TO_OUR_COMPANY; }
		return PropertyRight.BELONGS_TO_OUR_COMPANY;
	}

	public void AskTechnology(Category category, List<Technology> techsList)
	{
		MarkCurrentQuestion(askedQuestionCount);
		askedQuestionCount++;
		var clone = techPrefab;
		var firstClone = firstTechPrefab;
		techonologiesList = new List<Technology>();
		toggleObjects = new List<GameObject>();
		foreach (var tech in techsList)
		{
			isDefaultExist = 
					(tech.category == Category.GAME_ENGINE 
					|| tech.category == Category.GRAPHIC 
					|| tech.category == Category.WEBSITE 
					|| tech.category == Category.FONTS 
					|| tech.category == Category.WEBSITE
					|| tech.category == Category.STRUCTURE
					|| tech.category == Category.TEXT);
			if(tech.category == category)
			{				
				techonologiesList.Add(tech);
				if(isDefaultExist && !created)
				{
					firstClone = Instantiate(firstTechPrefab, TechnologiesList, false); 
					firstClone.GetComponent<Toggle>().group = toggleGroup;
					created = true;
				}
				clone = Instantiate(techPrefab, TechnologiesList, false);	
				if(isDefaultExist) clone.GetComponent<Toggle>().group = toggleGroup;	
				if(tech.state != TechState.RESEARCHED) clone.GetComponent<Toggle>().interactable = false;
				var background = clone.transform.Find("Background").gameObject;
				var techText = background.transform.Find("Text (TMP)").gameObject;
				if(techText != null) techText.GetComponent<TMPro.TextMeshProUGUI>().text = tech.title;
				toggleObjects.Add(clone);
			}
		}
		TechnologyPanel.SetActive(true);
	}
	
	public void ReadTechnology()
	{
		UpdateTechnology();
		TechnologyPanel.SetActive(false);
		created = false;
		foreach(Transform child in TechnologiesList)
		{
			GameObject.Destroy(child.gameObject);
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
	}
	
	public void UpdateTechnology()
	{		
		for(int i = 0; i < techonologiesList.Count; i++)
		{
			if (toggleObjects[i].GetComponent<Toggle>().isOn)
			{
				productCost += techonologiesList[i].productionCost;
				SetScore(-techonologiesList[i].productionCost, 0);
			}	
		}
	}

	public void AskQuestion()
	{		
		var randomQuestion = questionsPool[new System.Random().Next(0, questionsPool.Count)];
	
		while(askedQuestions.Contains(randomQuestion)) randomQuestion = questionsPool[new System.Random().Next(0, questionsPool.Count)];
		
		askedQuestions.Add(randomQuestion);
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
		}
		else if (option == 1)
		{
			answers.Add(new Answer(QuestionTextPanel.GetComponent<TMPro.TextMeshProUGUI>().text,
				AnswerText2Panel.GetComponent<TMPro.TextMeshProUGUI>().text, profit2));
		}
		//MarkDoneQuestion(askedQuestionCount - 1);

		QuestionPanel.SetActive(false);
		Release.SetActive(true);
	}

	public void ReleaseProduct()
	{
		product.propertyRight = PropertyRightCheck();
		profitRatio = GetProfitRatio();
		var income = 0;
		expRatio = creationCost / 10000;
		if (PropertyRightButtons[0].GetComponent<Toggle>().isOn)
		{
			income = (int)((creationCost + productCost) * profitRatio);
			Debug.Log(income);
			Debug.Log(creationCost + productCost);
			product.income = income;
			GlobalController.Instance.products.Add(product);
			SetScore(income, (int)(100 * expRatio));
		}
		else if (PropertyRightButtons[1].GetComponent<Toggle>().isOn)
		{
			income = (int)((creationCost + productCost) * profitRatio);
			product.income = income;
			GlobalController.Instance.products.Add(product);
		}
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
	
	void Dividends()
	{
		foreach(var product in GlobalController.Instance.products)
		{
			if(product.propertyRight == PropertyRight.BELONGS_TO_OUR_COMPANY && product.dividendsCount < 5)
			{
				SetScore((int)product.income / 4, (int)(100 * expRatio));
				product.dividendsCount++;
			}
		}
	}
	
	void Pause()
	{
		
	}
	
	void CheckOnDisableButton()
	{
		if(GlobalController.Instance.moneyScore < 200000) ProjectSizeButtons[1].GetComponent<Toggle>().interactable = false;
		else ProjectSizeButtons[1].GetComponent<Toggle>().interactable = true;
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
	
	public void StartAnalytics(){ analyticsWeeks = 1; }
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