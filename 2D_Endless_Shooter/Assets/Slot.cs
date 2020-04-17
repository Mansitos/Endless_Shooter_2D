using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{

    public bool WeaponSlot = false;
    public bool EngineSlot = false;
    public bool GadgetSlot = false;

    public GameObject objectInside = null;  // to configure, actually not used
    public GameObject prefabOfObjectInside = null;

    public bool debug = false;
    public GameObject icon;

    void Start()
    {
        if (!debug) {Destroy(icon);}
    }

    void Update()
    {       
    }

    public void IstantiateObject()
    {
        if (prefabOfObjectInside != null)
        {
            objectInside = Instantiate(prefabOfObjectInside, this.transform.position, this.transform.rotation);
            objectInside.transform.parent = this.transform;
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
