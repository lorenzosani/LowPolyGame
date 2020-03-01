using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getResource : MonoBehaviour
{
    public GameObject controller;

    void Update(){
        if (Input.GetMouseButtonDown(0)){
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                if (hit.transform.gameObject.layer == 8){
                    controller.GetComponent<ControllerScript>().AddRock(1);
                    Destroy(hit.transform.gameObject);
                } else if (hit.transform.gameObject.layer == 10){
                    controller.GetComponent<ControllerScript>().AddGold(1);
                    Destroy(hit.transform.gameObject);
                } else if (hit.transform.gameObject.layer == 9){
                    controller.GetComponent<ControllerScript>().AddWood(1);
                    Destroy(hit.transform.gameObject);
                }
        }
    }
 }
}
