using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowStatistic : MonoBehaviour
{
	public static ShowStatistic Instance;
	public List<TextMeshProUGUI> textFields;
	public string specialization;
	public string age;
	public string gender;
	public int income;
	public int productCost;
	public int gainedExp;
	
    void Awake()
    {
		if(Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
    }

    void Update()
    {
        
    }
	
	public void ShowText(Product product)
	{
		if (product.specialization == Specialization.WEBSITE) specialization = "Вебсайт";
		else specialization = "Игра";
		
		if (product.ageAudience == AgeAudience.CHILDREN) age = "Дети";
		else if (product.ageAudience == AgeAudience.ADULT) age = "Взрослые";
		else age = "Все";
		
		if (product.genderAudience == GenderAudience.MALE) gender = "Мужчины";
		else if (product.genderAudience == GenderAudience.FEMALE) gender = "Женщины";
		else gender = "Все";
		
		income = product.income;
		productCost = product.productCost;
		gainedExp = product.gainedExp;
		
		textFields[0].text = product.productName;
		textFields[1].text = specialization;
		textFields[2].text = age;
		textFields[3].text = gender;
		textFields[4].text = income + "";
		textFields[5].text = productCost + "";
		textFields[6].text = gainedExp + "";
	}
}
