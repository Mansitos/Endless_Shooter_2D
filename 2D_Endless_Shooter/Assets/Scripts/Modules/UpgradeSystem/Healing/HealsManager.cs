using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealsManager : MonoBehaviour
{
    // Variabili generali //
    private GameObject player;          // istanza del player
    private GameObject station;         // istanza della base
    public GameObject scoreManager;
    public GameObject[] HealButtons;
    private Coroutine warningCoroutine;

    // UIs elements //
    public Text notEnoughCashUI;

    // variabili di Player e Base(Station) utilizzate per il calcolo dei costi relativi all'healing
    private float playerLife;
    private float playerMaxLife;
    private float stationLife;
    private float stationMaxLife;
    private float playerLifePercentage;
    private float stationLifePercentage;

    // sub-modules
    public GameObject HealsUI;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        // inizializzazione dello script, ottengo le istanze di base e player
        player = gameManager.getPlayerInstance();
        station = gameManager.getStationInstance();

        // Ottengo tutte le informazioni di cui lo script ha bisogno: vita del player, etc.
        updateVariables();

        // inizializzo a false (inattivo) il testo di pop-up di avvertimento di cash insufficienti per l'healing richiesto
        EnableDisableCashWarning(false);
    }

    void Update()
    {
    }

    // Va ad aggiornare tutte le informazioni necessarie al corretto funzionamento dello script.
    public void updateVariables()
    {
        playerLife = player.GetComponent<Player>().getLife();
        playerMaxLife = player.GetComponent<Player>().getMaxLife();
        playerLifePercentage = (playerLife / playerMaxLife);

        stationLife = station.GetComponent<PlayerBase>().getLife();
        stationMaxLife = station.GetComponent<PlayerBase>().getMaxLife();
        stationLifePercentage = (stationLife / stationMaxLife);
    }

    // Setta i vari pulsanti di Healing come attivi o disattivati: ad esempio il pulsante "cura fino a 25%" deve essere DISATTIVO se la vita del player è già sopra il 25% etc.
    public void updateButtonsStatus()
    {
        for(int i = 0; i< HealButtons.Length; i++)
        {
            HealButtons[i].GetComponent<HealButton>().CalculateCost();      // Calcola il costo per la cura rappresentata da quel pulsante
            HealButtons[i].GetComponent<HealButton>().UpdateIsActive();     // Verifica se esso vada attivato o no
        }
    }

    // Abilita o disabilita in base al parametro passato il messaggio di warning "Not enough cash", utilizzata dalla coroutine del "pop-up"
    void EnableDisableCashWarning(bool value)
    {
        notEnoughCashUI.gameObject.SetActive(value);
    }
    
    // implementa una sorta di pop-up per il messaggio di "soldi non sufficienti" che dura 2sec
    public IEnumerator CashWarningCoroutine()
    {
        EnableDisableCashWarning(true);
        yield return new WaitForSeconds(2);
        EnableDisableCashWarning(false);
        warningCoroutine = null;
    }

    public void startCashWarning()
    {
        if(warningCoroutine == null) // startala solo se non c'è già una courotuine di questo tipo attiva...
        {
            warningCoroutine = StartCoroutine(CashWarningCoroutine());
        }
    }

    public float getStationLifePercentage()
    {
        return stationLifePercentage;
    }

    public float getPlayerLifePercentage()
    {
        return playerLifePercentage;
    }

    public GameObject getHealsUI()
    {
        return HealsUI;
    }

    public void enableDisableHealsUI(bool value)
    {
        HealsUI.SetActive(value);
    }
}
