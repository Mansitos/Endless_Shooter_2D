using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wings : MonoBehaviour
{
    /*  Script per la gestione del componente WINGS per la ship modulare
    */

    public GameObject leftWing;
    public GameObject rightWing;

    public GameObject[] weaponsSlots;

    void Start()
    {

    }

    void Update()
    { 
    }

    public GameObject[] getWeaponsSlots()
    {
        return weaponsSlots;
    }

    // posiziona le due ali nella posizione corretta (quella passata come parametro dal chiamante, che tipiamente è lo chassies che possiede 2 AttrachPoint per le ali)
    public void InitializeWingsPositions(Transform transform, string type)
    {
        if(type == "left")
        {
            leftWing.transform.position = transform.position;
        }else if(type == "right")
        {
            rightWing.transform.position = transform.position;
        }
    }
}
