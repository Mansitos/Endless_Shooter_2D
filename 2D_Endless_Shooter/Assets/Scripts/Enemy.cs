using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Questo Script gestisce l'entità "Enemy" di default.
 * 
 * Target è l'oggetto a cui punterà l'entità, muovendosi in sua direzione.
 *        Nel caso non sia NULL, e quindi abbia un target, le seguenti variabili indicano:
 *        -> target -> istanza del target (ad esempio Player, o PlayerBase).
 *        -> watchtarget -> se true, l'oggetto ruotera e punterà sempre il target.
 *        -> movespeed -> velocità di movimento
 *        -> neartargetsopeed -> velocità di movimento quando nel range "near target" (tipicamente inferiore a movespeed)
 *        -> mindistancetotarget -> distanza minima da mantenere dal target.
 *        -> nearTargetDistance -> distanza sotto il quale l'entità è nella zona "near target"
 * 
 * Shooting variables: variabili per la gestione dello shooting dell' entità Enemy.
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
    [SerializeField] public int difficultyValue = 1;

    // TARGET VARIABLES //
    [SerializeField] bool targetIsPlayer = true;
    [SerializeField] bool targetIsBase = false;
    private GameObject target = null;
    [SerializeField] bool watchTarget = true;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float nearTargetSpeed = 0.5f;
    [SerializeField] float minDistanceToTarget = 1f;
    [SerializeField] float nearTargetDistance = 3f;
    [SerializeField] bool isStopped = false;

    // SHOOTING VARIABLES //
    [SerializeField] bool canShoot;
    [SerializeField] GameObject weaponProjectile;
    [SerializeField] float fireRate;
    Coroutine firing;
    [SerializeField] bool isFiring = false;
    [SerializeField] float minFiringDistance = 2f;

    // DEBUG //
    [SerializeField] bool distancesColorsDebug = false;


	void Start () {	
	}
	

	void Update () {

        SearchTarget();

        if(target != null)
        {
            moveToTarget(); // Se ha un target assegnato, aggiorna la posizione muovendoti verso di lui.

            if (watchTarget == true)
            {
                WatchTarget(); // Se ha un target assegnato, aggiorna la rotazione su Z in modo da guardare il target.
            }

            ManageShooting(); // Managine del processo di shooting. aggiornato ogni frame.
        }

        ManageDebugs();
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
        if(target != null)
        {
           Vector2 targetPosition = target.transform.position;
           float distance = Vector2.Distance(targetPosition, transform.position);
           float movingSpeed;

           if(distance < nearTargetDistance)
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
            //Debug.Log("Enemy: I've shoot a pim to the target! :D");
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
                if(firing != null)
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
        if(distancesColorsDebug == true)
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
        if (targetIsPlayer) { target = GameObject.FindGameObjectWithTag("Player"); }
        if (targetIsBase) { target = GameObject.FindGameObjectWithTag("PlayerBase"); }
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
