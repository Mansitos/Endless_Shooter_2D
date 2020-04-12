using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealsManager : MonoBehaviour
{
    // Variabili generali //
    private GameObject player;
    private GameObject station;
    public GameObject scoreManager;
    public GameObject[] HealButtons;
    private Coroutine warningCoroutine;

    // UIs elements //
    public Text notEnoughCashUI;

    // variabili di Player/Station utilizzate per il calcolo dei costi relativi all'healing
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
        player = gameManager.getPlayerInstance();
        station = gameManager.getStationInstance();

        updateVariables();
        EnableDisableCashWarning(false);
    }

    void Update()
    {
    }

    public void updateVariables()
    {
        playerLife = player.GetComponent<Player>().getLife();
        playerMaxLife = player.GetComponent<Player>().getMaxLife();
        playerLifePercentage = (playerLife / playerMaxLife);

        stationLife = station.GetComponent<PlayerBase>().getLife();
        stationMaxLife = station.GetComponent<PlayerBase>().getMaxLife();
        stationLifePercentage = (stationLife / stationMaxLife);
    }

    public void updateButtonsStatus()
    {
        for(int i = 0; i< HealButtons.Length; i++)
        {
            HealButtons[i].GetComponent<HealButton>().CalculateCost();
            HealButtons[i].GetComponent<HealButton>().UpdateIsActive();
        }
    }

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
