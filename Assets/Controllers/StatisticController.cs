using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StatisticController : MonoBehaviour
{
	public GlobalController globalController;
	MainSceneController mainSceneController;
	public GameObject productsList;
	public Transform parentObject;
	RectTransform rectTransform;
	public List<Product> products;
	public TextMeshProUGUI companyName;
	
	void Awake()
	{

	}
	
    void Start()
    {
		companyName.text = GlobalController.Instance.companyName;
		products = GlobalController.Instance.products;
        rectTransform = productsList.GetComponent<RectTransform>();
		for(int i = 0; i < products.Count; i++)
		{
			AddNewProduct(i);
		}
    }

    void Update()
    {
        
    }
	
	public void AddNewProduct(int i)
	{
		ChangeListSize();
		GameObject childObject = new GameObject("Button");
		childObject.transform.SetParent(parentObject);
		childObject.AddComponent<RectTransform>();
		childObject.AddComponent<CanvasRenderer>();
		childObject.AddComponent<Image>().color = new Color32(135, 80, 60, 255);
		var button = childObject.AddComponent<Button>();
		var palette = button.colors;
		palette.highlightedColor = new Color32(200, 200, 200, 255);
		button.colors = palette;
		button.onClick.AddListener(() => ShowDescription(i));
		//palette.highlightedColor = new Color32(10, 10, 10, 255);
		AddTextToButton(childObject.transform, i);	
		//childObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors();
	}
	
	void AddTextToButton(Transform buttonObject, int i)
	{
		GameObject text = new GameObject("Text (TMP)");
		text.transform.SetParent(buttonObject, false);
		
		var rt = text.AddComponent<RectTransform>();
		rt.anchorMin = new Vector2(0, 0);
		rt.anchorMax = new Vector2(1, 1);
		rt.pivot = new Vector2(0.5f, 0.5f);
		SetLeft(rt,0);
		SetRight(rt,0);
		SetTop(rt,0);
		SetBottom(rt,0);
		text.AddComponent<CanvasRenderer>();
		var t = text.AddComponent<TMPro.TextMeshProUGUI>();
		t.text = products[i].productName;
		t.overflowMode = TextOverflowModes.Ellipsis;
		t.fontSize = 60;
		t.color = new Color32(50,50,50,255);
		t.alignment = TextAlignmentOptions.Center;
	}
	
	public void ShowDescription(int i)
	{
		ShowStatistic.Instance.ShowText(products[i]);
	}
	
	public void SellProduct()
	{
		
	}
	
	public void CloseProduct()
	{
		
	}
	
	void ChangeListSize()
	{
		var prevHeight = rectTransform.rect.height;
		rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, prevHeight + 175);
	}
	
	public void BackToMain()
	{
		SceneManager.LoadScene("MainScene");
	}
	
	public void SetLeft(RectTransform rt, float left)
    {
         rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }
 
     public void SetRight(RectTransform rt, float right)
    {
         rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }
 
     public void SetTop(RectTransform rt, float top)
    {
         rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }
 
     public void SetBottom(RectTransform rt, float bottom)
    {
         rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }
}
