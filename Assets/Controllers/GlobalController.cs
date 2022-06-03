using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
	public static GlobalController Instance;
	MainSceneController mainSceneController;
	BankController bankController;

	public int moneyScore;
	public int experienceScore;
	public int week;
	public int month;
	public int year;
	public bool flag;
	public List<Credit> credits;
	public List<Product> products;
	public List<DemandedProduct> demanded;
	public List<Technology> gameTechs;
	public List<Technology> websiteTechs;
	public List<Technology> researchedTechs;

	void Awake()
	{	
		if(Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		
		Instance = this;
	    DontDestroyOnLoad(gameObject);
	}
	
    void Start()
    {
		moneyScore = 5000000;
		experienceScore = 5000000;
		week = 1;
		month = 1;
		year = 1;
		flag = false;
		credits = new List<Credit>();
		products = new List<Product>();
		demanded = new List<DemandedProduct>();
		gameTechs = new List<Technology>();
		websiteTechs = new List<Technology>();
		researchedTechs = new List<Technology>();
		
		Initialize();
    }

    void Update()
	{
    }
	
	void Initialize()
	{
		//Первый этап
		gameTechs.Add(new Technology("Игровой движок v.1", Category.GAME_ENGINE, 5000, 10000, 150, TechState.AVAILABLE, new List<int>() {1, 2, 3} )); //0
		
		gameTechs.Add(new Technology("2D графика v.1", Category.GRAPHIC, 7500, 15000, 250, TechState.CLOSED, new List<int>() {4} )); //1
		gameTechs.Add(new Technology("Линейный сюжет", Category.PLOT_QUESTS, 10000, 15000, 200, TechState.CLOSED, new List<int>() {4} )); //2
		gameTechs.Add(new Technology("Монозвук", Category.SOUND, 7500, 10000, 200, TechState.CLOSED, new List<int>() {4})); //3 
		
		//Второй этап
		gameTechs.Add(new Technology("Игровой движок v.2", Category.GAME_ENGINE, 25000, 50000, 500, TechState.CLOSED, new List<int>() {5, 6, 7} )); //4
		
		gameTechs.Add(new Technology("Сохранения", Category.TECHNICAL_FEATURES, 13000, 35000, 300, TechState.CLOSED, new List<int>() {8, 9} )); //5
		gameTechs.Add(new Technology("Редактор уровней", Category.LEVEL_DESIGN, 15000, 50000, 350, TechState.CLOSED, new List<int>() {10, 11} )); //6
		gameTechs.Add(new Technology("Смена дня и ночи", Category.WORLD_DESIGN, 10000, 40000, 300, TechState.CLOSED, new List<int>() {12, 13} )); //7
		
		gameTechs.Add(new Technology("Базовый ИИ", Category.AI, 35000, 60000, 500, TechState.CLOSED, null)); //8
		gameTechs.Add(new Technology("Мультиплеер", Category.TECHNICAL_FEATURES, 50000, 75000, 650, TechState.CLOSED, null)); //9
		
		gameTechs.Add(new Technology("Обучение", Category.GAMEPLAY, 25000, 50000, 450, TechState.CLOSED, new List<int>() {14})); //10
		gameTechs.Add(new Technology("Внутриигровая валюта", Category.GAMEPLAY, 35000, 50000, 450, TechState.CLOSED, null)); //11
	
		gameTechs.Add(new Technology("Открытый мир", Category.WORLD_DESIGN, 50000, 100000, 600, TechState.CLOSED, null)); //12
		gameTechs.Add(new Technology("Улучшенные диалоги", Category.DIALOGS, 40000, 80000, 500, TechState.CLOSED, null)); //13
		
		//Третий этап 
		gameTechs.Add(new Technology("Игровой движок v.3", Category.GAME_ENGINE, 100000, 250000, 1250, TechState.CLOSED, new List<int>() {15, 16, 17} )); //14
		
		gameTechs.Add(new Technology("2D графика v.2", Category.GRAPHIC, 80000, 150000, 1000, TechState.CLOSED, new List<int>() {18, 19, 20} )); //15
		gameTechs.Add(new Technology("Развитие персонажа", Category.GAMEPLAY, 90000, 125000, 1200, TechState.CLOSED, new List<int>() {21, 22, 23} )); //16
		gameTechs.Add(new Technology("Стереозвук", Category.SOUND, 100000, 175000, 1150, TechState.CLOSED, new List<int>() {24, 25, 26} )); //17
		
		gameTechs.Add(new Technology("3D графика v.1", Category.GRAPHIC, 150000, 200000, 1500, TechState.CLOSED, null)); //18
		gameTechs.Add(new Technology("Улучшенный ИИ", Category.AI, 200000, 300000, 2200, TechState.CLOSED, null)); //19
		gameTechs.Add(new Technology("Видеоролики", Category.TECHNICAL_FEATURES, 175000, 250000, 1800, TechState.CLOSED, null )); //20
		
		gameTechs.Add(new Technology("Разветвленный сюжет", Category.PLOT_QUESTS, 175000, 250000, 1750, TechState.CLOSED, null)); //21
		gameTechs.Add(new Technology("Дерево способностей", Category.GAMEPLAY, 250000, 450000, 2250, TechState.CLOSED, null)); //22
		gameTechs.Add(new Technology("\"Пасхалки\"", Category.LEVEL_DESIGN, 150000, 200000, 1600, TechState.CLOSED, null )); //23
		
		gameTechs.Add(new Technology("Дерево диалогов", Category.DIALOGS, 220000, 350000, 2000, TechState.CLOSED, null)); //24
		gameTechs.Add(new Technology("Улучшенные текстуры", Category.WORLD_DESIGN, 240000, 400000, 2000, TechState.CLOSED, null)); //25
		gameTechs.Add(new Technology("Объемный звук", Category.SOUND, 225000, 350000, 2200, TechState.CLOSED, null)); //26
		
		
		
		//Первый этап
		websiteTechs.Add(new Technology("HTML-страница", Category.WEBSITE, 5000, 10000, 150, TechState.AVAILABLE, new List<int>() {1, 2, 3} )); //0
		
		websiteTechs.Add(new Technology("Гиперссылки", Category.SITE_FEATURES, 7000, 15000, 250, TechState.CLOSED, new List<int>() {4} )); //1
		websiteTechs.Add(new Technology("Собственные шрифты", Category.FONTS, 15000, 10000, 200, TechState.CLOSED, new List<int>() {4} )); //2
		websiteTechs.Add(new Technology("Базовый текст", Category.TEXT, 10000, 10000, 200, TechState.CLOSED, new List<int>() {4})); //3 
		
		//Второй этап
		websiteTechs.Add(new Technology("Динамический сайт", Category.WEBSITE, 5000, 10000, 150, TechState.CLOSED, new List<int>() {5, 6, 7} )); //4
		
		websiteTechs.Add(new Technology("Система CMS", Category.TECHNOLOGIES, 10000, 35000, 100, TechState.CLOSED, new List<int>() {8, 9} )); //5
		websiteTechs.Add(new Technology("Стилизация", Category.DESIGN, 10000, 35000, 100, TechState.CLOSED, new List<int>() {10, 11} )); //6
		websiteTechs.Add(new Technology("Графический интерфейс", Category.INTERACT, 100, 50000, 350, TechState.CLOSED, new List<int>() {12, 13} )); //7
		
		websiteTechs.Add(new Technology("Cookie", Category.SITE_FEATURES, 100, 20000, 250, TechState.CLOSED, null)); //8
		websiteTechs.Add(new Technology("Веб-сервисы", Category.TECHNOLOGIES, 100, 50000, 500, TechState.CLOSED,  null)); //9
		
		websiteTechs.Add(new Technology("Табличная верстка", Category.STRUCTURE, 100, 10000, 100, TechState.CLOSED, new List<int>() {14})); //10
		websiteTechs.Add(new Technology("Простая анимация", Category.DESIGN, 100, 100000, 1000, TechState.CLOSED, null)); //11
		
		websiteTechs.Add(new Technology("Лаконичный текст", Category.TEXT,100,  100000, 1000, TechState.CLOSED, null)); //12
		websiteTechs.Add(new Technology("Целевая аудитория", Category.MARKETING, 100, 100000, 1000, TechState.CLOSED, null)); //13
	
		//Третий этап 
		websiteTechs.Add(new Technology("Веб-приложение", Category.WEBSITE, 5000, 10000, 150, TechState.CLOSED, new List<int>() {15, 16, 17} )); //14
		
		websiteTechs.Add(new Technology("Веб-технологии", Category.TECHNOLOGIES, 10000, 35000, 100, TechState.CLOSED, new List<int>() {18, 19, 20} )); //15
		websiteTechs.Add(new Technology("Дизайн-макет", Category.DESIGN, 10000, 35000, 100, TechState.CLOSED, new List<int>() {21, 22, 23} )); //16
		websiteTechs.Add(new Technology("Маркетинг", Category.MARKETING, 100, 50000, 350, TechState.CLOSED, new List<int>() {24, 25, 26} )); //17
		
		websiteTechs.Add(new Technology("База данных", Category.SITE_FEATURES, 100, 20000, 250, TechState.CLOSED, null)); //18
		websiteTechs.Add(new Technology("Фреймворки", Category.TECHNOLOGIES, 100, 50000, 500, TechState.CLOSED, null)); //19
		websiteTechs.Add(new Technology("Портативность", Category.SITE_FEATURES, 100, 50000, 500, TechState.CLOSED, null)); //20
		
		websiteTechs.Add(new Technology("Стили дизайна", Category.DESIGN, 100, 10000, 100, TechState.CLOSED, null)); //21
		websiteTechs.Add(new Technology("Семейства шрифтов", Category.FONTS, 100, 100000, 1000, TechState.CLOSED, null)); //22
		websiteTechs.Add(new Technology("Блочная верстка", Category.STRUCTURE, 100, 50000, 500, TechState.CLOSED, null )); //23
		
		websiteTechs.Add(new Technology("Аналитика", Category.MARKETING,100,  100000, 1000, TechState.CLOSED, null)); //24
		websiteTechs.Add(new Technology("Реклама", Category.MARKETING, 100, 100000, 1000, TechState.CLOSED, null)); //25
		websiteTechs.Add(new Technology("Тестирование", Category.MARKETING, 100, 50000, 500, TechState.CLOSED, null )); //26
		
		credits.Add(new Credit(CREDIT_SIZE.SMALL, false, 50000, 4560, 12));
		credits.Add(new Credit(CREDIT_SIZE.MEDIUM, false, 125000, 11460, 12));
		credits.Add(new Credit(CREDIT_SIZE.BIG, false, 250000, 22920, 12));
		
		demanded.Add(new DemandedProduct(ProjectSize.MINOR, Specialization.WEBSITE, Theme.SPORT, AgeAudience.CHILDREN, GenderAudience.MALE, new List<Technology>(){websiteTechs[0], websiteTechs[3]}));
		demanded.Add(new DemandedProduct(ProjectSize.MINOR, Specialization.GAME, Theme.SCIENCE, AgeAudience.CHILDREN, GenderAudience.MALE, new List<Technology>(){gameTechs[3]}));
		demanded.Add(new DemandedProduct(ProjectSize.MINOR, Specialization.WEBSITE, Theme.FASHION, AgeAudience.EVERYONE, GenderAudience.FEMALE, new List<Technology>(){websiteTechs[0], websiteTechs[5]}));
		demanded.Add(new DemandedProduct(ProjectSize.MINOR, Specialization.GAME, Theme.WEATHER, AgeAudience.EVERYONE, GenderAudience.FEMALE, new List<Technology>(){gameTechs[3]}));
		demanded.Add(new DemandedProduct(ProjectSize.MINOR, Specialization.WEBSITE, Theme.SPACE, AgeAudience.ADULT, GenderAudience.MALE, new List<Technology>(){websiteTechs[0], websiteTechs[2]}));
		demanded.Add(new DemandedProduct(ProjectSize.MINOR, Specialization.GAME, Theme.MUSIC, AgeAudience.ADULT, GenderAudience.FEMALE, new List<Technology>(){gameTechs[3]}));
		demanded.Add(new DemandedProduct(ProjectSize.MAJOR, Specialization.WEBSITE, Theme.MUSIC, AgeAudience.EVERYONE, GenderAudience.MALE, new List<Technology>(){websiteTechs[0], websiteTechs[1]}));
		demanded.Add(new DemandedProduct(ProjectSize.MAJOR, Specialization.GAME, Theme.LOVE, AgeAudience.EVERYONE, GenderAudience.FEMALE, new List<Technology>(){gameTechs[3]}));
		demanded.Add(new DemandedProduct(ProjectSize.MAJOR, Specialization.WEBSITE, Theme.LOVE, AgeAudience.ADULT, GenderAudience.FEMALE, new List<Technology>(){websiteTechs[3], websiteTechs[4]}));
		demanded.Add(new DemandedProduct(ProjectSize.MAJOR, Specialization.GAME, Theme.SCHOOL, AgeAudience.ADULT, GenderAudience.MALE, new List<Technology>(){gameTechs[3]}));
	}
}

