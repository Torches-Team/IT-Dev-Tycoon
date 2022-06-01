using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Object.FindObjectsOfType<DestroyOnLoad>().Length; i++)
		{
			if(Object.FindObjectsOfType<DestroyOnLoad>()[i] != this)
			{
				if(Object.FindObjectsOfType<DestroyOnLoad>()[i].name == gameObject.name)
				{
					Destroy(gameObject);
				}
			}
		}
		
		DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
