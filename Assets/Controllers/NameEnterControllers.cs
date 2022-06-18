using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NameEnterControllers : MonoBehaviour
{
    private string sceneToOpen = "MainScene";

    public void OpenScene()
    {
        SceneManager.LoadScene(sceneToOpen);
    }
}
