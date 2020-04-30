using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chassis : MonoBehaviour
{
    /*  Script per la gestione del componente CHASSIES per la ship modulare
    */

    public string name;
    public int lifepoints = 200;

    public GameObject[] weaponsSlots;   // lista di slot per weapons presenti nello chassie
    public GameObject[] enginesSlots;   // lista di slot per engines presenti nello chassie

    public GameObject[] weapons;    // lista di weapons montate
    public GameObject[] engines;    // lista di engines montati

    public GameObject wings;        // istanza del sotto-componente wings
    public GameObject leftWingAttachPoint;  // punti di attracco per le wings, sinistro e destro
    public GameObject rightWingAttachPoint;


    void Start()
    {
        GetWingsWeaponsSlots();

        IstantiateSlotObjects();

        GetWeaponsIstances();
        GetGadgetsIstances();
        GetEnginesInstances();

        InitializeWingsPositions();
    }

    void Update()
    {
    }

    public void GetGadgetsIstances()
    {
        // todo, gadgets mechenics -> shields etc.
    }

    // posiziona le wings nella posizione corretta rispetto allo chassie e i suoi attrach point
    void InitializeWingsPositions()
    {
        wings.GetComponent<Wings>().InitializeWingsPositions(leftWingAttachPoint.transform, "left");
        wings.GetComponent<Wings>().InitializeWingsPositions(rightWingAttachPoint.transform, "right");
    }

    // una volta assemblata la ship nel modo corretto, istanzia tutti gli oggetti (armi, motori etc.) presenti nei vari slot (qualora fossero presenti = slot non vuoti)
    public void IstantiateSlotObjects()
    {
        for(int i = 0; i<= weaponsSlots.Length-1; i++)
        {
            weaponsSlots[i].GetComponent<Slot>().IstantiateObject();
        }

        for (int i = 0; i <= enginesSlots.Length-1; i++)
        {
            enginesSlots[i].GetComponent<Slot>().IstantiateObject();
        }
    }

    // ottieni un riferimento agli slot delle wings
    public void GetWingsWeaponsSlots()
    {
        GameObject[] wingsSlots = wings.GetComponent<Wings>().getWeaponsSlots();

        var newArray = new GameObject[wingsSlots.Length + weaponsSlots.Length];
        weaponsSlots.CopyTo(newArray, 0);
        wingsSlots.CopyTo(newArray, weaponsSlots.Length);

        weaponsSlots = newArray;
    }

    // ottieni un riferimento agli oggetti posizionati negli slot (per le weapons)
    void GetWeaponsIstances()
    {
        int qnty = weaponsSlots.Length;
        weapons = new GameObject[qnty];

        for(int i = 0; i <= qnty-1; i++)
        {
            if (weaponsSlots[i] != null)
            {
                weapons[i] = weaponsSlots[i].GetComponent<Slot>().getObjectInside();
            }
        }
    }

    // ottieni un riferimento agli oggetti posizionati negli slot (per gli engines)
    void GetEnginesInstances()
    {
        int qnty = enginesSlots.Length;
        engines = new GameObject[qnty];

        for (int i = 0; i <= qnty - 1; i++)
        {
            if (enginesSlots[i] != null)
            {
                engines[i] = enginesSlots[i].GetComponent<Slot>().getObjectInside();
            }
        }
    }

    public int getLifePoints()
    {
        return lifepoints;
    }

    public GameObject[] getWeapons()
    {
        return weapons;
    }

    public GameObject[] getEngines()
    {
        return engines;
    }

    public GameObject getWings()
    {
        return wings;
    }

    public void setWeaponByIndex(int index, Weapon newWeapon)
    {

    }

    public void setEngineByIndex(int index, Engine newEngine)
    {

    }

    public void setWings(Wings newWings)
    {

    }
}
