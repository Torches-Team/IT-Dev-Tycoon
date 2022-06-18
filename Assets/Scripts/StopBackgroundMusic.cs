using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBackgroundMusic : MonoBehaviour
{
    public void DestroyMusicObject()
    {
        Destroy(GameObject.Find("BackgroundMusic"));
    }
}
