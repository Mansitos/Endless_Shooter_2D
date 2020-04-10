using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script del GameManager.
 * Il GameManager, con i suoi vari moduli (Childs)
 */

public class GameManager : MonoBehaviour
{
    private bool paused;  // bool value for paused or unpaused game
    [SerializeField] GameObject Player;          // Player Instance
    [SerializeField] GameObject Station;         // Station Instance
    [SerializeField] GameObject PauseMenu;       // PauseMenu UI Instance
    [SerializeField] GameObject UpgradeMenuUI;   // UpgradeMenuUI Instance
    [SerializeField] GameObject NextWaveUI;      // NextWaveUI Instance
    [SerializeField] GameObject HealsManager;    // HealsManager Instance

    [SerializeField] GameObject[] UIs_ToHideOnPause;
    private bool[] UIs_SavedStatus;

    void Start()
    {
        UIs_SavedStatus = new bool[UIs_ToHideOnPause.Length];
    }

    void Update()
    {
        checkForPauseRequest();
    }

    // Implementa il meccanismo di Pausa, il parametro bool value indica se il gioco deve o non deve essere pausato.
    void PauseMechanic(bool value)
    {
        if (value)
        {
            Time.timeScale = 0;
            Player.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            Player.SetActive(true);
        }
    }

    bool isGamePause()
    {
        return paused;
    }

    // Esegue un check su KeyCode.Escape per vedere se lo user ha richiesto un pause/unpause
    void checkForPauseRequest()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // IF 
            if(paused == false)
            {
                paused = true;
                for(int i = 0; i< UIs_ToHideOnPause.Length; i++)
                {
                    UIs_SavedStatus[i] = UIs_ToHideOnPause[i].activeSelf;
                    UIs_ToHideOnPause[i].SetActive(false);
                }

                PauseMenu.SetActive(true);
                Player.SetActive(false);

            }
            else if(paused == true)
            {
                paused = false;
                for (int i = 0; i < UIs_ToHideOnPause.Length; i++)
                {
                    UIs_ToHideOnPause[i].SetActive(UIs_SavedStatus[i]);
                }
                PauseMenu.SetActive(false);
                Player.SetActive(true);
            }
        }
    }

    public void EnableDisableUpgradeMenu(bool value)
    {
        UpgradeMenuUI.SetActive(value);
        NextWaveUI.SetActive(!value);
        Player.SetActive(!value);
        if(value == true)
        {
            HealsManager.GetComponent<HealsManager>().updateVariables();
            HealsManager.GetComponent<HealsManager>().updateButtonsActive();
        }
        
    }

    public GameObject getPlayerInstance()
    {
        return Player;
    }

    public GameObject getStationInstance()
    {
        return Station;
    }


}
