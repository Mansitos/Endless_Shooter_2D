using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script del GameManager.
 * Il GameManager, con i suoi vari moduli (Childs)
 */

public class GameManager : MonoBehaviour
{
    // VARIABILI PRINCIPALI //
    public GameObject Player;          // Player Instance
    public GameObject Station;         // Main Player Station Instance

    // sub-manager-modules
    private WavesManager wavesManager;
    private ScoreManager scoreManager;
    private UpgradesManager upgradesManager;
    private MainHUDManager mainHUDManager; 

    // variabili relative alla meccanica di pausa (tasto ESC)
    public GameObject PauseMenuUI;       // Istanza dell'UI di pausa da mostrare quando pause = true
    private bool paused;                 // indica se il gioco è o meno in pausa
    public GameObject[] UIs_ToHideOnPause;  // lista degli UI da nascondere durante la pausa
    private bool[] UIs_SavedStatus;         // array dove salvare lo stato degli UI prima di pausare. Se l'UI x è attivo, e pausiamo, esso verrà nascosto, ma quando torneremo in game, l'UI x andrà ripristinato allo stato precedente (che deve quindi essere salvato)

    void Start()
    {
        getSubManagerModulesIstances();
        initializeUIsSavedStatus();
    }

    void Update()
    {
        checkForPauseRequest();
    }

    // ottiene le istanze dei sotto-moduli di games manager: wavesmanager, scoremanager etc. etc.
    void getSubManagerModulesIstances()
    {
        wavesManager = GameObject.FindGameObjectWithTag("WavesManager").GetComponent<WavesManager>();
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        upgradesManager = GameObject.FindGameObjectWithTag("UpgradesManager").GetComponent<UpgradesManager>();
        mainHUDManager = GameObject.FindGameObjectWithTag("MainHUD").GetComponent<MainHUDManager>();
    }

    void initializeUIsSavedStatus()
    {
        UIs_SavedStatus = new bool[UIs_ToHideOnPause.Length];
    }

    // Implementa il meccanismo di pausa, il parametro "bool value" indica se il gioco deve o non deve essere pausato.
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

    // Ritorna lo stato del gioco, paused or unpaused
    bool isGamePause()
    {
        return paused;
    }

    // Viene chiamato ciclicamente su Update().
    // Esegue un check su (KeyCode.Escape) per controllare se lo user ha richiesto un pause/unpause
    void checkForPauseRequest()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   // se il player ha premuto ESC (pausa)
        {
            if(paused == false) // se non era in pausa....
            {
                paused = true;
                for(int i = 0; i< UIs_ToHideOnPause.Length; i++)
                {
                    UIs_SavedStatus[i] = UIs_ToHideOnPause[i].activeSelf;   // salva il vecchio stato degli UI
                    UIs_ToHideOnPause[i].SetActive(false);                  // nascondili tutti
                }

                // attiva il menu di pausa e nascondi il player.
                PauseMenuUI.SetActive(true);
                Player.SetActive(false);

            }
            else if(paused == true) // se era in pausa....
            {
                paused = false;
                for (int i = 0; i < UIs_ToHideOnPause.Length; i++)
                {
                    UIs_ToHideOnPause[i].SetActive(UIs_SavedStatus[i]);     // ripristina lo stato degli UI (se ad esempio prima di pausa c'era l'UI nextwave aperto, va riaperto)
                }

                // disabilità l'UI del menù pausa e ripristina il player.
                PauseMenuUI.SetActive(false);
                Player.SetActive(true);
            }
        }
    }

    // LIST OF GETTER
    public GameObject getPlayerInstance()
    {
        return Player;
    }

    public GameObject getStationInstance()
    {
        return Station;
    }

    public GameObject getPauseMenuUI()
    {
        return PauseMenuUI;
    }

    public WavesManager getWavesManager()
    {
        return wavesManager;
    }

    public ScoreManager getScoreManager()
    {
        return scoreManager;
    }

    public MainHUDManager getMainHUDManager()
    {
        return mainHUDManager;
    }
}
