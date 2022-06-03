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
		AgeAudienceDict = new Dictionary<AgeAudience, string>();
		GenderAudienceDict = new Dictionary<GenderAudience, string>();

		ProjectSizeDict.Add(ProjectSize.MINOR, "Маленький");
		ProjectSizeDict.Add(ProjectSize.MAJOR, "Большой");
		
		SpecializationDict.Add(Specialization.GAME, "Игра");
		SpecializationDict.Add(Specialization.WEBSITE, "Веб-сайт");
		
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
		buttons[1].onClick.AddListener(() => mainSceneController.CreateDemandedProduct(newProduct));
		buttons[2].onClick.AddListener(() => ShowAnotherProduct(1));
		
        demandedProducts = GetNewDemandedProducts();
		activeId = 1;
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
	
	List<DemandedProduct> GetNewDemandedProducts()
	{
		Debug.Log("1");
		var takenProducts = new List<DemandedProduct>();
		var newDemandedProducts = new List<DemandedProduct>();
		Debug.Log("2");
		var randomProduct = GlobalController.Instance.demanded[new System.Random().Next(0, GlobalController.Instance.demanded.Count)];
		Debug.Log("3");
		for (int i = 0; i < 3; i++)
		{
			while(takenProducts.Contains(randomProduct)) randomProduct = GlobalController.Instance.demanded[new System.Random().Next(0, GlobalController.Instance.demanded.Count)];
			newDemandedProducts.Add(randomProduct);
			takenProducts.Add(randomProduct);
			Debug.Log("4");
		}
		return newDemandedProducts;
	}
}
