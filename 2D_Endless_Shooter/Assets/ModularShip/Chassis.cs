using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chassis : MonoBehaviour
{
    public string name;
    public int lifepoints = 200;

    public GameObject[] weaponsSlots;
    public GameObject[] enginesSlots;

    public GameObject[] weapons;
    public GameObject[] engines;

    public GameObject wings;
    public float wingsXoffsetPosToCenter = 0;
    public float wingsYoffsetPosToCenter = 0;



    void Start()
    {
        GetWingsWeaponsSlots();
        GetWeaponsIstances();
        //GetGadgetsIstances();
        GetWeaponsInstances();
        InitializeWingsPositions();

        IstantiateSlotObjects();
    }

    void InitializeWingsPositions()
    {
        wings.transform.position = new Vector3(0,wingsYoffsetPosToCenter,0);
        wings.GetComponent<Wings>().setXOffset(wingsXoffsetPosToCenter);

    }

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

    public void GetWingsWeaponsSlots()
    {
        GameObject[] wingsSlots = wings.GetComponent<Wings>().getWeaponsSlots();

        var newArray = new GameObject[wingsSlots.Length + weaponsSlots.Length];
        weaponsSlots.CopyTo(newArray, 0);
        wingsSlots.CopyTo(newArray, weaponsSlots.Length);

        weaponsSlots = newArray;

    }

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

    void GetWeaponsInstances()
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

    void Update()
    {    
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
