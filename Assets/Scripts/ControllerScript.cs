using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ControllerScript : MonoBehaviour
{
    public Text rockValue;
    public Text woodValue;
    public Text goldValue;
    
    private int rockOwned;
    private int woodOwned;
    private int goldOwned;

    public GameObject buildingsParentObject;
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
                    GameObject building = (GameObject) Instantiate(housePrefab, hit.point, new Quaternion(0,270,0,1));
                    building.transform.parent = buildingsParentObject.transform;
                    building.AddComponent<MeshCollider>();
                    placing = false;
                    woodOwned -= 4;
                } else {
                    houseImage.position = houseInitialPosition;
                    placing = false;
                }
            }
        }
        rockValue.text = rockOwned.ToString ();
        woodValue.text = woodOwned.ToString ();
        goldValue.text = goldOwned.ToString ();
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
        if (woodOwned>=4){
            placing = true;
        }
    }

    public void AddRock(int n)
    {
        rockOwned = rockOwned + n;
        rockValue.text = rockOwned.ToString ();

    }

    public void AddWood(int n)
    {
        woodOwned = woodOwned + n;
        woodValue.text = woodOwned.ToString ();

    }

    public void AddGold(int n){
        goldOwned = goldOwned + n;
        goldValue.text = goldOwned.ToString ();
    }

    public int GetRock() {
        return rockOwned;
    }

    public int GetWood() {
        return woodOwned;
    }

    public int GetGold() {
        return goldOwned;
    }
}
