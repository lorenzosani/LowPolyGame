using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerScript : MonoBehaviour
{
    public Text rockValue;
    public Text woodValue;
    public Text goldValue;
    
    private int rockOwned;
    private int woodOwned;
    private int goldOwned;

    public GameObject buildingsMenu;
    private bool buildingsMenuOpen;

    public Transform houseImage;
    public GameObject housePrefab;
    private Vector3 houseInitialPosition;
    private bool placing;

    private bool attacked = false;
    private bool attacking = false;
    public GameObject attackingShip;
    public GameObject attackingDialog;
    public GameObject attackResultDialog;

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

        if (woodOwned >= 5 & !attacked) {
            attackingShip.gameObject.SetActive(true);
            attackingDialog.gameObject.SetActive(true);
            attacked = true;
            attacking = true;
        }
        if (attacking & Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 12)
                {
                    attackResultDialog.transform.GetChild(0).GetComponent<Text>().text = generateAttackOutcome();
                    attackResultDialog.gameObject.SetActive(true);
                    attackingDialog.gameObject.SetActive(false);
                    attackingShip.gameObject.SetActive(false);

                }
            }
        }
        rockValue.text = "= " + rockOwned.ToString ();
        woodValue.text = "= " + woodOwned.ToString ();
        goldValue.text = "= " + goldOwned.ToString ();

    }

    public void AddRock(int n)
    {
        //Debug.Log("ROCK ADDED");
        rockOwned = rockOwned + 1;
        rockValue.text = "= " + rockOwned.ToString ();

    }

    public void AddWood(int n)
    {
        woodOwned = woodOwned + 1;
        woodValue.text = "= " + woodOwned.ToString ();

    }

    public void AddGold(int n){
        goldOwned = goldOwned + 1;
        goldValue.text = "= " + goldOwned.ToString ();
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
            woodOwned -= 4;
        }
    }

    private string generateAttackOutcome() {
        int rockStolen = (int) UnityEngine.Random.Range(1.0F, (float) rockOwned);
        int woodStolen = (int) UnityEngine.Random.Range(1.0F, (float) woodOwned);
        int goldStolen = (int) UnityEngine.Random.Range(1.0F, (float) goldOwned);

        rockOwned -= rockStolen;
        woodOwned -= woodStolen;
        goldOwned -= goldStolen;

        return string.Format("Your village has been attacked by pirates! You lost {0} rock, {1} wood, {2} gold.", rockStolen, woodStolen, goldStolen);
    }
}
