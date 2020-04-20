using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BuildingsScript : MonoBehaviour
{
    public btnFX soundFx;
    public GameObject buildingsParentObject;
    public GameObject buildingsMenu;
    public Transform[] buildingsImages;
    public GameObject[] buildingsPrefabs;
    public Vector3[] buildingsCosts;
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
        //If a building is selected and the mouse is clicked, the building is placed
        if (placing & Input.GetMouseButton(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                //Check if the building is placed on an allowed area
                if (hit.transform.gameObject.layer == 11 && Input.mousePosition.y>=buildingsMenu.GetComponent<RectTransform>().rect.height){
                    // Check if the new building overlaps with existing objects
                    Collider[] hitColliders = Physics.OverlapBox(hit.point, buildingsPrefabs[currentBuilding].transform.localScale/2, buildingsPrefabs[currentBuilding].transform.rotation);
                    if (hitColliders.Length > 4){
                        placing = false;
                        GetComponent<ControllerScript>().showDialog("Oops! You can't place a building on top of another object");
                    }else{
                        //Check if the user has enough resources
                        if(buyBuilding(currentBuilding)){
                            newBuilding(currentBuilding, hit.point);
                            soundFx.BuildingSound();
                        }else{
                            // Otherwise, a message is shown to the user
                            placing = false;
                            GetComponent<ControllerScript>().showDialog("Oops! You don't have enough resources for this building");
                        }
                    }
                } else {
                    placing = false;
                }
                buildingsImages[currentBuilding].position = buildingsInitialPosition[currentBuilding];
            }
        }
    }

    // This instantiates a new building in the scene
    public void newBuilding(int buildingId, Vector3 buildingPos){
        GameObject building = (GameObject) Instantiate(buildingsPrefabs[buildingId], buildingPos, buildingsPrefabs[buildingId].transform.rotation);
        building.transform.parent = buildingsParentObject.transform;
        building.AddComponent<MeshCollider>();
        building.layer = 14;
        addBuildingFunctionality(buildingId);
        GetComponent<ControllerScript>().AddNewBuilding(buildingId, buildingPos);
        placing = false;
    }

    // This opens the list of buildings
    public void showBuildingsMenu(){
        if (buildingsMenuOpen) {
            buildingsMenuOpen = false;
            buildingsMenu.gameObject.SetActive(false);
        } else {
            buildingsMenuOpen = true;
            buildingsMenu.gameObject.SetActive(true);
        }
    }

    // This is called when a user wants to place a new building
    public void placeBuilding(int i){
        placing = true;
        currentBuilding = i;
    }

    //This returns true if the user can afford to buy a given building
    public bool buyBuilding(int buildingID){
        Vector3 resourcesOwned = GetComponent<ControllerScript>().GetResources();
        Vector3 buildingCost = buildingsCosts[buildingID];
        Vector3 resourcesLeft = resourcesOwned - buildingCost;
        if(resourcesLeft.x>=0.0f && resourcesLeft.y>=0.0f && resourcesLeft.z>=0.0f){
            GetComponent<ControllerScript>().SetResources(resourcesLeft);
            return true;
        }
        return false;
    }

    // Each building has its own role, this function enables that role
    private void addBuildingFunctionality(int buildingNumber){
        switch(buildingNumber){
            case 0: GetComponent<ControllerScript>().updateVillageStrength(2); //House
                GetComponent<SpawnerScript>().spawnVillagers(2); 
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