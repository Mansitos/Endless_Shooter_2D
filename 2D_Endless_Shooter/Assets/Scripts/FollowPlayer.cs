using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Questo script permette di settare un istanza di "player" che la
 * camera poi seguirà.
 * Se enabled la visuale sarà sempre fixata sul player.
 */

public class FollowPlayer : MonoBehaviour {
    [SerializeField] bool enable;
    [SerializeField] GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (enable == true && player != null)
        {
            UpdatePosition();
        }
	}

    // Chiamata ogni frame. x/y camera = x/y player.
    void UpdatePosition()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}
