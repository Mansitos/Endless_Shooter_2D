using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealButton : MonoBehaviour
{
    public Text costTextUI;
    private float cost;
    public float myPercentage;
    public GameObject healsManager;
    public bool healsPlayer;
    public bool healsBase;

    private GameManager gameManager;

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
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        healsManager.GetComponent<HealsManager>().updateVariables();

        if (healsPlayer) {
            Player player = gameManager.getPlayerInstance().GetComponent<Player>();
            float playerMaxLife = player.getMaxLife();
            float lifeToHeal = playerMaxLife * myPercentage - player.getLife();
            cost = lifeToHeal * player.getPricePerHeal();
        }else if (healsBase)
        {
            PlayerBase station = gameManager.getStationInstance().GetComponent<PlayerBase>();
            float BaseMaxLife = station.getMaxLife();
            float lifeToHeal = BaseMaxLife * myPercentage - station.getLife();
            cost = lifeToHeal * station.getPricePerHeal();
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
