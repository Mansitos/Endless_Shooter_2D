using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Gestisce il generico oggetto distruttibile.
 * --> filtra le collisioni da non considerare come danno.
 *      --> ad esempio collisione con la playerhitbox -> 0 danno,
 *      --> collisione proiettile -> danno
 */

public class Destroyable : MonoBehaviour {

    public int life = 1;
    public bool immortal = false;
    public int scoreValue = 10;
    public int cashValue = 1;

    private GameManager gameManager;

	void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
	
	void Update () {	
	}

    // Esegue un filtro sulle collisioni in base al tag dell'oggetto con cui collide; se è di un determinato tipo allora decrementane la vita.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (immortal != true)
        {
            if (gameObject.tag == "Entities" || gameObject.tag == "Enemy")  // tag di oggetti distruggibili dal player
            {
                if (collision.gameObject.tag == "PlayerFiring")
                {
                    life = life - collision.gameObject.GetComponent<Bullet>().GetDamageValue();
                }
            }
        }

        if (life <= 0)  // vita azzerata, distruggi e dai al playe realativo score e cash
        {
            gameManager.getScoreManager().addCash(cashValue);
            gameManager.getScoreManager().addScore(scoreValue);
            Destroy(this.gameObject);
        }
    }
}
