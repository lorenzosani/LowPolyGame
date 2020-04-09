using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BuildingsScript : MonoBehaviour
{
    public GameObject buildingsParentObject;
    public GameObject buildingsMenu;
    public Transform[] buildingsImages;
    public GameObject[] buildingsPrefabs;
    private Vector3[] buildingsInitialPosition = new Vector3[10];
    private bool placing;
    private bool buildingsMenuOpen;
    private int currentBuilding;

    void Start() {
        buildingsMenuOpen = false;
        for (int i=0; i<buildingsImages.Length; i++){
            buildingsInitialPosition[i] = buildingsImages[i].position;
        }
    }

    void LateUpdate() {
        if (placing) {
            buildingsImages[currentBuilding].position = Input.mousePosition;
        }
        if (placing & Input.GetMouseButton(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                if (hit.transform.gameObject.layer == 11 && Input.mousePosition.y>=buildingsMenu.GetComponent<RectTransform>().rect.height){
                    buildingsImages[currentBuilding].position = buildingsInitialPosition[currentBuilding];
                    GameObject building = (GameObject) Instantiate(buildingsPrefabs[currentBuilding], hit.point, buildingsPrefabs[currentBuilding].transform.rotation);
                    building.transform.parent = buildingsParentObject.transform;
                    building.AddComponent<MeshCollider>();
                    building.layer = 14;
                    addBuildingFunctionality(currentBuilding);
                    placing = false;
                    // TODO: Remove used resources
                } else {
                    buildingsImages[currentBuilding].position = buildingsInitialPosition[currentBuilding];
                    placing = false;
                }
            }
        }
    }

    public void showBuildingsMenu(){
        if (buildingsMenuOpen) {
            buildingsMenuOpen = false;
            buildingsMenu.gameObject.SetActive(false);
        } else {
            buildingsMenuOpen = true;
            buildingsMenu.gameObject.SetActive(true);
        }
    }

    public void placeBuilding(int i){
        // TODO: If has enough resources
        placing = true;
        currentBuilding = i;
    }

    private void addBuildingFunctionality(int buildingNumber){
        switch(buildingNumber){
            case 0: Debug.Log("New House added"); //House
                break;
            case 1: GetComponent<ControllerScript>().updateStorageSpace(20); //Storage
                break;
            case 2: GetComponent<ControllerScript>().newFactory(); //Factory
                break;
            default: Debug.Log("Building's functionality not recognized");
                break;
        }
    }
}