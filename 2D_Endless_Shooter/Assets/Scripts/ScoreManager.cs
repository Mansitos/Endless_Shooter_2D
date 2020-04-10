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

    private GameManager gameManager;        // Game Manager Instance

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        // Update UIs
        gameManager.getMainHUDManager().updateCashUI(cash);
        gameManager.getMainHUDManager().updateScoreUI(actualScore);
    }

    // Score methods (getters, setters etc.)
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

    // cash methods (getters, setters etc.)
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
