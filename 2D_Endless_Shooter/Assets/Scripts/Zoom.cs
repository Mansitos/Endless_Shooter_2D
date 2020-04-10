using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Questo script, serve per gestire lo zoom della camera su cui è posizionato.
 * maxZoom e minZoom sono valori float che permettono di decidere il range di zoom possibile (inteso come distanza massima e minima).
 * Lo script interviene modificando il valore "orthrographic size" della main.camera.
 * inverted zoom serve a invertire il senso di zoom della rotellina del mouse (l'asse di default per lo zoom),
 * settarlo a true (ovvero zoomDirection = -1) per invertirlo.
 */

public class Zoom : MonoBehaviour
{
    public bool enable;
    public float maxZoom;
    public float minZoom;
    public float zoomSpeed;
    public bool invertedZoom;
    public bool debug;

    private int zoomDirection = 1;  // -1 if inverted

    void Start()
    {
        checkZoomDirection();
    }

    // corregge zoomDirection (-1 or 1) in base al parametro bool invertedZoom
    void checkZoomDirection()
    {
        if (invertedZoom == true){ zoomDirection = -1;}
        else { zoomDirection = 1; }
    }

    void Update()
    {
        // controllo ciclico dell'input della rotellina del mouse, in cerca di input di zoom
        if (enable == true)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward zoom
            {
                if (debug)
                {
                    Debug.Log("Playe zoomed in!");
                }
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - zoomSpeed * zoomDirection,maxZoom,minZoom);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0) // back zoom
            {
                if (debug)
                {
                    Debug.Log("Player zoomed out!");
                }
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + zoomSpeed * zoomDirection,maxZoom,minZoom);
            }
        }
    }
}
