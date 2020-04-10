using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Script per il sottomodulo ScoreManager del GameManager.
 * 
 */

public class ScoreManager : MonoBehaviour
{

    [SerializeField] Text ScoreUI;
    [SerializeField] int actualScore;

    [SerializeField] Text CashUI;
    [SerializeField] int cash;
   
    void Start()
    { 
    }

    void Update()
    {
        UpdateScoreUI();
        UpdateCashUI();
    }

    void UpdateScoreUI()
    {
        ScoreUI.text = "SCORE: " + actualScore;
    }

    void UpdateCashUI()
    {
        CashUI.text = "CASH: " + cash;
    }

    public int getActualScore()
    {
        return actualScore;
    }

    public void addScore(int value)
    {
        actualScore = actualScore + value;
    }

    public void removeScore(int value)
    {
        actualScore = actualScore - value;
        if (actualScore < 0)
        {
            actualScore = 0;
        }
    }

    public int getActualCash()
    {
        return cash;
    }

    public void addCash(int value)
    {
        cash = cash + value;
    }

    public void removeCash(float value)
    {

        cash = cash - (int) value;
        if(cash < 0)
        {
            cash = 0;
        }
    }

}
