﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script di gestione del comportamento del proiettile generale.
 * life_time è il tempo dopo il quale l'oggetto deve essere distrutto.
 * L'oggetto viene distrutto anche nel caso in cui entri in una collisione 2D.
 * speed -> è la velocità di movimento.
 * damageValue -> danno del proiettile.
 */

public class Bullet : MonoBehaviour {

    public float speed = 1;
    public float life_time = 10;
    public int damageValue = 1;   

	void Start(){
        Move();
        Destroy(this.gameObject,life_time); // Starta la distruzione temporizzata
	}
	
	void Update(){
	}

    // Chiamato in start, attiva il movimento del proiettile applicandogli la giusta componente di velocità sull'asse delle X e Y in base all'orientamento, calcolata mediante sen e cos.
    private void Move()
    {
        float angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Mathf.Sin(angle) * -1, speed * Mathf.Cos(angle));
    }

    // Distruggi l'oggetto in presenza di collisione 2D.
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

    public int GetDamageValue()
    {
        return damageValue;
    }

    public float GetSpeed()
    {
        return speed;
    }

}
