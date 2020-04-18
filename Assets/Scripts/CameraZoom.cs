using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{  
    public float zoomStrength = 5.0f;
    public float zoomSpeed = 10.0f;
    public Camera cam;

    private float targetZoom;
    private float scrollData;

    void Start(){
        targetZoom = cam.orthographicSize;
    }

    void Update() {
        // Get users' scroll input
        scrollData = Input.GetAxis("Mouse ScrollWheel");

        // Calculate the desired zoom
        targetZoom -= scrollData*zoomStrength;
        targetZoom = Mathf.Clamp(targetZoom, 5f, 12f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime*zoomSpeed);
    }
}
