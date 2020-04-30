using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

    /*  Script per la gestione della SHIP modulare
    */

    public string name;
    public GameObject chassis;
    
    void Start()
    {    
    }

    void Update()
    {    
    }

    public GameObject getChassis()
    {
        return chassis;
    }

    public void setChassis(GameObject newChassis)
    {
        chassis = newChassis;
    }




}
