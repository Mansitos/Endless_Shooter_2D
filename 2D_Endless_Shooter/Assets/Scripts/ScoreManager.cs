using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Script per il sottomodulo ScoreManager del GameManager.
 * 
 * Dovrebbe essere un child di GameManager
 */

public class ScoreManager : MonoBehaviour
{
    // VARIABILI GENERALI //
    public int actualScore = 0;
    public int cash = 0;

    // UI - relativi allo score e al denaro //
    public Text ScoreUI;
    public Text CashUI;

    void Start()
    { 
    }

    void Update()
    {
        UpdateScoreUI();
        UpdateCashUI();
    }

    // UI methods
    void UpdateScoreUI()
    {
        ScoreUI.text = "SCORE: " + actualScore;
    }

    void UpdateCashUI()
    {
        CashUI.text = "CASH: " + cash;
    }

    public Text getScoreUI()
    {
        return ScoreUI;
    }

    public Text getCashUI()
    {
        return CashUI;
    }

    // Score methods
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

    // cash methods
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
