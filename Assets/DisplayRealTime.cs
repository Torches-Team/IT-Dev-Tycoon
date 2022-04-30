using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DisplayRealTime : MonoBehaviour
{
    public TMP_Text realTimeText;

    // Update is called once per frame
    void Update()
    {
        realTimeText.text = "Now: "+DateTime.Now.ToString(); 
    }
}
