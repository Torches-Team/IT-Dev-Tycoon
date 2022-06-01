using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AnalytiscController : MonoBehaviour
{
	public List<DemandedProduct> fullListOfDemandedProducts;
	public List<DemandedProduct> demandedProducts;
	public List<Button> buttons;
	public TMPro.TextMeshProUGUI descriptionText;
	public Dictionary<ProjectSize, string> ProjectSizeDict;
	public Dictionary<Specialization, string> SpecializationDict;
	public Dictionary<AgeAudience, string> AgeAudienceDict;
	public Dictionary<GenderAudience, string> GenderAudienceDict;
	public int activeId;
	
	void Awake()
	{
		demandedProducts = new List<DemandedProduct>();
		fullListOfDemandedProducts = new List<DemandedProduct>();

		ProjectSizeDict = new Dictionary<ProjectSize, string>();
		SpecializationDict = new Dictionary<Specialization, string>();
		AgeAudienceDict = new Dictionary<AgeAudience, string>();
		GenderAudienceDict = new Dictionary<GenderAudience, string>();
		
		fullListOfDemandedProducts.Add(new DemandedProduct(ProjectSize.MINOR, Specialization.WEBSITE, AgeAudience.CHILDREN, GenderAudience.MALE));
		fullListOfDemandedProducts.Add(new DemandedProduct(ProjectSize.MINOR, Specialization.GAME, AgeAudience.CHILDREN, GenderAudience.MALE));
		fullListOfDemandedProducts.Add(new DemandedProduct(ProjectSize.MINOR, Specialization.WEBSITE, AgeAudience.EVERYONE, GenderAudience.FEMALE));
		fullListOfDemandedProducts.Add(new DemandedProduct(ProjectSize.MINOR, Specialization.GAME, AgeAudience.EVERYONE, GenderAudience.FEMALE));
		fullListOfDemandedProducts.Add(new DemandedProduct(ProjectSize.MINOR, Specialization.WEBSITE, AgeAudience.ADULT, GenderAudience.MALE));
		fullListOfDemandedProducts.Add(new DemandedProduct(ProjectSize.MINOR, Specialization.GAME, AgeAudience.ADULT, GenderAudience.FEMALE));
		fullListOfDemandedProducts.Add(new DemandedProduct(ProjectSize.MAJOR, Specialization.WEBSITE, AgeAudience.EVERYONE, GenderAudience.MALE));
		fullListOfDemandedProducts.Add(new DemandedProduct(ProjectSize.MAJOR, Specialization.GAME, AgeAudience.EVERYONE, GenderAudience.FEMALE));
		fullListOfDemandedProducts.Add(new DemandedProduct(ProjectSize.MAJOR, Specialization.WEBSITE, AgeAudience.ADULT, GenderAudience.FEMALE));
		fullListOfDemandedProducts.Add(new DemandedProduct(ProjectSize.MAJOR, Specialization.GAME, AgeAudience.ADULT, GenderAudience.MALE));

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
		buttons[2].onClick.AddListener(() => ShowAnotherProduct(1));
		
        demandedProducts = GetNewDemandedProducts();
		activeId = 1;

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
	}
	
	string ParseProductToText(DemandedProduct product)
	{
		var str = 
		"Размер проекта: " + ProjectSizeDict[product.projectSize] + 
		"\n Специализация: " + SpecializationDict[product.specialization] + 
		"\n Возраст: " + AgeAudienceDict[product.ageAudience] + 
		"\n Пол: " + GenderAudienceDict[product.genderAudience];
		return str;
	}
	
	public void GetNewProducts()
	{
		demandedProducts = GetNewDemandedProducts();
	}
	
	List<DemandedProduct> GetNewDemandedProducts()
	{
		var takenProducts = new List<DemandedProduct>();
		var newDemandedProducts = new List<DemandedProduct>();
		
		var randomProduct = fullListOfDemandedProducts[new System.Random().Next(0, fullListOfDemandedProducts.Count)];
		for (int i = 0; i < 3; i++)
		{
			while(takenProducts.Contains(randomProduct)) randomProduct = fullListOfDemandedProducts[new System.Random().Next(0, fullListOfDemandedProducts.Count)];
			newDemandedProducts.Add(randomProduct);
			takenProducts.Add(randomProduct);
		}
		return newDemandedProducts;
	}
}

/*public class DemandedProduct
{
	public ProjectSize projectSize;
	public Specialization specialization;
	public AgeAudience ageAudience;
	public GenderAudience genderAudience;

	public DemandedProduct(ProjectSize projectSize, Specialization specialization, AgeAudience ageAudience, GenderAudience genderAudience)
	{
		this.projectSize = projectSize;
		this.specialization = specialization;
		this.ageAudience = ageAudience;
		this.genderAudience = genderAudience;
	}
}*/
