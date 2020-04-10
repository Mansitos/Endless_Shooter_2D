using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneCollider : MonoBehaviour
{
    /*
     * Script per la gestione della safeZone della main base.
     * Possiede un collider, (trigger=on) che rileva se il player è o no in safezon, notifica lo script del waves manager
     * ogni qual volta il player entra ed esce
     */

    private bool playerIsOnSafeZone = false;            // true se il player è in safezon -> iscolliding with player = true
    [SerializeField] GameObject WavesManagerInstance;   // Istanza del WavesManager

    void Start()
    {
    }

    void Update()
    {  
    }

    void OnTriggerEnter2D(Collider2D col)   // il player è entrato in safezone, notifica il wavesManager
    {
        if(col.gameObject.tag == "Player")
        {
            WavesManagerInstance.GetComponent<WavesManager>().NextWavePhaseHandler(true,false);
        }
    }

    void OnTriggerExit2D(Collider2D col)    // il player è uscito dalla safezone, notifica il wavesManager
    {
        if (col.gameObject.tag == "Player")
        {
            WavesManagerInstance.GetComponent<WavesManager>().NextWavePhaseHandler(false,true);
        }
    }
}
