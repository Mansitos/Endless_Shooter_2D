﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Questo Script gestisce l'entità "Enemy" di default.
 * 
 * Target è l'oggetto a cui punterà l'entità, muovendosi verso la sua direzione. [da migliorare con un algoritmo di pathphinding]
 *        Nel caso non sia NULL, e quindi abbia un target, le seguenti variabili indicano:
 *        -> target -> istanza del target (ad esempio Player, o PlayerBase).
 *        -> watchTarget -> se true, l'oggetto ruotera e punterà sempre il target.
 *        -> moveSpeed -> velocità di movimento
 *        -> nearTargetSpeed -> velocità di movimento quando nel range "near target" (tipicamente inferiore a movespeed)
 *        -> minDistanceToTarget -> distanza minima da mantenere dal target.
 *        -> nearTargetDistance -> distanza sotto il quale l'entità è nella zona "near target"
 * 
 * Shooting variables: variabili per la gestione dello shooting dell'entità Enemy.
 *        -> canShoot -> se true può sparare
 *        -> weaponProjectile -> proiettile che spara (ad es. laserBeam)
 *        -> fireRate -> velocità di fuoco in RPM
 *        -> firing -> Istanza della coroutine di fuoco
 *        -> isFiring -> true se sta sparando, false altrimenti
 *        -> minFiringDistance -> distanza sotto il quale inizierà a sparare al Target.
 * 
 */

public class Enemy : MonoBehaviour {

    // GENERAL //
    public int difficultyValue = 1; // parametro indicante il valore di difficoltà di questa entità (se vale 2, e la wave ha difficoltà 12, ne spawneranno 6)

    // TARGET VARIABLES //
    public bool targetIsPlayer = true;
    public bool targetIsBase = false;
    private GameObject target = null;
    public bool watchTarget = true;
    public float moveSpeed = 1f;
    public float nearTargetSpeed = 0.5f;    // velocità una volta entrati in nearTargetDistance
    public float minDistanceToTarget = 1f;
    public float nearTargetDistance = 3f;
    public bool isStopped = false;

    // SHOOTING VARIABLES //
    public bool canShoot;
    public GameObject weaponProjectile;
    public float fireRate;
    Coroutine firing;
    public bool isFiring = false;
    public float minFiringDistance = 2f;

    // Altre
    GameManager gameManager;

    // DEBUG //
    public bool distancesColorsDebug = false;


    void Start() {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }


    void Update() {

        SearchTarget();
        Move();

        ManageDebugs();
    }

    // Gestisce il movimento dell'entità ad ogni frame.
    // fa uso di sotto-funzioni per ciascuna sotto-attività.
    void Move()
    {
        if (target != null)
        {
            moveToTarget(); // Se ha un target assegnato, aggiorna la posizione muovendo l'entità verso il target.

            if (watchTarget == true)
            {
                WatchTarget(); // Se ha un target assegnato, aggiorna la rotazione su Z in modo da guardare il target.
            }

            ManageShooting(); // Managine del processo di shooting. aggiornato ogni frame.
        }
    }


    // Muove l'entità in direzione del target.
    // -> Trova la pos. del target
    // -> ne calcola la distanza
    // -> se è più lontano della distanza minima da mantenere si avvicina ->
    //      -> Seleziona la velocità corretta in base al fatto di essere o no nel near range.
    //      -> calcola delta movement corretto in base al time.deltatime dell'ultimo frame.
    //      -> si muove in direzione del target, REPEAT.

    void moveToTarget()
    {
        if (target != null)
        {
            Vector2 targetPosition = target.transform.position;
            float distance = Vector2.Distance(targetPosition, transform.position);
            float movingSpeed;

            if (distance < nearTargetDistance)
            {
                movingSpeed = nearTargetSpeed;
                isStopped = true;
            }
            else
            {
                movingSpeed = moveSpeed;
                isStopped = false;
            }

            var deltaMovement = movingSpeed * Time.deltaTime; // FRAME RATE INDIPENDENT

            if (distance > minDistanceToTarget)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, deltaMovement);
            }
        }
    }

    // Ruota l'entità sempre in direzione del target.
    void WatchTarget()
    {
        var direction = target.transform.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Coroutine di firing. Istanzia un proiettile con cadenza "firing rate".
    IEnumerator FireToTarget()
    {
        while (true)
        {
            GameObject projectile = Instantiate(weaponProjectile, transform.position, this.transform.rotation) as GameObject;
            yield return new WaitForSeconds(60 / fireRate);
        }
    }

    // Gestisce lo shooting, controlla che l'entità sia dentro il range di shooting, se si spara, quando esce stoppa lo shooting.
    // Agisce sulla coroutine FireToTarget().
    void ManageShooting()
    {
        Vector2 targetPosition = target.transform.position;
        float distance = Vector2.Distance(targetPosition, transform.position);

        if (isFiring == false)
        {
            if (distance < minFiringDistance)
            {
                isFiring = true;
                firing = StartCoroutine(FireToTarget());
            }
            else
            {
                isFiring = false;
                if (firing != null)
                {
                    StopCoroutine(firing);
                }
            }
        }
        else if (firing != null && distance >= minFiringDistance)
        {
            isFiring = false;
            StopCoroutine(firing);
        }
    }

    void ManageDebugs()
    {
        if (distancesColorsDebug == true)
        {
            Vector2 targetPosition = target.transform.position;
            float distance = Vector2.Distance(targetPosition, transform.position);

            if (distance < minFiringDistance)
            {
                GetComponent<SpriteRenderer>().color = Color.red;
            }
            else if (distance >= minFiringDistance)
            {
                GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }
        else
        {
            //GetComponent<SpriteRenderer>().color = Color.white;
        }


    }

    // Esisterà sempre 1 player e 1 base, quindi l'output è univoco.
    void SearchTarget()
    {
        if (targetIsPlayer) { target = gameManager.getPlayerInstance(); }
        if (targetIsBase) { target = gameManager.getStationInstance();  }
    }

    // TODO!TODO!TODO!TODO!
    // ritorna la speed, approssimazione, movespeed se si muove, 0 se fermo, ma rallenta... andrebbe migliorata ritornando il valore di speed effettivo del motore fisico, ma come? TODO!
    public float GetSpeed()
    {
        if(isStopped == true)
        {
            return 0;
        }
        return moveSpeed;
    }

    public GameObject GetTarget()
    {
        return target;
    }
}
