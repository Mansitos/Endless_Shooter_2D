using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script che gestisce il rilevamento di nemici per una torretta. 
 * Deve essere posto in un child object della torretta.
 */

public class TurretRadar : MonoBehaviour
{

    // debugging
    public bool debug;

    void Start()
    { 
    }

    void Update()
    {
    }

    // All'inizio di una collisione "triggerEnter", verifica che l'entità sia nemica, nel caso lo sia lo notifica al parent (la torretta).
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        NotifyParentTurret(target, true);
        if (debug)
        {
            Debug.Log("Turret: Enemy detected");
        }
    }

    // Alla conclusione di una collisione "triggerExit", verifica che l'entità sia nemica, nel caso lo sia lo notifica al parent (la torretta).
    void OnTriggerExit2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        NotifyParentTurret(target, false);

        if (debug)
        {
            Debug.Log("Turret: Enemy escaped!");
        }
    }

    // Se il parent (la torretta) non ha più un target, allora OnTriggerStay ritorna un entità che è attualmente nel range e lo notifica al parent che lo ingaggerà.
    void OnTriggerStay2D(Collider2D collision)
    {
        if(transform.parent.GetComponent<Turret>().getTarget() == null)
        {
            GameObject target = collision.gameObject;
            NotifyParentTurret(target, true); // true -> da considerare come nuova collisione.
        }

        if (debug)
        {
            Debug.Log("Turret: Engaging another enemy!");
        }
    }

    // Chiamata di supporto, notifica al parent un target, entering true -> nuova coll. entering false -> fine di una coll.
    void NotifyParentTurret(GameObject target, bool entering)
    {
        if (target.CompareTag("Enemy"))
        {
            transform.parent.GetComponent<Turret>().ReceiveTarget(target, entering);
        }
    }

}
