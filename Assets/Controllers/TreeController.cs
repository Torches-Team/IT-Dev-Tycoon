using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TreeController : MonoBehaviour
{
	//Общие переменные
	public int moneyScore;
	public int experienceScore;
	public int year;
	public int month;
	public int week;
	
	//Текст интерфейса
	public TMPro.TextMeshProUGUI dateText;
	public TMPro.TextMeshProUGUI moneyScoreText;
	public TMPro.TextMeshProUGUI experienceScoreText;
	public List<TMPro.TextMeshProUGUI> buttonTextList;
	public List<TMPro.TextMeshProUGUI> moneyScoreList;
	public List<TMPro.TextMeshProUGUI> experienceScoreList;
	
	//Основные элементы
	public Button researchButton;
	public List<Button> buttons;
	public List<Technology> techs;
	public List<Technology> researchedTechs;
	
	//Переменные элементы
	Technology currentTech;
	
	public void Start()
	{	
		buttons[0].onClick.AddListener(() => PressButton(0));
		buttons[1].onClick.AddListener(() => PressButton(1));
		buttons[2].onClick.AddListener(() => PressButton(2));
		buttons[3].onClick.AddListener(() => PressButton(3));
		buttons[4].onClick.AddListener(() => PressButton(4));
		buttons[5].onClick.AddListener(() => PressButton(5));
		buttons[6].onClick.AddListener(() => PressButton(6));
		buttons[7].onClick.AddListener(() => PressButton(7));
		buttons[8].onClick.AddListener(() => PressButton(8));
		buttons[9].onClick.AddListener(() => PressButton(9));
		
		techs = new List<Technology>();
		researchedTechs = new List<Technology>();
		techs.Add(new Technology("First", 1000, 100, TechState.AVAILABLE, null, new List<int>() {1} )); //0
		techs.Add(new Technology("Second", 10000, 150, TechState.CLOSED, techs[0], new List<int>() {2,3,4} )); //1
		techs.Add(new Technology("Third", 25000, 200, TechState.CLOSED, techs[1], new List<int>() {5,6} )); //2
		techs.Add(new Technology("Fourth", 30000, 150, TechState.CLOSED, techs[1], null)); //3 
		techs.Add(new Technology("Fifth", 35000, 100, TechState.CLOSED, techs[1], new List<int>() {7} )); //4
		techs.Add(new Technology("Sixth", 50000, 350, TechState.CLOSED, techs[2], new List<int>() {9} )); //5
		techs.Add(new Technology("Seventh", 20000, 250, TechState.CLOSED, techs[2], null)); //6
		techs.Add(new Technology("Eights", 50000, 500, TechState.CLOSED, techs[4], new List<int>() {8} )); //7
		techs.Add(new Technology("Nineth", 10000, 100, TechState.CLOSED, techs[7], null)); //8
		techs.Add(new Technology("Nddineth", 100000, 1000, TechState.CLOSED, techs[5], null)); //9
		
		for(int i = 0; i < buttons.Count; i++)
		{
			buttonTextList[i].GetComponent<TMPro.TextMeshProUGUI>().text = techs[i].title;
			moneyScoreList[i].GetComponent<TMPro.TextMeshProUGUI>().text = techs[i].moneyCost + "$";
			experienceScoreList[i].GetComponent<TMPro.TextMeshProUGUI>().text = techs[i].experienceCost + " ОО";
		}
		CheckButtons();
		SetScore(0, 0);
		SetDate();
	}
	
	public void Update()
	{

	}
	
	public void CheckButtons()
	{
		for(int i = 0; i < techs.Count; i++)
		{
			if (techs[i].state == TechState.CLOSED)
			{
				buttons[i].interactable = false;
			}
			else if (techs[i].state == TechState.AVAILABLE)
			{
				buttons[i].interactable = true;
			}
		}
	}
	
	public void PressButton(int id)
	{
		currentTech = techs[id];
		if (currentTech.state == TechState.AVAILABLE)
		{
			researchButton.interactable = true;
		} else researchButton.interactable = false;
	}
	
	public void CheckResearch()
	{
		var nexts = currentTech.next;
		if(nexts != null)
		{
			foreach(int e in nexts)
			{
				techs[e].state = TechState.AVAILABLE;
			}
		}
		CheckButtons();
	}
	
	public void ResearchNewTech()
	{
		if (currentTech.state == TechState.AVAILABLE && currentTech.moneyCost < moneyScore && currentTech.experienceCost < experienceScore)
		{
			currentTech.state = TechState.RESEARCHED;
			SetScore(currentTech.moneyCost, currentTech.experienceCost);
			researchedTechs.Add(currentTech);
			researchButton.interactable = false;
			CheckResearch();
		}
	}
	
	void SetScore(int moneyCost, int experienceCost)
	{
		moneyScore -= moneyCost;
		experienceScore -= experienceCost;
		moneyScoreText.text = moneyScore + "$";
		experienceScoreText.text = experienceScore + " ОО";
	}
	
	void SetDate()
	{
		dateText.text = "Н: " + week + " М: " + month + " Г: " + year; 
	}
	
    public void BackToMain()
	{
		SceneManager.LoadScene("MainScene");
	}
}

public class Technology
{
	public string title;
	public int moneyCost;
	public int experienceCost;
	public TechState state;
	public Technology previous;
	public List<int> next;

	public Technology(string title, int moneyCost, int experienceCost, TechState state, Technology previous, List<int> next)
	{
		this.title = title;
		this.moneyCost = moneyCost;
		this.experienceCost = experienceCost;
		this.state = state;
		this.previous = previous;
		this.next = next;
	}
}

public enum TechState
{
	RESEARCHED,
	AVAILABLE,
	CLOSED
}