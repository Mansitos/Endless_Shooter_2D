using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Questo script permette di settare un istanza di "player" che la camera poi seguirà.
 * Se enabled = true la visuale sarà sempre fixata sul player.
 */

public class FollowPlayer : MonoBehaviour {

    public bool enable;
    public GameObject player;

	void Start() {	
	}
	
	void Update () {
        if (enable == true && player != null)
        {
            UpdatePosition();
        }
	}

    void UpdatePosition()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}