public class Product
{
	public string productName;
	public double productProfitRatio;
	public int productCost;

	public ProjectSize projectSize;
	public Specialization specialization;
	public Theme theme;
	public AgeAudience ageAudience;
	public GenderAudience genderAudience;
	public PropertyRight propertyRight;
	
	public int dividendsCount;
	public int income;

	public Product(string productName, 
				double productProfitRatio, 
				int productCost, 
				ProjectSize projectSize, 
				Theme theme,
				Specialization specialization, 
				AgeAudience ageAudience, 
				GenderAudience genderAudience, 
				PropertyRight propertyRight,
				int dividendsCount,
				int income)
	{
		this.productName = productName;
		this.productProfitRatio = productProfitRatio;
		this.productCost = productCost;
	
		this.projectSize = projectSize;
		this.specialization = specialization;
		this.theme = theme;
		this.ageAudience = ageAudience;
		this.genderAudience = genderAudience;
		this.propertyRight = propertyRight;
		
		this.dividendsCount = dividendsCount;
		this.income = income;
	}
}

public class DemandedProduct
{
	public ProjectSize projectSize;
	public Specialization specialization;
	public Theme theme;
	public AgeAudience ageAudience;
	public GenderAudience genderAudience;
	public List<Technology> usedTechs;

	public DemandedProduct(ProjectSize projectSize, Specialization specialization, Theme theme, AgeAudience ageAudience, GenderAudience genderAudience, List<Technology> usedTechs)
	{
		this.projectSize = projectSize;
		this.specialization = specialization;
		this.theme = theme;
		this.ageAudience = ageAudience;
		this.genderAudience = genderAudience;
		this.usedTechs = usedTechs;
	}
}

