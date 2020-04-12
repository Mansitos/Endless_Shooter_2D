using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script per la gestione della Torretta
 */
public class Turret : MonoBehaviour
{

    // VARIABILI GENERALI //
    public GameObject Target;         // Target su cui sparare
    public GameObject weaponBullet;   // Istanza di proiettile da usare
    public float fireRate;            // firing rate in RPM
    public bool canShoot;             // true -> può sparare
    public float range;               // range della torretta -> utilizzato per settare il raggio del collider del TurretRadar
    private CircleCollider2D radarCollider;     // Istanza del range collider
    private bool isShooting;                    // true -> sta sparando
    public bool predictTrajectory = true;       // Se true, la torretta spara anticipando la mira per non missare gli hit
    Coroutine shootingCorouitine = null;        // Instanza di coroutine shooting

    // DEBUGGING //
    public bool debug;
    public GameObject shootingPoint;          // effettiva pos dove spara con predictTrajectory true. -> "debug in editor" , cerchio dove spara.

    void Start()
    {
        InitializeTurretRadarCollider();                  
    }

    void Update()
    {
        ShootingTargetProcess();
    }

    // Funzione che gestisci lo start/stop del processo di fuoco verso il target
    void ShootingTargetProcess()
    {
        if (Target != null)
        {
            WatchTarget();

            if (isShooting == false)
            {
                shootingCorouitine = StartCoroutine(FireToTarget());
                isShooting = true;
            }
        }
        else if (shootingCorouitine != null && Target == null)
        {
            StopCoroutine(shootingCorouitine);
            isShooting = false;
        }
    }

    // Inizializza il range collider, cercando il child(0), prendendone il collider e settandone il raggio.
    // Lavora sul sottomodulo TurretRadar
    void InitializeTurretRadarCollider()
    {
        radarCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
        radarCollider.radius = range;
    }

    // Riceve notifica di target dal child.
    // entering specifica se l'informazione del target fa riferimento ad una uscita o entrata nel range.
    // true -> entrata
    // false -> uscita
    public void ReceiveTarget(GameObject target, bool entering)
    {
        if (target != null)
        {
            if (entering == true && Target == null)
            {
                Target = target; // new target, yolo! :3
            }
            else if (entering == false)
            {
                Target = null;
            }
        }

    }

    // Ruota l'entità sempre in direzione del target a cui sparare
    // Se predict è TRUE, non guarderà esattamente il target ma leggermente avanti, anticipandone la mira.
    void WatchTarget()
    {
        Vector3 NextPos = Target.transform.position;
        if (predictTrajectory == true)
        {
            //////////////////// PREDICT ENEMY POSITION /////////////////////////////
            Vector2 TargetPos = Target.transform.position;
            float distance = Vector2.Distance(TargetPos, transform.position);
            float bulletTime = distance / weaponBullet.GetComponent<Bullet>().GetSpeed();
            var TargetdeltaMovement = Target.GetComponent<Enemy>().GetSpeed() * bulletTime;
            NextPos = Vector2.MoveTowards(TargetPos, Target.GetComponent<Enemy>().GetTarget().transform.position, TargetdeltaMovement);
            if (debug) { Debug.Log(distance + "|" + bulletTime + "|" + TargetdeltaMovement + "| trajectory prediction: " + predictTrajectory); }
            /////////////////////////////////////////////////////////////////////////
        }

        shootingPoint.transform.position = NextPos;
        var direction = NextPos - transform.position; // OLD: Target.transform.position  - transform.position == normal
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Coroutine di shooting
    IEnumerator FireToTarget()
    {
        while (true)
        {
            Instantiate(weaponBullet, transform.position, this.transform.rotation);
            yield return new WaitForSeconds(60 / fireRate);
        }
    }

    // returns Target Instance
    public GameObject getTarget()
    {
        return Target;
    }



}
