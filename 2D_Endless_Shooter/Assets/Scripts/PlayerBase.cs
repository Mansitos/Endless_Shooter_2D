using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Script di gestione della MAIN BASE del player.
 */

public class PlayerBase : MonoBehaviour {

    public int life = 100;        // Vita attuale
    public int maxLife = 100;     // Vita massima
    public bool immortal = false;     // Se true, la base è immortale, per debugging e testing
    public Text baseLifeUI;           // Istanza UI per la vita della base.
    public int cashPerHeal;
    public GameObject SafeZone;

	void Start () {
	}
	
	void Update () {
        UpdateLifeUI();
	}

    // Rileva le collisioni con proiettili nemici, e applica il danno.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (immortal != true)
        { 
            if (collision.gameObject.tag == "EnemyFiring")
            {
               life--;
            }
        }
        if (life <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    // Aggiorna l'UI della VITA della BASE
    void UpdateLifeUI()
    {
        baseLifeUI.text = "BASE LIFE: "+life+"/" + maxLife;
    }

    public int getLife()
    {
        return life;
    }

    public int getMaxLife()
    {
        return maxLife;
    }

    public int getPricePerHeal()
    {
        return cashPerHeal;
    }

    public void increaseLife(float value)
    {
        life = life + (int) value;
        UpdateLifeUI();
    }

    public void setPercentageLife(int percentage)
    {
        int newlife = (int)(maxLife * ((float)(percentage) / 100));
        life = newlife + 1;
        if (life >= maxLife)
        {
            life = maxLife;
        }
        UpdateLifeUI();
    }

    public void SafeZoneSetActive(bool value)
    {
        SafeZone.SetActive(value);
    }
}
