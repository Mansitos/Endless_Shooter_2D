using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScenebyName(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    
    public void quitApplication()
    {
        Debug.Log("Quit app");
        Application.Quit();
    }
}
