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
	public Dictionary<int, Category> websiteCreationStages;
	
	public GlobalController globalController;
	public AnalyticsController analyticsController;
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
	
	public List<bool> checkList;

	public TextMeshProUGUI Date; //Объект внутриигрового времени
	public GameObject Score; //Объект денежного счёта игрока
	public GameObject ContextMenu; //Контекстное меню
	
	public GameObject AnalyticsPanel;
	public GameObject BankPanel;
	public GameObject CreationPanel; //Панель создания продукта
	public GameObject ProjectSizePanel;
	public GameObject SpecializationPanel; //Панель специализации
	public GameObject ThemePanel;
	public GameObject TargetAudiencePanel; //Панель целевой аудитории

	public TextMeshProUGUI BottomPanelProductName; //Название компании на нижней панели
	public GameObject QuestionPanel; //Панель вопроса
	public GameObject TechnologyPanel;
	public GameObject ReleasePanel; //Панель выпуска продукта
	public GameObject WinGamePanel; //Панель победы
	public GameObject LoseGamePanel; //Панель поражения
	
	public GameObject QuestionTextPanel; //Текст вопроса
	public GameObject AnswerText1Panel; //Текст ответа 1
	public GameObject AnswerText2Panel; //Текст ответа 2

	public GameObject InputProductName; //Ввод названия продукта
	public GameObject ProjectSizeText;
	public GameObject AgeText;
	public GameObject GenderText;
	
	public List<Button> AnswerButtons;
	public List<Toggle> PlayButtonsToggle;
	public List<GameObject> DarkSpeedButtons;
	public List<GameObject> Backgrounds;
	public List<Button> CreationButtons;
	public List<GameObject> EducationPanels;
	public List<GameObject> Stages;
	public List<TextMeshProUGUI> StageTexts;

	public GameObject[] ProjectSizeButtons;
	public GameObject[] SpecializationButtons;
	public GameObject[] ThemeButtons;
	public GameObject[] AgeAudienceButtons;
	public GameObject[] GenderAudienceButtons;
	public GameObject[] PropertyRightButtons;
	public GameObject[] Cells;
	public Button CreateButton;
	public Button AnalyticsButton;
	
	public Product product;

	//Общие переменные
	float secPerDay = 0.2f; //Число реальных секунд приходящихся на один игровой день

	const int MaxDay = 8; //Ограничитель счётчика дней
	const int MaxWeek = 5; //Ограничитель счётчика недель
	const int MaxMonth = 13; //Ограничитель счётчика месяцев

	static int losingScore = -100000;
	
	//Переменные продукта
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
	bool demandedFlag = false;
	
	//Вспомогательные переменные
	string productName;
	int creationCost; //Стоимость создания нового продукта
	int techsCost;
	double ratio = 1;
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
		GlobalController.Instance.gameExist = true;
	
		AnswerButtons[0].onClick.AddListener(() => ReadAnswer(0));
		AnswerButtons[1].onClick.AddListener(() => ReadAnswer(1));
		
		generalQuestions.Add(new Question("Мы создаем новое меню для пользовательского интерфейса, на что стоит уделить большее внимание?", "Красота", "Фукционал", 0.8, 1.4));
		generalQuestions.Add(new Question("Для разработки продукта в коллектив требуются новые люди. Какой уровень будущих сотрудников будет приемлемым?", "Студенты", "Специалисты", 0.75, 1.4));
		generalQuestions.Add(new Question("Презентовать ли будущий продукт на выставке IT-отрасли?", "Да", "Нет", 1.5, 0.9));
		generalQuestions.Add(new Question("Недавно разгорелся скандал из-за утечки данных о пользователях популярного сайта доставки еды. Стоит ли нам лучше поработать над защитой персональных данных?", "Да", "Нет", 1.4, 1));
		generalQuestions.Add(new Question("Один из сотрудников компании на недавнем совещании заявил о скором увольнении из-за переезда. Стоит ли нам уже сейчас найти ему замену", "Да", "Нет", 1.5, 0.8));
		generalQuestions.Add(new Question("Один из сотрудников заявил о необходимости найма Scrum-менеджера для более грамотной работы команды. Стоит ли нам вложиться в это?", "Да", "Нет", 1.5, 0.9));
		generalQuestions.Add(new Question("Использовать ли новую неопробованную технологию обратной связи с разработчиками на нашем продукте?", "Да", "Нет", 0.9, 1.5));
		generalQuestions.Add(new Question("Уже скоро наступит дата очередного дедлайна для нашей команды, однако темп разработки говорит о нехватке времени для тестирования продукта. Стоит ли закрыть глаза на тесты и сделать работу в срок?", "Да", "Нет", 0.85, 1.3));
		generalQuestions.Add(new Question("Скоро наступит лето, большинство сотрудников на это время запланировали свои отпуска. Для успешного завершения проекта нужно либо нанять новых сотрудников на время, либо обратиться к фрилансу. Что мы сделаем?", "Новые люди", "Фриланс", 1.5, 0.8));
		generalQuestions.Add(new Question("В последнее время сотрудники все больше говорят о необходимости переезда в более просторный офис. Послушать ли сотрудников?", "Да", "Нет", 1.4, 0.9));
		generalQuestions.Add(new Question("Один из сотрудников жалуется наневыносимую жару в кабинете даже с открытым окном. Поставить ли кондиционеры в офисе?", "Да", "Нет", 1.2, 0.95));
		generalQuestions.Add(new Question("Все больше IT-компаний предоставляют своим подчиненным ДМС(Дополнительное Медицинское Страхование), стоит ли нам подхватить эту волну?", "Да", "Нет", 1.3, 1));
		generalQuestions.Add(new Question("Недавно PR-менеджер компании предложил прорекламировать будущий продукт, заплатив гонорар известным блогерам. Стоит ли послушать его?", "Да", "Нет", 1.5, 1));
		
		childrenQuestions.Add(new Question("Для привлечения внимания детской аудитории маркетологи советуют разнообразить цветовую палитру продукта, однако это может негативно сказаться на других аспектах, стоит ли нам попробовать?", "Да", "Нет", 1.3, 0.8));
		childrenQuestions.Add(new Question("В последнем выпуске популярного журнала по психологии высказывалось мнение о негативном влиянии компьютерных игра на психику детей. В качестве решения было предложено ввести ограничение по времени для игровой сессии, стоит ли нам применить данное решение?", "Да", "Нет", 0.7, 1.6));
		childrenQuestions.Add(new Question("Стоит ли нам разработать специальный фильтр нецензурной лексики для нашего продукта?", "Да", "Нет", 1.3, 0.4));
		childrenQuestions.Add(new Question("Родители часто жалуются на необдуманные покупки своих детей в играх и сети, называя главной прчиной - жадность самих разработчиков. Откажемся ли мы от внутриигровых покупок или попросим самих родителей найти другие способы огрничить действия детей?", "Откажемся", "Дело родителей", 1.6, 0.9));
		childrenQuestions.Add(new Question("У нас есть прекрасная возможность добавить образовательную функцию в наш продукт, стоит ли нам этим занятьься?", "Да", "Нет", 1.4, 0.9));
		
		adultQuestions.Add(new Question("Стоит ли нам разработать специальный фильтр нецензурной лексики для нашего продукта?", "Да", "Нет", 0.8, 1.3));
		adultQuestions.Add(new Question("Взрослые люди более мотивированы найти определенную полезную информацию для себя, множетсво отвлекающих компонентов продукта могут помешать этому. Стоит ли нам упростить дизайн?", "Да", "Нет", 1.3, 0.9));
		//adultQuestions.Add(new Question("?", "Да", "Нет", 0.8, 1.2));
		
		//maleQuestions.Add(new Question("?", "Да", "Нет", 0.8, 1.2));
		
		//femaleQuestions.Add(new Question("?", "Да", "Нет", 0.8, 1.2));
		
		gameCreationStages = new Dictionary<int, Category>();
		gameCreationStages.Add(0, Category.GAME_ENGINE);
		gameCreationStages.Add(1, Category.GAMEPLAY);
		gameCreationStages.Add(2, Category.GRAPHIC);
		gameCreationStages.Add(3, Category.WORLD_DESIGN);
		gameCreationStages.Add(4, Category.PLOT_QUESTS);
		gameCreationStages.Add(5, Category.AI);
		gameCreationStages.Add(6, Category.DIALOGS);
		gameCreationStages.Add(7, Category.LEVEL_DESIGN);
		gameCreationStages.Add(8, Category.TECHNICAL_FEATURES);
		gameCreationStages.Add(9, Category.SOUND);
		
		websiteCreationStages = new Dictionary<int, Category>();
		websiteCreationStages.Add(0, Category.WEBSITE);
		websiteCreationStages.Add(1, Category.STRUCTURE);
		websiteCreationStages.Add(2, Category.TEXT);
		websiteCreationStages.Add(3, Category.DESIGN);
		websiteCreationStages.Add(4, Category.FONTS);
		websiteCreationStages.Add(5, Category.INTERACT);
		websiteCreationStages.Add(6, Category.SITE_FEATURES);
		websiteCreationStages.Add(7, Category.TECHNOLOGIES);
		websiteCreationStages.Add(8, Category.MARKETING);
		
		analyticsController.Awake();
		SpeedButtonChange();
		if(GlobalController.Instance.eduWelcome) {EducationPanels[0].SetActive(true); GlobalController.Instance.eduWelcome = false;}
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
		if (GlobalController.Instance.gameWon)
		{
			WinGame();
		}
		if (releaseFlag)
		{
			AskQuestion();
			releaseFlag = false;
			askNewQuestionFlag = false;
		}
		if (askNewQuestionFlag)
		{
			MarkCurrentQuestion(askedQuestionCount);
			if(!demandedFlag)
			{
				if(product.specialization == Specialization.WEBSITE)
				{
					AskTechnology(websiteCreationStages[askedQuestionCount], GlobalController.Instance.websiteTechs);
				}
				if(product.specialization == Specialization.GAME)
				{
					AskTechnology(gameCreationStages[askedQuestionCount], GlobalController.Instance.gameTechs);
				}
				askNewQuestionFlag = false;			
			}
			else
			{
				askedQuestionCount++;
				if(askedQuestionCount > questionsCount)
				{
					askNewQuestionFlag = false;
					releaseFlag = true;
				}
			}
		}
		if(analyticsWeeks != 0)
		{
			analyticsWeeks++;
			if(analyticsWeeks >= 4)
			{
				analyticsWeeks = 0;
				AnalyticsButton.interactable = true;
				AnalyticsPanel.SetActive(true);
				if(GlobalController.Instance.eduAnalytics) {GlobalController.Instance.eduAnalytics = false; EducationPanels[21].SetActive(true);}
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
		checkList = new List<bool>();
		checkList.Add(BankPanel.activeSelf);
		checkList.Add(AnalyticsPanel.activeSelf);
		checkList.Add(CreationPanel.activeSelf);
		
		checkList.Add(ProjectSizePanel.activeSelf);
		checkList.Add(SpecializationPanel.activeSelf);
		checkList.Add(ThemePanel.activeSelf);
		checkList.Add(TargetAudiencePanel.activeSelf);
		
		checkList.Add(TechnologyPanel.activeSelf);
		checkList.Add(QuestionPanel.activeSelf);
		checkList.Add(ReleasePanel.activeSelf);
		
		checkList.Add(WinGamePanel.activeSelf);
		checkList.Add(LoseGamePanel.activeSelf);
		
		foreach(bool check in checkList)
		{
			if(check) return true;
		}
		return false;
	}

	void SetScore(int deltaMoney, int deltaExperience)
	{
		GlobalController.Instance.moneyScore += deltaMoney;
		GlobalController.Instance.experienceScore += deltaExperience;
		
		if (GlobalController.Instance.moneyScore <= losingScore && !gameFinishedFlag)
		{
			gameFinishedFlag = true;
			LoseGame();
		}
	}
	
	void IOCheck()
	{
		if (Input.GetKeyUp(KeyCode.Mouse1) && !CheckOnStopTime())
		{
			if(GlobalController.Instance.eduContext) {EducationPanels[4].SetActive(true); GlobalController.Instance.eduContext = false; ContextMenu.transform.position = new Vector3(960,540,0);}
			else ContextMenu.transform.position = Input.mousePosition; 
			ContextMenu.SetActive(!ContextMenu.activeSelf);
		}
	}	
	
	public void EduWelcome()
	{
		if(GlobalController.Instance.eduWelcome) GlobalController.Instance.eduWelcome = false;
	}
	
	public void EduCreation()
	{
		if(GlobalController.Instance.eduCreation) {EducationPanels[5].SetActive(true); GlobalController.Instance.eduCreation = false;}
	}
	
	public void EduTheme()
	{
		if(GlobalController.Instance.eduTheme) {EducationPanels[7].SetActive(true); GlobalController.Instance.eduTheme = false;}
	}
	
	public void EduBank()
	{
		if(GlobalController.Instance.eduBank) {EducationPanels[19].SetActive(true); GlobalController.Instance.eduBank = false;}
	}
	
	public void EduAnalytics()
	{
		if(GlobalController.Instance.eduAnalytics) EducationPanels[20].SetActive(true);
	}
	
	public void CreateNewProduct()
	{
		askNewQuestionFlag = true;
		CreateButton.interactable = false;
		
		productName = InputProductName.GetComponent<TMP_InputField>().text;
		BottomPanelProductName.text = productName;
		ResetBottomPanel();
		
		ProjectSize projectSize = ProjectSizeCheck();
		Specialization specialization = SpecializationCheck();
		Theme theme = ThemeCheck();
		AgeAudience ageAudience = AgeAudienceCheck();
		GenderAudience genderAudience = GenderAudienceCheck();
		askedQuestions = new List<Question>();
	
		SetScore(-creationCost, 0);
		SetScore(-techsCost, 0);
		product = new Product(productName,
									  ratio,
									  creationCost,
									  projectSize,
									  theme,
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
	
	public void SetDemandedProduct(DemandedProduct newProduct)
	{
		AnalyticsPanel.SetActive(false);
		ResetBottomPanel();
		demandedFlag = true;
		
		if(newProduct.projectSize == ProjectSize.MINOR) ProjectSizeButtons[0].GetComponent<Toggle>().isOn = true;
		if(newProduct.projectSize == ProjectSize.MAJOR) ProjectSizeButtons[1].GetComponent<Toggle>().isOn = true;
		
		if(newProduct.specialization == Specialization.WEBSITE) SpecializationButtons[0].GetComponent<Toggle>().isOn = true;
		if(newProduct.specialization == Specialization.GAME) SpecializationButtons[1].GetComponent<Toggle>().isOn = true;
		
		if(newProduct.theme == Theme.SPORT) ThemeButtons[0].GetComponent<Toggle>().isOn = true;
		if(newProduct.theme == Theme.MUSIC) ThemeButtons[1].GetComponent<Toggle>().isOn = true;
		if(newProduct.theme == Theme.LOVE) ThemeButtons[2].GetComponent<Toggle>().isOn = true;
		if(newProduct.theme == Theme.FASHION) ThemeButtons[3].GetComponent<Toggle>().isOn = true;
		if(newProduct.theme == Theme.SCHOOL) ThemeButtons[4].GetComponent<Toggle>().isOn = true;
		if(newProduct.theme == Theme.SCIENCE) ThemeButtons[5].GetComponent<Toggle>().isOn = true;
		if(newProduct.theme == Theme.SPACE) ThemeButtons[6].GetComponent<Toggle>().isOn = true;
		if(newProduct.theme == Theme.WEATHER) ThemeButtons[7].GetComponent<Toggle>().isOn = true;

		if(newProduct.ageAudience == AgeAudience.CHILDREN) AgeAudienceButtons[0].GetComponent<Toggle>().isOn = true;
		if(newProduct.ageAudience == AgeAudience.EVERYONE) AgeAudienceButtons[1].GetComponent<Toggle>().isOn = true;
		if(newProduct.ageAudience == AgeAudience.ADULT) AgeAudienceButtons[2].GetComponent<Toggle>().isOn = true;
		
		if(newProduct.genderAudience == GenderAudience.MALE) GenderAudienceButtons[0].GetComponent<Toggle>().isOn = true;
		if(newProduct.genderAudience == GenderAudience.EVERYONE) GenderAudienceButtons[1].GetComponent<Toggle>().isOn = true;
		if(newProduct.genderAudience == GenderAudience.FEMALE) GenderAudienceButtons[2].GetComponent<Toggle>().isOn = true;
	
		CreationButtons[0].interactable = false;			
		CreationButtons[1].interactable = false;
		CreationButtons[2].interactable = false;			
		CreationButtons[3].interactable = false;
		
		techsCost = FindTotalCost(newProduct.usedTechs);
		ratio = 1.4;
		CreationPanel.SetActive(true);
	}
	
	public int FindTotalCost(List<Technology> techs)
	{
		var sum = 0;
		foreach(var tech in techs)
		{
			sum += tech.productionCost;
		}
		return sum;
	}
	
	ProjectSize ProjectSizeCheck()
	{
		if (ProjectSizeButtons[0].GetComponent<Toggle>().isOn)
		{
			ProjectSizeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Стоимость разработки: 25К";
			creationCost = 25000;
			questionsCount = 5;
			return ProjectSize.MINOR;
		}
		if (ProjectSizeButtons[1].GetComponent<Toggle>().isOn)
		{
			ProjectSizeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Стоимость разработки: 200К";
			creationCost = 200000;
			questionsCount = 9;
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
		return Specialization.WEBSITE;
	}
	
	Theme ThemeCheck()
	{
		if (SpecializationButtons[0].GetComponent<Toggle>().isOn)
		{
			return Theme.SPORT;
		}
		if (SpecializationButtons[1].GetComponent<Toggle>().isOn)
		{
			return Theme.MUSIC;
		}
		if (SpecializationButtons[2].GetComponent<Toggle>().isOn)
		{
			return Theme.LOVE;
		}
		if (SpecializationButtons[3].GetComponent<Toggle>().isOn)
		{
			return Theme.FASHION;
		}
		if (SpecializationButtons[4].GetComponent<Toggle>().isOn)
		{
			return Theme.SCHOOL;
		}
		if (SpecializationButtons[5].GetComponent<Toggle>().isOn)
		{
			return Theme.SCIENCE;
		}
		if (SpecializationButtons[6].GetComponent<Toggle>().isOn)
		{
			return Theme.SPACE;
		}
		if (SpecializationButtons[7].GetComponent<Toggle>().isOn)
		{
			return Theme.WEATHER;
		}
		return Theme.SPORT;
	}
	
	AgeAudience AgeAudienceCheck()
	{
		if (AgeAudienceButtons[0].GetComponent<Toggle>().isOn)
		{
			AgeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Возраст: Дети";
			return AgeAudience.CHILDREN;
		}
		if (AgeAudienceButtons[1].GetComponent<Toggle>().isOn)
		{
			AgeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Возраст: Все";
			return AgeAudience.EVERYONE;
		}
		if (AgeAudienceButtons[2].GetComponent<Toggle>().isOn)
		{
			AgeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Возраст: Взрослые";
			return AgeAudience.ADULT;
		}
		return AgeAudience.EVERYONE;
	}
	
	GenderAudience GenderAudienceCheck()
	{
		if (GenderAudienceButtons[0].GetComponent<Toggle>().isOn)
		{
			GenderText.GetComponent<TMPro.TextMeshProUGUI>().text = "Пол: Мужчины";
			return GenderAudience.MALE;
		}
		if (GenderAudienceButtons[1].GetComponent<Toggle>().isOn)
		{
			GenderText.GetComponent<TMPro.TextMeshProUGUI>().text = "Пол: Все";
			return GenderAudience.EVERYONE;
		}
		if (GenderAudienceButtons[2].GetComponent<Toggle>().isOn)
		{
			GenderText.GetComponent<TMPro.TextMeshProUGUI>().text = "Пол: Женщины";
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
		askedQuestionCount++;

		var clone = techPrefab;
		var firstClone = firstTechPrefab;
		techonologiesList = new List<Technology>();
		toggleObjects = new List<GameObject>();
		foreach (var tech in techsList)
		{
			isDefaultExist = (tech.category == Category.GAME_ENGINE 
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
		if(GlobalController.Instance.eduTechChoose) {EducationPanels[10].SetActive(true); GlobalController.Instance.eduTechChoose = false;}
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
				product.productCost += techonologiesList[i].productionCost;
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

		QuestionPanel.SetActive(false);
		ReleasePanel.SetActive(true);
		if(GlobalController.Instance.eduRelease) {EducationPanels[14].SetActive(true); GlobalController.Instance.eduRelease = false;}
	}

	public void ReleaseProduct()
	{
		product.propertyRight = PropertyRightCheck();
		profitRatio = GetProfitRatio();
		product.gainedExp = (int)product.productCost / 100;
		
		if (PropertyRightButtons[0].GetComponent<Toggle>().isOn)
		{
			product.income = (int)((product.productCost) * profitRatio * ratio);
			GlobalController.Instance.products.Add(product);
			SetScore(product.income, product.gainedExp);
		}
		else if (PropertyRightButtons[1].GetComponent<Toggle>().isOn)
		{
			product.income = (int)((product.productCost) * profitRatio * ratio);
			GlobalController.Instance.products.Add(product);
		}
		
		analyticsController.GetNewProducts();
		CreationButtons[0].interactable = true;			
		CreationButtons[1].interactable = true;
		CreationButtons[2].interactable = true;			
		CreationButtons[3].interactable = true;
		CreateButton.interactable = true;
		demandedFlag = false;
		techsCost = 0;
		ratio = 1;
	}

	double GetProfitRatio()
	{
		double k = 1;
		foreach (var answer in answers)
		{
			k *= answer.AnswerProfitRatio;
		}
		answers.Clear();
		return k * 1.4;
	}
	
	void Dividends()
	{
		foreach(var product in GlobalController.Instance.products)
		{
			if(product.propertyRight == PropertyRight.BELONGS_TO_OUR_COMPANY && product.dividendsCount < 5)
			{
				SetScore((int)product.income / 4, (int)((100 * expRatio) / 4 ));
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

	public void WinGame()
	{
		WinGamePanel.SetActive(true);
	}
	
	void LoseGame()
	{
		LoseGamePanel.SetActive(true);
	}
	
	void MarkCurrentQuestion(int questionNumber)
	{
		if(questionNumber == 0)
		{
			Stages[0].SetActive(false);
			Stages[1].SetActive(false);
			Stages[2].SetActive(true);
			Stages[3].SetActive(true);
			Stages[4].SetActive(true);
			
			StageTexts[2].text = "I";
			StageTexts[3].text = "II";
			StageTexts[4].text = "III";
		}
		else if(questionNumber == 1)
		{
			Stages[0].SetActive(false);
			Stages[1].SetActive(true);
			Stages[2].SetActive(true);
			Stages[3].SetActive(true);
			Stages[4].SetActive(true);
			
			StageTexts[1].text = "I";
			StageTexts[2].text = "II";
			StageTexts[3].text = "III";
			StageTexts[4].text = "IV";
		}
		else if(questionsCount == 5 && questionNumber == 3)
		{
			Stages[0].SetActive(true);
			Stages[1].SetActive(true);
			Stages[2].SetActive(true);
			Stages[3].SetActive(true);
			Stages[4].SetActive(false);
			
			StageTexts[0].text = "II";
			StageTexts[1].text = "III";
			StageTexts[2].text = "IV";
			StageTexts[3].text = "V";
		}
		else if(questionsCount == 5 && questionNumber == 4)
		{
			Stages[0].SetActive(true);
			Stages[1].SetActive(true);
			Stages[2].SetActive(true);
			Stages[3].SetActive(false);
			Stages[4].SetActive(false);
			
			StageTexts[0].text = "III";
			StageTexts[1].text = "IV";
			StageTexts[2].text = "V";
		}
		else if(questionsCount == 9 && questionNumber == 7)
		{
			Stages[0].SetActive(true);
			Stages[1].SetActive(true);
			Stages[2].SetActive(true);
			Stages[3].SetActive(true);
			Stages[4].SetActive(false);
			
			StageTexts[0].text = "VI";
			StageTexts[1].text = "VII";
			StageTexts[2].text = "VIII";
			StageTexts[3].text = "IX";
		}
		else if(questionsCount == 9 && questionNumber == 8)
		{
			Stages[0].SetActive(true);
			Stages[1].SetActive(true);
			Stages[2].SetActive(true);
			Stages[3].SetActive(false);
			Stages[4].SetActive(false);
			
			StageTexts[0].text = "VII";
			StageTexts[1].text = "VIII";
			StageTexts[2].text = "IX";
		}
		else
		{
			Stages[0].SetActive(true);
			Stages[1].SetActive(true);
			Stages[2].SetActive(true);
			Stages[3].SetActive(true);
			Stages[4].SetActive(true);
			
			if(questionNumber == 2)
			{
				StageTexts[0].text = "I";
				StageTexts[1].text = "II";
				StageTexts[2].text = "III";
				StageTexts[3].text = "IV";
				StageTexts[4].text = "V";
			}
			else if(questionNumber == 3)
			{
				StageTexts[0].text = "II";
				StageTexts[1].text = "III";
				StageTexts[2].text = "IV";
				StageTexts[3].text = "V";
				StageTexts[4].text = "VI";
			}
			else if(questionNumber == 4)
			{
				StageTexts[0].text = "III";
				StageTexts[1].text = "IV";
				StageTexts[2].text = "V";
				StageTexts[3].text = "VI";
				StageTexts[4].text = "VII";
			}
			else if(questionNumber == 5)
			{
				StageTexts[0].text = "IV";
				StageTexts[1].text = "V";
				StageTexts[2].text = "VI";
				StageTexts[3].text = "VII";
				StageTexts[4].text = "VIII";
			}
			else if(questionNumber == 6)
			{
				StageTexts[0].text = "V";
				StageTexts[1].text = "VI";
				StageTexts[2].text = "VII";
				StageTexts[3].text = "VIII";
				StageTexts[4].text = "IX";
			}
		}
		//Cells[questionNumber].GetComponent<Image>().color = new Color32(154, 154, 154, 255);
	}
	
	void ResetBottomPanel()
	{
		questionNumber = 0;
	}
	
	public void StartAnalytics()
	{ 
		AnalyticsButton.interactable = false;
		analyticsWeeks = 1; 
	}
	
	public void EducationBank()
	{
		if(GlobalController.Instance.eduBank) {GlobalController.Instance.eduBank = false; EducationPanels[19].SetActive(true);}
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