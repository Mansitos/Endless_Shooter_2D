using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{

    public GameObject[] UIs_ToHideWhenActive;  // lista degli UI da nascondere mentre l'ui delle opzioni è active

    void Start()
    {
    }

    void Update()
    {   
    }

    public void EnableDisableOptionsMenuUI(bool value)
    {
        gameObject.SetActive(value);

        for (int i = 0; i < UIs_ToHideWhenActive.Length; i++)
        {
            UIs_ToHideWhenActive[i].SetActive(!value);                  
        }

    }
}
