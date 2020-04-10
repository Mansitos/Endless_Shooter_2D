using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Questo script, serve per gestire lo zoom della camera su cui è posizionato.
 * maxZoom e minZoom sono valori float che permettono di decidere il range di zoom possibile.
 * Lo script interviene modificando il valore "orthrographic size" della main.camera.
 * inverted zoom serve a invertire il senso di zoom della rotellina del mouse (l'asse di default per lo zoom),
 * settarlo a -1 per invertirlo.
 */

public class Zoom : MonoBehaviour
{
    [SerializeField] bool enable;
    [SerializeField] float maxZoom;
    [SerializeField] float minZoom;
    [SerializeField] float zoomSpeed;
    [SerializeField] bool invertedZoom;
    [SerializeField] bool debug;

    private int zoomDirection = 1;  // -1 if inverted

    void Start()
    {
        if(invertedZoom == true)
        {
            zoomDirection = -1;
        }
    }

    void Update()
    {
        if (enable == true)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            { // forward zoom
                if (debug)
                {
                    Debug.Log("zoom in");
                }
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - zoomSpeed * zoomDirection,maxZoom,minZoom);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            { // back zoom
                if (debug)
                {
                    Debug.Log("zoom out");
                }
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + zoomSpeed * zoomDirection,maxZoom,minZoom);
            }
        }
    }
}
