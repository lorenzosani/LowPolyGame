using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    private int rockOwned;
    private int woodOwned;
    private int goldOwned;

    public GameObject buildingsMenu;
    private bool buildingsMenuOpen;

    public Transform houseImage;
    public GameObject housePrefab;
    private Vector3 houseInitialPosition;
    private bool placing;
    
    void Start(){
        rockOwned = 0;
        woodOwned = 0;
        goldOwned = 0;

        buildingsMenuOpen = false;
        houseInitialPosition = houseImage.position;
    }

    void LateUpdate(){
        if (placing) {
            houseImage.position = Input.mousePosition;
        }
        if (placing & Input.GetMouseButton(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                if (hit.transform.gameObject.layer == 11){
                    houseImage.position = houseInitialPosition;
                    Instantiate(housePrefab, hit.point, new Quaternion(0,270,0,1));
                    placing = false;
                } else {
                    houseImage.position = houseInitialPosition;
                    placing = false;
                }
            }
        }
    }
    
    public void AddRock(int n){
        rockOwned += n;
    }

    public void AddWood(int n){
        woodOwned += n;
    }

    public void AddGold(int n){
        goldOwned += n;
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

    public void placeBuilding(){
        placing = true;
    }
}
