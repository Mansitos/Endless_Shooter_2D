using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainHUDManager : MonoBehaviour
{
    // Componenti del main HUD //
    public Text baseLifeUI;
    public Text playerLifeUI;
    public Text actualWaveUI;
    public Text returnToBaseMessageUI;
    public Text scoreUI;
    public Text cashUI;
    public Text abortMissionTimer;
    public GameObject maxDistanceReachedUI;

    void Start()
    {   
    }

    void Update()
    {   
    }

    public void updateBaseLifeUI(int new_value, int max_value)
    {
        baseLifeUI.text = "BASE LIFE: " + new_value + "/" + max_value;
    }

    public void updatePlayerLifeUI(int new_value, int max_value)
    {
        playerLifeUI.text = "LIFE: " + new_value + "/" + max_value;
    }

    public void updateActualWaveUI(int new_value, int max_value)
    {
        actualWaveUI.text = "WAVE: " + new_value + " OF " + max_value;
    }

    public void updateScoreUI(int new_value)
    {
        scoreUI.text = "SCORE: " + new_value;
    }

    public void updateCashUI(int new_value)
    {
        cashUI.text = "CASH: " + new_value;
    }

    public Text getReturnToBaseMessageUI()
    {
        return returnToBaseMessageUI;
    }

    public GameObject getDistanceReachedUI()
    {
        return maxDistanceReachedUI;
    }

    public void updateAbortMissionTimer(int new_value)
    {
        abortMissionTimer.text = "" + new_value;
    }
}
