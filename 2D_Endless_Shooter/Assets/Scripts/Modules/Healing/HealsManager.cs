using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealsManager : MonoBehaviour
{

    private GameObject player;
    private GameObject station;
    private GameObject gameManager;
    [SerializeField] GameObject scoreManager;
    [SerializeField] GameObject[] HealButtons;
    [SerializeField] Text notEnoughCashUI;

    private float playerLife;
    private float playerMaxLife;
    private float stationLife;
    private float stationMaxLife;
    private float playerLifePercentage;
    private float stationLifePercentage;

    private Coroutine warningCoroutine;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        player = gameManager.GetComponent<GameManager>().getPlayerInstance();
        station = gameManager.GetComponent<GameManager>().getStationInstance();

        updateVariables();
        CashWarning(false);
    }

    void Update()
    {
    }

    public void updateVariables()
    {
       gameManager = GameObject.FindGameObjectWithTag("GameManager");
       player = gameManager.GetComponent<GameManager>().getPlayerInstance();
       station = gameManager.GetComponent<GameManager>().getStationInstance();

       playerLife = player.GetComponent<Player>().getLife();
       playerMaxLife = player.GetComponent<Player>().getMaxLife();
       playerLifePercentage = (playerLife / playerMaxLife);

       stationLife = station.GetComponent<PlayerBase>().getLife();
       stationMaxLife = station.GetComponent<PlayerBase>().getMaxLife();
       stationLifePercentage = (stationLife / stationMaxLife);
    }

    public void updateButtonsActive()
    {
        for(int i = 0; i< HealButtons.Length; i++)
        {
            HealButtons[i].GetComponent<HealButton>().CalculateCost();
            HealButtons[i].GetComponent<HealButton>().UpdateIsActive();
        }
    }

    void CashWarning(bool value)
    {
        notEnoughCashUI.gameObject.SetActive(value);
    }

    public IEnumerator CashWarningCoroutine()
    {
        CashWarning(true);
        yield return new WaitForSeconds(2);
        CashWarning(false);
        warningCoroutine = null;
    }

    public void startCashWarning()
    {
        if(warningCoroutine == null)
        {
            warningCoroutine = StartCoroutine(CashWarningCoroutine());
        }

    }

    public GameObject getPlayer()
    {
        return player;
    }

    public GameObject getStation()
    {
        return station;
    }

    public GameObject getScoreManager()
    {
        return scoreManager;
    }

    public float getPlayerMaxLife()
    {
        return playerMaxLife;
    }

    public float getPlayerLife()
    {
        return playerLife;
    }

    public float getBaseMaxLife()
    {
        return stationMaxLife;
    }

    public float getBaseLife()
    {
        return stationLife;
    }

    public float getStationLifePercentage()
    {
        return stationLifePercentage;
    }

    public float getPlayerLifePercentage()
    {
        return playerLifePercentage;
    }
}
