using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{

    // sub-manager-modules
    private HealsManager healsManager;

    // UIs
    public GameObject UpgradesMenuUI;

    private GameManager gameManager;                // Game Manager Instance

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        getSubManagerModulesIstances();
    }

    void Update()
    {    
    }

    void getSubManagerModulesIstances()
    {
        healsManager = GameObject.FindGameObjectWithTag("HealsManager").GetComponent<HealsManager>();
    }

    public HealsManager getHealsManager()
    {
        return healsManager;
    }

    public GameObject getUpgradesMenuUI()
    {
        return UpgradesMenuUI;
    }

    public void EnableDisableUpgradeMenuUI(bool value)
    {
        UpgradesMenuUI.SetActive(value);
        gameManager.getWavesManager().getNextWaveUI().SetActive(!value);
        gameManager.getPlayerInstance().SetActive(!value);
        if (value == true)
        {
            healsManager.updateVariables();
            healsManager.updateButtonsActive();
        }
    }

}
