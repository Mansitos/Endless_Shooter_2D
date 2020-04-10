using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script per l'inizializzazione del RandomSpawnPoint
 */

public class RandomSpawnPoint : MonoBehaviour {

    // PARAMETRI POSIZIONE //
    [SerializeField] float minDistanceToCenter; // 
    [SerializeField] float maxDistanceToCenter; // 

    private float x_coordinate;
    private float z_rotation;

    void Start () {
    ChooseCoordinates();
    MoveToCoordinates();
	}
	
	void Update () {	
	}

    // Sceglie in modo semirandomico la posizione dello spawnpoint.
    void ChooseCoordinates()
    {
        float range = maxDistanceToCenter - minDistanceToCenter;
        x_coordinate = (Random.Range(0, range) + minDistanceToCenter);
        z_rotation = Random.Range(0, 360);
    }

    // Sposta lo spawnPoint alle pos inizializzate.
    void MoveToCoordinates()
    {
        transform.position = new Vector2(x_coordinate*Mathf.Sin(z_rotation), x_coordinate*Mathf.Cos(z_rotation));
    }
}
