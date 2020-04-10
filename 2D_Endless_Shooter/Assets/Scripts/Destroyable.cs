using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Gestisce il generico oggetto distruttibile, filtra le collisioni da non considerare come "DANNO".
 */

public class Destroyable : MonoBehaviour {

    [SerializeField] int life = 1;
    [SerializeField] bool immortal = false;
    [SerializeField] int scoreValue = 10;
    [SerializeField] int cashValue = 1;
    private GameObject scoreManager;

	void Start () {
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager");
	}
	
	void Update () {	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (immortal != true)
        {
            if (gameObject.tag == "Entities" || gameObject.tag == "Enemy")
            {
                if (collision.gameObject.tag != "EnemyFiring" && collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Entities")
                {
                    life = life - collision.gameObject.GetComponent<Bullet>().GetDamageValue();
                }
            }
        }

        if (life <= 0)
        {
            ScoreManager sc = scoreManager.GetComponent<ScoreManager>();
            sc.addCash(cashValue);
            sc.addScore(scoreValue);
            Destroy(this.gameObject);
        }
    }
}
