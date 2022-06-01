using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TreeController : MonoBehaviour
{		
	//Основные элементы
	public Button researchButton;
	public List<Button> buttons;
	public List<Technology> techs;
	
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
		buttons[10].onClick.AddListener(() => PressButton(10));
		buttons[11].onClick.AddListener(() => PressButton(11));
		buttons[12].onClick.AddListener(() => PressButton(12));
		buttons[13].onClick.AddListener(() => PressButton(13));
		buttons[14].onClick.AddListener(() => PressButton(14));
		buttons[15].onClick.AddListener(() => PressButton(15));
		buttons[16].onClick.AddListener(() => PressButton(16));
		buttons[17].onClick.AddListener(() => PressButton(17));
		buttons[18].onClick.AddListener(() => PressButton(18));
		buttons[19].onClick.AddListener(() => PressButton(19));
		buttons[20].onClick.AddListener(() => PressButton(20));
		buttons[21].onClick.AddListener(() => PressButton(21));
		buttons[22].onClick.AddListener(() => PressButton(22));
		buttons[23].onClick.AddListener(() => PressButton(23));
		buttons[24].onClick.AddListener(() => PressButton(24));
		buttons[25].onClick.AddListener(() => PressButton(25));
		buttons[26].onClick.AddListener(() => PressButton(26));
		
		techs = GlobalController.Instance.gameTechs;
		ResetTree();
		SetScore(0, 0);
	}
	
	public void Update()
	{

	}
	
	public void ChangeSpecializationToGame()
	{
		techs = GlobalController.Instance.gameTechs;
		ResetTree();
	}
	
	public void ChangeSpecializationToWebsite()
	{
		techs = GlobalController.Instance.websiteTechs;
		ResetTree();
	}
	
	public void ResetTree()
	{
		for(int i = 0; i < buttons.Count; i++)
		{
			var header = buttons[i].transform.Find("Header");
			var body = buttons[i].transform.Find("Panel");
			var title = header.transform.Find("Text (TMP)");
			var money = body.transform.Find("Text (TMP)");
			var experience = body.transform.Find("Text (TMP) (1)");
			
			title.GetComponent<TMPro.TextMeshProUGUI>().text = techs[i].title;
			money.GetComponent<TMPro.TextMeshProUGUI>().text = techs[i].moneyCost + "$";
			experience.GetComponent<TMPro.TextMeshProUGUI>().text = techs[i].experienceCost + " ОО";
		}
		CheckButtons();
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
			else buttons[i].interactable = true;
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
		if (currentTech.state == TechState.AVAILABLE && currentTech.moneyCost < GlobalController.Instance.moneyScore && currentTech.experienceCost < GlobalController.Instance.experienceScore)
		{
			currentTech.state = TechState.RESEARCHED;
			GlobalController.Instance.researchedTechs.Add(currentTech);
			SetScore(currentTech.moneyCost, currentTech.experienceCost);
			CheckResearch();
			researchButton.interactable = false;
		}
	}
	
	void SetScore(int moneyCost, int experienceCost)
	{
		GlobalController.Instance.moneyScore -= moneyCost;
		GlobalController.Instance.experienceScore -= experienceCost;
	}
	
    public void BackToMain()
	{
		SceneManager.LoadScene("MainScene");
	}
}