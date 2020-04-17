using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject mainWeaponProjectile;   // Prefab del proiettile da usare con la main weapon.
    public float mainWeaponFireRate;          // Rateo di fuoco della main weapon. in RPM (colpi al min).
    Coroutine MainWeaponFireCoroutine;        // Reference alla coroutine di shooting della main weapon.
    public GameObject shootingPoint;          // Punto dove istanziare i proiettili sparati


    void Start()
    {    
    }

    void Update()
    {
        WeaponFireInput();
    }

    // Funzione di gestione del processo di fuoco. Rileva gli input e starta/stoppa la coroutine di fuoco.
    private void WeaponFireInput()
    {
        if (Input.GetButtonDown("MainWeaponFire"))
        {
            MainWeaponFireCoroutine = StartCoroutine(AutomaticFire());
        }
        if (Input.GetButtonUp("MainWeaponFire"))
        {
            StopCoroutine(MainWeaponFireCoroutine);
        }
    }

    // Coroutine del firing.
    IEnumerator AutomaticFire()
    {
        while (true)
        {
            Instantiate(mainWeaponProjectile, shootingPoint.transform.position, this.transform.rotation);
            yield return new WaitForSeconds(60 / mainWeaponFireRate);
        }
    }

}
