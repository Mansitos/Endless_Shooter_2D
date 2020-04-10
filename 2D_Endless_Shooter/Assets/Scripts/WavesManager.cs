using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Script di gestione dell' oggetto "WAVES MANAGER" che gestisce il flusso di ondate "WAVES" 
 */

public class WavesManager : MonoBehaviour {

    [SerializeField] float timeBetweenWaves = 20;   // Tempo tra la fine di una wave e l'inizio di quella successiva. default=20sec
    [SerializeField] GameObject[] waves;            // Array di wave in ordine di esecuzione.
    private int waveIndex = 0;                      // Wave raggiunta
    private GameObject actualWave;                  // Istanza Wave attuale.

    [SerializeField] Text wavesUI;                  // Istanza dell'UI per la visualizzazione della wave attuale.
    [SerializeField] GameObject NextWaveUI;         // NextWaveUI
    [SerializeField] GameObject ReturnToBaseUI;     // ReturnToBase alarm MENU UI

    private GameManager gameManager;                // Game Manager Instance



	void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        startNextWave();
    }
	
	void Update () {	
	}

    // Fa partire la coroutine di wave next // ATTIVATA DAL TASTO NEXT WAVE
    public void startNextWave()
    {
        gameManager.getStationInstance().GetComponent<PlayerBase>().SafeZoneSetActive(false);
        NextWaveUI.SetActive(false);
        ReturnToBaseUI.SetActive(false);
        StartCoroutine(StartNextWave());
    }

    // Inizializza e starta la wave successiva.
    IEnumerator StartNextWave()
    {
        if (waveIndex <= waves.Length - 1)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            Debug.Log("Starting wave:" + (waveIndex + 1));
            actualWave = Instantiate(waves[waveIndex], transform.position, transform.rotation);
            actualWave.transform.SetParent(this.gameObject.transform);
            waveIndex++;
            UpdateWavesUI();
        }
    }

    // Riceve e gestisce una richiesta di "start next Wave"
    public void NextWavePhaseHandler(bool value_2, bool value_3)
    {
        gameManager.getStationInstance().GetComponent<PlayerBase>().SafeZoneSetActive(true);
        NextWaveUI.SetActive(value_2);
        ReturnToBaseUI.SetActive(value_3);

    }

    

    // Aggiorna l'UI delle waves
    public void UpdateWavesUI()
    {
        wavesUI.text = " Wave: " + waveIndex + " of " + waves.Length;
    }


}
