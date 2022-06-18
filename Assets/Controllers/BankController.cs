using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BankController : MonoBehaviour
{
	public List<Button> buttons;
	public List<Credit> credits;
	
	public void Awake()
	{		

	}

    void Start()
    {
        buttons[0].onClick.AddListener(() => ApplyLoan(0));
		buttons[1].onClick.AddListener(() => ApplyLoan(1));
		buttons[2].onClick.AddListener(() => ApplyLoan(2));
		
		credits = GlobalController.Instance.credits;
    }

    void Update()
    {
        
    }
	
	public void ApplyLoan(int id)
	{
		var credit = credits[id];
		if(!credit.isActive)
		{
			credit.isActive = true;	
			SetScore(credit.loanAmount);
			buttons[id].interactable = false;
		}
		GlobalController.Instance.flag = true;
	}
	
	public void RepayLoan(int id)
	{
		var credit = credits[id];
		if(credit.isActive)
		{
			credit.isActive = false;
			credit.paymentsLeft = 12;
			buttons[id].interactable = true;
		}
	}
	
	public void Payment()
	{
		credits = GlobalController.Instance.credits;
		for(int i = 0; i < credits.Count; i++)
		{
			Debug.Log("1");
			var credit = credits[i];
			if(credit.isActive)
			{
				Debug.Log("2");
				SetScore(-credit.monthlyPayment);
				credit.paymentsLeft--;
				Debug.Log("3");
				if(credit.paymentsLeft <= 0) RepayLoan(i);
			}
		}
	}
	
	public void SetScore(int deltaMoney)
	{
		GlobalController.Instance.moneyScore += deltaMoney;
	}
}
