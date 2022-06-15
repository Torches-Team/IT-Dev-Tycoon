using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AnalyticsController : MonoBehaviour
{
	public MainSceneController mainSceneController;
	public List<DemandedProduct> fullListOfDemandedProducts;
	public List<DemandedProduct> demandedProducts;
	public List<Technology> gameTechs;
	public List<Technology> websiteTechs;
	public List<Button> buttons;
	public TMPro.TextMeshProUGUI descriptionText;
	public Dictionary<ProjectSize, string> ProjectSizeDict;
	public Dictionary<Specialization, string> SpecializationDict;
	public Dictionary<Theme, string> ThemeDict;
	public Dictionary<AgeAudience, string> AgeAudienceDict;
	public Dictionary<GenderAudience, string> GenderAudienceDict;
	public DemandedProduct newProduct;
	public int activeId;
	
	public void Awake()
	{
		fullListOfDemandedProducts = new List<DemandedProduct>();
		demandedProducts = new List<DemandedProduct>();
		
		gameTechs = GlobalController.Instance.gameTechs;
		websiteTechs = GlobalController.Instance.websiteTechs;

		ProjectSizeDict = new Dictionary<ProjectSize, string>();
		SpecializationDict = new Dictionary<Specialization, string>();
		ThemeDict = new Dictionary<Theme, string>();
		AgeAudienceDict = new Dictionary<AgeAudience, string>();
		GenderAudienceDict = new Dictionary<GenderAudience, string>();

		ProjectSizeDict.Add(ProjectSize.MINOR, "Маленький");
		ProjectSizeDict.Add(ProjectSize.MAJOR, "Большой");
		
		SpecializationDict.Add(Specialization.GAME, "Игра");
		SpecializationDict.Add(Specialization.WEBSITE, "Веб-сайт");
		
		ThemeDict.Add(Theme.SPORT, "Спорт");
		ThemeDict.Add(Theme.MUSIC, "Музыка");
		ThemeDict.Add(Theme.LOVE, "Любовь");
		ThemeDict.Add(Theme.FASHION, "Мода");
		ThemeDict.Add(Theme.SCHOOL, "Школа");
		ThemeDict.Add(Theme.SCIENCE, "Наука");
		ThemeDict.Add(Theme.SPACE, "Космос");
		ThemeDict.Add(Theme.WEATHER, "Погода");
		
		AgeAudienceDict.Add(AgeAudience.CHILDREN, "Дети");
		AgeAudienceDict.Add(AgeAudience.EVERYONE, "Все");
		AgeAudienceDict.Add(AgeAudience.ADULT, "Взрослые");
		
		GenderAudienceDict.Add(GenderAudience.MALE, "Мужчины");
		GenderAudienceDict.Add(GenderAudience.EVERYONE, "Все");
		GenderAudienceDict.Add(GenderAudience.FEMALE, "Женщины");
	}
	
    void Start()
    {
		buttons[0].onClick.AddListener(() => ShowAnotherProduct(-1));
		buttons[1].onClick.AddListener(() => CheckAllRequiredTechsResearched());
		buttons[2].onClick.AddListener(() => ShowAnotherProduct(1));
		
        demandedProducts = GetNewDemandedProducts();
		activeId = 2;
		newProduct = demandedProducts[activeId];

		ShowAnotherProduct(0);
    }

    void Update()
    {
        
    }
	
	public void ShowAnotherProduct(int delta)
	{
		activeId += delta;
		descriptionText.text = ParseProductToText(demandedProducts[activeId]);
		
		if (activeId == 0) buttons[0].interactable = false;
		else buttons[0].interactable = true;
		
		if (activeId == demandedProducts.Count - 1) buttons[2].interactable = false;
		else buttons[2].interactable = true;
		
		newProduct = demandedProducts[activeId];
	}
	
	string ParseProductToText(DemandedProduct product)
	{
		var str = 
		" -Размер проекта: " + ProjectSizeDict[product.projectSize] + 
		"\n -Специализация: " + SpecializationDict[product.specialization] + 
		"\n -Тематика: " + ThemeDict[product.theme] +
		"\n -Возраст: " + AgeAudienceDict[product.ageAudience] + 
		"\n -Пол: " + GenderAudienceDict[product.genderAudience] +
		"\n -Технологии: \n";
		var techsStr = "";
		var flag = true;
		foreach(var tech in product.usedTechs)
		{
			techsStr += flag ? " " : ", ";
			if(flag) flag = false;
			techsStr += tech.title;
		}
		return String.Concat(str + techsStr);
	}
	
	public void GetNewProducts()
	{
		demandedProducts = GetNewDemandedProducts();
	}
	
	public void CheckAllRequiredTechsResearched()
	{
		foreach(var tech in newProduct.usedTechs)
		{
			if(!GlobalController.Instance.researchedTechs.Contains(tech))
			{
				return;
			}
		}
		mainSceneController.SetDemandedProduct(newProduct);
	}
	
	public List<DemandedProduct> GetAvailableProductsList()
	{
		List<DemandedProduct> availableTechnology = new List<DemandedProduct>();
		
		if(GlobalController.Instance.gameLevel > 1 || GlobalController.Instance.websiteLevel > 1)
		{
			if(GlobalController.Instance.gameLevel > 2 || GlobalController.Instance.websiteLevel > 2)
			{
				if(GlobalController.Instance.gameLevel > 3 || GlobalController.Instance.websiteLevel > 3)
				{
					foreach(var product in GlobalController.Instance.demanded)
					{
						if(product.level == 4) availableTechnology.Add(product);
					}
				}
				
				foreach(var product in GlobalController.Instance.demanded)
				{
					if(product.level == 3) availableTechnology.Add(product);
				}
			}
			
			foreach(var product in GlobalController.Instance.demanded)
			{
				if(product.level == 2) availableTechnology.Add(product);
			}
		}
		
		foreach(var product in GlobalController.Instance.demanded)
		{
			if(product.level == 1) availableTechnology.Add(product);
		}
		return availableTechnology;
	}
	
	List<DemandedProduct> GetNewDemandedProducts()
	{
		var demandedProducts = GetAvailableProductsList();
		var takenProducts = new List<DemandedProduct>();
		var newDemandedProducts = new List<DemandedProduct>();
		var randomProduct = demandedProducts[new System.Random().Next(0, demandedProducts.Count)];
		for (int i = 0; i < 5; i++)
		{
			while(takenProducts.Contains(randomProduct)) randomProduct = demandedProducts[new System.Random().Next(0, demandedProducts.Count)];
			newDemandedProducts.Add(randomProduct);
			takenProducts.Add(randomProduct);
		}
		return newDemandedProducts;
	}
}
