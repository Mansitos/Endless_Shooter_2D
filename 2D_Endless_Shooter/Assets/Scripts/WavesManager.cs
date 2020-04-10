﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Script di gestione dell' oggetto "WAVES MANAGER" che gestisce il flusso di ondate "WAVES" 
 * 
 * Dovrebbe essere un child di GameManager
 */

public class WavesManager : MonoBehaviour {

    public float timeToWaitAfterStart = 5;   // Tempo tra il click su "NEXT WAVE" e l'inizio effettivo della next wave
    public GameObject[] waves;               // Array delle waves in ordine di esecuzione.
    private int waveIndex = 0;                      // Wave raggiunta (counter)
    private GameObject actualWave;                  // Istanza della wave attuale

    public Text wavesUI;                  // Istanza dell'UI per la visualizzazione della wave attuale.
    public GameObject NextWaveUI;         // NextWaveUI
    public GameObject ReturnToBaseUI;     // ReturnToBase alarm MENU UI

    private GameManager gameManager;                // Game Manager Instance

	void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        startNextWave();
    }
	
	void Update () {	
	}

    // Fa partire la coroutine di wave next; attivata dal testo "NEXT WAVE"
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
        if (waveIndex <= waves.Length - 1)  // se non sono finite le wave
        {
            yield return new WaitForSeconds(timeToWaitAfterStart);  // aspetta timeBetweenWaves dopo il click di "NEXT WAVE"
            Debug.Log("Starting wave:" + (waveIndex + 1));
            actualWave = Instantiate(waves[waveIndex], transform.position, transform.rotation);
            actualWave.transform.SetParent(gameObject.transform);
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

    public GameObject getNextWaveUI()
    {
        return NextWaveUI;
    }


}
