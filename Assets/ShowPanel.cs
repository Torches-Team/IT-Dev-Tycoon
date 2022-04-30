using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class ShowPanel : MonoBehaviour
{
    public GameObject PanelToClose;
    public GameObject PanelToShow;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PanelToClose.SetActive(false);
	    PanelToShow.SetActive(true);
        }
    }
}
