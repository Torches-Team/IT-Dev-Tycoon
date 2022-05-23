using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TreeController : MonoBehaviour
{
	public GameObject moneyScoreText;
	public GameObject experienceScoreText;
	public Button researchButton;
	
	public List<Button> buttons;
	public List<Technology> list;
	
	public int moneyScore;
	public int experienceScore;
	public int year;
	public int month;
	public int week;
	
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
		
		list = new List<Technology>();
		list.Add(new Technology("First", "Your first tech", 1000, 100, TechState.RESEARCHED, null, new List<Technology>() {list[1]} )); //0
		list.Add(new Technology("Second", "Your second tech", 10000, 150, TechState.CLOSED, list[0], new List<Technology>() {list[2], list[3], list[4]} )); //1
		list.Add(new Technology("Third", "Your third tech", 25000, 200, TechState.CLOSED, list[1], new List<Technology>() {list[5], list[6]} )); //2
		list.Add(new Technology("Fourth", "Your fourth tech", 30000, 150, TechState.CLOSED, list[1], null)); //3 
		list.Add(new Technology("Fifth", "Your fifth tech", 35000, 100, TechState.CLOSED, list[1], new List<Technology>() {list[7]} )); //4
		list.Add(new Technology("Sixth", "Your sixth tech", 50000, 350, TechState.CLOSED, list[2], new List<Technology>() {list[9]} )); //5
		list.Add(new Technology("Seventh", "Your seventh tech", 20000, 250, TechState.CLOSED, list[2], null)); //6
		list.Add(new Technology("Eights", "Your eights tech", 50000, 500, TechState.CLOSED, list[4], new List<Technology>() {list[8]} )); //7
		list.Add(new Technology("Nineth", "Your nineth tech", 10000, 100, TechState.CLOSED, list[7], null)); //8
		list.Add(new Technology("Nddineth", "Youdddr nineth tech", 100000, 1000, TechState.CLOSED, list[5], null)); //9
		
		CheckResearch();
	}
	
	public void Update()
	{
		CheckResearch();
	}
	
	public void PressButton(int id)
	{
		currentTech = list[id];
		if (currentTech.state == TechState.AVAILABLE)
		{
			researchButton.interactable = true;
		}
	}
	
	public void CheckResearch(Technology tech)
	{
		var nexts = tech.next;
		foreach(Technology e in nexts)
		{
			e.state = TechState.AVAILABLE;
		}
		/*for(int i = buttons.Count - 1; i > 0; i--)
		{
			if(list[i].previous != null && list[i].previous.state == TechState.RESEARCHED)
			{
				var nexts = list[i].next;
				buttons[i].interactable = true;
				list[i].state = TechState.RESEARCHED;
				foreach(var e in nexts)
				{
					if(e.state == TechState.CLOSED)
						list[i].state = TechState.AVAILABLE;
				}
			}
			else if(list[i].previous != null && list[i].previous.state != TechState.RESEARCHED) 
			{
				buttons[i].interactable = false;
				list[i].state = TechState.CLOSED;
			}
		}*/
	}
	
	public void ResearchNewTech()
	{
		if (currentTech.state == TechState.AVAILABLE && currentTech.moneyCost < moneyScore && currentTech.experienceCost < experienceScore)
		{
			currentTech.state = TechState.RESEARCHED;
			SetScore(-currentTech.moneyCost, -currentTech.experienceCost);
			researchButton.interactable = false;
		}
	}
	
	void SetScore(int moneyCost, int experienceCost)
	{
		moneyScore += moneyCost;
		experienceScore += experienceCost;
		moneyScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = moneyScore + "$";
		experienceScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = experienceScore + " ОО";
	}
	
    public void BackToMain()
	{
		SceneManager.LoadScene("MainScene");
	}
}

public class Technology
{
	public string title;
	public string description;
	public int moneyCost;
	public int experienceCost;
	public TechState state;
	public Technology previous;
	public List<Technology> next;

	public Technology(string title, string description, int moneyCost, int experienceCost, TechState state, Technology previous, List<Technology> next)
	{
		this.title = title;
		this.description = description;
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