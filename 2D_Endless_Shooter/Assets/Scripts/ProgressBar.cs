using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{

    private float percentageStatus = 50; // from 0 to 100
    public int maxWidth = 100;   // massima larghezza della barra interna quando al 100
    public GameObject statusBar;

    GameManager gameManager;
  
    void Start()
    {
        gameManager = gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
    }

    private void correctPercentageValue()
    {
        if(percentageStatus < 0)
        {
            percentageStatus = 0;
        }
        else if(percentageStatus > 100)
        {
            percentageStatus = 100;
        }
    }

    public void updateProgressBar(float value, float maxValue)
    {
        percentageStatus = value / maxValue;
        correctPercentageValue(); // fix overflow and underflow
        RectTransform statusBarRectTransform = statusBar.transform.GetComponent<RectTransform>();
        statusBarRectTransform.sizeDelta = new Vector2(percentageStatus*maxWidth, statusBarRectTransform.sizeDelta.y);
    }
}
