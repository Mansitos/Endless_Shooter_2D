using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    /*  Script per la gestione del componente SLOT per la ship modulare
    */

    public bool WeaponSlot = false;
    public bool EngineSlot = false;
    public bool GadgetSlot = false;

    public GameObject objectInside;         // istanza dell'oggetto (prefab) istanziato
    public GameObject prefabOfObjectInside; // prefab dell'oggetto assegnato

    public bool debug = false;
    public GameObject icon;

    void Start()
    {
        if (!debug) {Destroy(icon);}

        checkType();

    }

    void Update()
    {       
    }

    // controlla che l'oggetto assegnato coincida con il tipo di slot; ad es: un weaponslot (weaponSlot = true) non può avere objectInside di tipo Engine.
    public bool checkType()
    {
        string type = prefabOfObjectInside.tag;
        if(type == "weapon" && WeaponSlot == true)
        {
            return true;
        }
        else if(type == "engine" && EngineSlot == true)
        {
            return true;
        }
        else if(type == "gadget" && GadgetSlot == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Istanzia gli oggetti ad esso assegnato.
    public void IstantiateObject()
    {
        if (prefabOfObjectInside != null && checkType())
        {
            objectInside = Instantiate(prefabOfObjectInside, this.transform.position, this.transform.rotation);
            objectInside.transform.parent = this.transform;
        }
        else if(checkType() == false)
        {
            Debug.Log("[ERRORE] -> in questo slot c'è un oggetto assegnato del tipo errato");
        }
    }

    public GameObject getObjectInside()
    {
        return objectInside;
    }

    public bool isEmpty()
    {
        if(objectInside != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
