using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wings : MonoBehaviour
{
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

    public void setXOffset(float offset)
    {
        leftWing.transform.position = new Vector3(-offset, 0, 0);
        rightWing.transform.position = new Vector3(offset, 0, 0);
    }
}
