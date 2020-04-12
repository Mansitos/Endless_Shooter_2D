using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Dato un target, l'oggetto su cui è lo script punterà al target, usato per la freccetta di direzione della base
 * 
 */
public class TargetIndicator : MonoBehaviour {

    public GameObject Target;
    public bool enable;
    public float hideRange;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

            var direction = Target.transform.position - transform.position;

            if (direction.magnitude < hideRange)
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
            else if(direction.magnitude > hideRange && enable == true)
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }

                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }

	}
}
