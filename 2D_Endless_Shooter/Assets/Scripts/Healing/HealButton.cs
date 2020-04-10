using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealButton : MonoBehaviour
{
    [SerializeField] Text costText;
    private float cost;
    [SerializeField] float myPercentage;
    [SerializeField] GameObject HealsManager;
    private HealsManager hm;
    [SerializeField] bool healsPlayer;
    [SerializeField] bool healsBase;

    void Start()
    {
        hm = HealsManager.GetComponent<HealsManager>();

        CalculateCost();
        UpdateIsActive();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateCostText()
    {
        costText.text = "COST:" + cost;
    }

    public void CalculateCost()
    {
        hm.updateVariables();

        if (healsPlayer) {
            float playerMaxLife = hm.getPlayerMaxLife();
            float lifeToHeal = playerMaxLife*myPercentage - hm.getPlayerLife();
            cost = lifeToHeal * hm.getPlayer().GetComponent<Player>().getPricePerHeal();
        }else if (healsBase)
        {
            float BaseMaxLife = hm.getBaseMaxLife();
            float lifeToHeal = BaseMaxLife * myPercentage - hm.getBaseLife();
            cost = lifeToHeal * hm.getStation().GetComponent<PlayerBase>().getPricePerHeal();
        }

        updateCostText();

    }

    public void UpdateIsActive()
    {
        if (healsPlayer) // Update HealsPlayer Buttons
        {
            float playerLifePercentage = hm.getPlayerLifePercentage();
            if(playerLifePercentage >= myPercentage)
            {
                this.gameObject.GetComponent<Button>().interactable = false;
                this.costText.gameObject.SetActive(false);
            }
            else
            {
                this.gameObject.GetComponent<Button>().interactable = true;
                this.costText.gameObject.SetActive(true);
            }
        }
        else if (healsBase) // Update HealsStation Buttons
        {
            float StationLifePercentage = hm.getStationLifePercentage();
            if (StationLifePercentage >= myPercentage)
            {
                this.gameObject.GetComponent<Button>().interactable = false;
                this.costText.gameObject.SetActive(false);
            }
            else
            {
                this.gameObject.GetComponent<Button>().interactable = true;
                this.costText.gameObject.SetActive(true);
            }
        }
    }

    public void ManageRequest() // Receive input from buttons, manage the request
    {
        int cash = hm.getScoreManager().GetComponent<ScoreManager>().getActualCash();

        if(cash > cost)
        {
            if (healsPlayer)
            {
                hm.getPlayer().GetComponent<Player>().setPercentageLife((int)(myPercentage * 100));
                hm.getScoreManager().GetComponent<ScoreManager>().removeCash(cost);
            }
            else if (healsBase)
            {
                hm.getStation().GetComponent<PlayerBase>().setPercentageLife((int)(myPercentage * 100));
                hm.getScoreManager().GetComponent<ScoreManager>().removeCash(cost);
            }
        }
        else
        {
            hm.startCashWarning();
        }

        hm.updateVariables();
        hm.updateButtonsActive();
    }
}
