using UnityEngine;
using System.Collections;

public class BackgroundMusicControllerScript : MonoBehaviour
{

    private static BackgroundMusicControllerScript instance = null;
    public static BackgroundMusicControllerScript Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}