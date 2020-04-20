using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getResource : MonoBehaviour
{
    public btnFX soundFx;
    public GameObject controller;
    public GameObject progressFrontPrefab;
    public GameObject progressBackPrefab;
    private GameObject progressFront;
    private GameObject progressBack;

    private System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
    private bool collectingResource = false;
    private float duration;
    private float startTime;
    private RaycastHit currentHit;
    private int resourceValue;


    void Update(){
        //If the user clicks on a resource it will be collected
        if (Input.GetMouseButtonDown(0)){
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                //Only one resource at a time can be collected
                if (!collectingResource && (hit.transform.gameObject.layer == 8 || hit.transform.gameObject.layer == 9 || hit.transform.gameObject.layer == 10)) {
                    //The value of a resoruce is a function of its size
                    resourceValue = (int) (hit.transform.gameObject.GetComponent<Collider>().bounds.size.z * 2.0);
                    //The resource will be collected only if there is enough storage space
                    if(controller.GetComponent<ControllerScript>().getStorageLeft() >= resourceValue){
                        startProgressBar(hit, resourceValue);
                    } else {
                        controller.GetComponent<ControllerScript>().showDialog("Your storage is full! Build a new Storage Building to hold more resources.");
                    }
                }
                else if(collectingResource && (hit.transform.gameObject.layer == 8 || hit.transform.gameObject.layer == 9 || hit.transform.gameObject.layer == 10)){
                    controller.GetComponent<ControllerScript>().showDialog("Too busy! You can only gather one resource at a time");
                }
            }
        }
    }

    private void FixedUpdate(){
        //This controls the progress bar when a resource is being collected
        if (collectingResource){
            float passedTime = Time.time - startTime;
        if(passedTime < duration){
            Vector3 progress = progressFront.transform.localScale;
            progress.x = 3.0f-(passedTime*3.0f/duration);
            progressFront.transform.localScale = progress;
        } else {
            Destroy(progressFront);
            Destroy(progressBack);
            collectingResource = false;
            addResource();
        }
        }
    }
    
    // This shows a progress bar on top of a resource that is being collected
    private void startProgressBar(RaycastHit hit, int value){
        currentHit = hit;
        Vector3 progressPos = hit.point;
        progressPos.y = 3.0f;

        duration = value*5.0f;
        startTime = Time.time;

        progressBack = (GameObject) Instantiate(progressBackPrefab, progressPos, progressBackPrefab.transform.rotation);
        progressFront = (GameObject) Instantiate(progressFrontPrefab, progressPos, progressFrontPrefab.transform.rotation);
        collectingResource = true;
        soundFx.ResourcesSound();
    }

    // This adds a resource to those owned by the player
    private void addResource(){
        if (currentHit.transform.gameObject.layer == 8){                        
            controller.GetComponent<ControllerScript>().AddRock(resourceValue);
            Destroy(currentHit.transform.gameObject);
        } else if (currentHit.transform.gameObject.layer == 10){
            controller.GetComponent<ControllerScript>().AddGold(resourceValue);
            Destroy(currentHit.transform.gameObject);
        } else if (currentHit.transform.gameObject.layer == 9){
            controller.GetComponent<ControllerScript>().AddWood(resourceValue);
            Destroy(currentHit.transform.gameObject);
        }
    }
}
