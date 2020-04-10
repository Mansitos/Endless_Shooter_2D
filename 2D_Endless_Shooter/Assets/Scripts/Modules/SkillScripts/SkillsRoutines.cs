using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsRoutines : MonoBehaviour
{
    private GameObject player;
    private GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        player = gameManager.GetComponent<GameManager>().getPlayerInstance();
    }

    void Update()
    {  
    }

    public void ManageRequest(int ID, int value)
    {
        switch (ID)
        {
            case 1:
                IncreasePlayerMaxLife(value);
                break;
        }
    }

    void IncreasePlayerMaxLife(int value)
    {
        player.GetComponent<Player>().IncreaseMaxLife(value);
        player.GetComponent<Player>().UpdateLifeUI();
    }
}
