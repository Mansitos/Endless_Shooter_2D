using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealButton : MonoBehaviour
{
    // Sub-Script per i singoli pulsanti relativi allo script HealsManager
    // Ciascun pulsante gestice l'input di cura relativo alle sue caratteristiche (pulsate di cura del 25% della vita del player; ad esempio)

    // Variabili generali //
    public Text costTextUI;     // Testo indicante il costo 
    private float cost;         // costo della cura
    public float myPercentage;  // percentuale di cura relativa a questo pulsante (ad esempio 25% -> questo pulsante cura fino al 25%)
    public GameObject healsManager; // istanza del parent script

    public bool healsPlayer;        // -> true -> cura il player    (dovrebbe essere solo uno dei due true)
    public bool healsBase;          // -> true -> cura la base      (dovrebbe essere solo uno dei due true)

    private GameManager gameManager;        // game manager

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        CalculateCost();
        UpdateIsActive();
    }


    void Update()
    {  
    }


    void updateCostTextUI()
    {
        costTextUI.text = "COST:" + cost;
    }

    public void CalculateCost()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();  // needs to be there because this functions is called before Start()
        healsManager.GetComponent<HealsManager>().updateVariables();

        if (healsPlayer) {
            Player player = gameManager.getPlayerInstance().GetComponent<Player>();
            float playerMaxLife = player.getMaxLife();
            float lifeToHeal = playerMaxLife * myPercentage - player.getLife();
            cost = Mathf.Ceil(lifeToHeal * player.getPricePerHeal());
        }else if (healsBase)
        {
            PlayerBase station = gameManager.getStationInstance().GetComponent<PlayerBase>();
            float BaseMaxLife = station.getMaxLife();
            float lifeToHeal = BaseMaxLife * myPercentage - station.getLife();
            cost = Mathf.Ceil(lifeToHeal * station.getPricePerHeal());
        }

        updateCostTextUI();

    }

    public void UpdateIsActive()
    {
        if (healsPlayer) // Update HealsPlayer Buttons
        {
            float playerLifePercentage = healsManager.GetComponent<HealsManager>().getPlayerLifePercentage();
            if(playerLifePercentage >= myPercentage)
            {
                this.gameObject.GetComponent<Button>().interactable = false;
                this.costTextUI.gameObject.SetActive(false);
            }
            else
            {
                this.gameObject.GetComponent<Button>().interactable = true;
                this.costTextUI.gameObject.SetActive(true);
            }
        }
        else if (healsBase) // Update HealsStation Buttons
        {
            float StationLifePercentage = healsManager.GetComponent<HealsManager>().getStationLifePercentage();
            if (StationLifePercentage >= myPercentage)
            {
                this.gameObject.GetComponent<Button>().interactable = false;
                this.costTextUI.gameObject.SetActive(false);
            }
            else
            {
                this.gameObject.GetComponent<Button>().interactable = true;
                this.costTextUI.gameObject.SetActive(true);
            }
        }
    }

    public void ManageRequest() // Receive input from buttons, manage the request
    {
        ScoreManager scoreManager = gameManager.getScoreManager();
        int cash = scoreManager.getActualCash();

        if(cash > cost)
        {
            if (healsPlayer)
            {
                Player player = gameManager.getPlayerInstance().GetComponent<Player>();
                player.setPercentageLife((int)(myPercentage * 100));
                scoreManager.removeCash(cost);
            }
            else if (healsBase)
            {
                PlayerBase station = gameManager.getStationInstance().GetComponent<PlayerBase>();
                station.setPercentageLife((int)(myPercentage * 100));
                scoreManager.removeCash(cost);
            }
        }
        else
        {
            healsManager.GetComponent<HealsManager>().startCashWarning();
        }

        healsManager.GetComponent<HealsManager>().updateVariables();
        healsManager.GetComponent<HealsManager>().updateButtonsStatus();
    }
}