public class Technology
{
	public string title;
	public Category category;
	public int productionCost;
	public int moneyCost;
	public int experienceCost;
	public TechState state;
	public List<int> next;

	public Technology(string title, Category category, int productionCost, int moneyCost, int experienceCost, TechState state, List<int> next)
	{
		this.title = title;
		this.category = category;
		this.productionCost = productionCost;
		this.moneyCost = moneyCost;
		this.experienceCost = experienceCost;
		this.state = state;
		this.next = next;
	}
}

public class Credit
{
	public CREDIT_SIZE creditSize;
	public bool isActive;
	public int loanAmount;
	public int monthlyPayment;
	public int paymentsLeft;
	
	public Credit(CREDIT_SIZE creditSize, bool isActive, int loanAmount, int monthlyPayment, int paymentsLeft)
	{
		this.creditSize = creditSize;
		this.isActive = isActive;
		this.loanAmount = loanAmount;
		this.monthlyPayment = monthlyPayment;
		this.paymentsLeft = paymentsLeft;
	}
}

public enum Category
{
	GAME_ENGINE,
	//Технологии
	GRAPHIC,
	AI,
	TECHNICAL_FEATURES,
	//Дизайн
	WORLD_DESIGN,
	DIALOGS,
	SOUND,
	//Геймдизайн
	PLOT_QUESTS,
	GAMEPLAY,
	LEVEL_DESIGN,
	
	WEBSITE,
	FONTS,
	STRUCTURE,
	DESIGN,
	TEXT,
	INTERACT,
	MARKETING,
	SITE_FEATURES,
	TECHNOLOGIES
}

public enum TechState
{
	RESEARCHED,
	AVAILABLE,
	CLOSED
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

public enum Theme
{
	SPORT,
	MUSIC,
	LOVE,
	FASHION,
	SCHOOL,
	SCIENCE,
	SPACE,
	WEATHER
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

public enum CREDIT_SIZE
{
	SMALL,
	MEDIUM,
	BIG
}
