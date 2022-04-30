using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenuController : MonoBehaviour
{
    public GameObject PanelToShowOnRMB;
    public GameObject someObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowContextMenu()
    {
	    PanelToShowOnRMB.SetActive(true);
	    PanelToShowOnRMB.transform.position = Input.mousePosition;	
    }
}
