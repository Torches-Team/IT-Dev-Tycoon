using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TreeController : MonoBehaviour
{
	public List<Button> buttons;
	public List<Technology> list;
	
	public void Start()
	{	
		buttons[0].onClick.AddListener(() => ResearchNewTech(0));
		buttons[1].onClick.AddListener(() => ResearchNewTech(1));
		buttons[2].onClick.AddListener(() => ResearchNewTech(2));
		buttons[4].onClick.AddListener(() => ResearchNewTech(4));
		buttons[5].onClick.AddListener(() => ResearchNewTech(5));
		buttons[7].onClick.AddListener(() => ResearchNewTech(7));
		buttons[8].onClick.AddListener(() => ResearchNewTech(8));


		
		list = new List<Technology>();
		list.Add(new Technology("First", "Your first tech", 100, false, null)); //0
		list.Add(new Technology("Second", "Your second tech", 150, false, list[0])); //1
		list.Add(new Technology("Third", "Your third tech", 200, false, list[1])); //2
		list.Add(new Technology("Fourth", "Your fourth tech", 150, false, list[1])); //3 
		list.Add(new Technology("Fifth", "Your fifth tech", 100, false, list[1])); //4
		list.Add(new Technology("Sixth", "Your sixth tech", 350, false, list[2])); //5
		list.Add(new Technology("Seventh", "Your seventh tech", 250, false, list[2])); //6
		list.Add(new Technology("Eights", "Your eights tech", 500, false, list[4])); //7
		list.Add(new Technology("Nineth", "Your nineth tech", 100, false, list[7])); //8
		list.Add(new Technology("Nddineth", "Youdddr nineth tech", 1000, false, list[5])); //9
	}
	
	public void Update()
	{
		CheckResearch();
	}
	
	void CheckResearch()
	{
		for(int i = buttons.Count - 1; i >= 0; i--)
		{
			if(list[i].previous != null && !list[i].previous.isResearched) buttons[i].interactable = false;
			if(list[i].previous != null && list[i].previous.isResearched) buttons[i].interactable = true;
		}
	}
	
	void ResearchNewTech(int id)
	{
		list[id].isResearched = true;
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
	public int experienceCost;
	public bool isResearched;
	public Technology previous;

	public Technology(string title, string description, int experienceCost, bool isResearched, Technology previous)
	{
		this.title = title;
		this.description = description;
		this.experienceCost = experienceCost;
		this.isResearched = isResearched;
		this.previous = previous;
	}
}