using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    void Start()
    {    
    }

    void Update()
    {   
    }

    // carica la scena passata come parametro: attenzione alla correttezza della stringa
    public void loadScenebyName(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    
    public void quitApplication()
    {
        Debug.Log("Quitting application...");
        Application.Quit();
    }
}
