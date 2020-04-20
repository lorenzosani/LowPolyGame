using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    private Vector3 origin;
    private Vector3 delta;
    private bool dragging = false;

    // This allows to move the view by dragging
    void LateUpdate() {
        if (Input.GetMouseButton(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                if (hit.transform.gameObject.layer == 11){
                    delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position;
                    if (!dragging){
                        dragging = true;
                        origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    }
                }
            }
        } else {
            dragging = false;
        }
        if (dragging){
            Camera.main.transform.position = origin-delta;
        }
    }
}
