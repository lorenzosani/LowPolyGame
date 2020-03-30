using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsScript : MonoBehaviour
{       
    public GameObject[] ships = new GameObject[3];
    public Vector3 attackingShipPosition;
    public GameObject attackingDialog;
    public GameObject attackChoiceDialog;
    public GameObject attackResultDialog;
    public GameObject sea;
    public GameObject buildingsParentObject;
    public float attackFrequencyInSeconds = 150.0f;
    private GameObject ship;
    private bool underAttack = false;
    private bool navigatingAway = false;
    private int attackerStrength;
    private int villageStrength;
    private int shipSize;
    private int count = 0;

    void Start()
    {
        float nextAttackTime = Random.Range(30, 30+attackFrequencyInSeconds*2);
        Invoke("generateAttack", nextAttackTime);
    }

    void FixedUpdate()
    {   
        if (navigatingAway) {
            Vector3 target = new Vector3(-160.0f, ship.transform.position.y, -30.0f);
            Vector3 direction = target-ship.transform.position;
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            ship.transform.rotation = Quaternion.RotateTowards(ship.transform.rotation, toRotation, Time.deltaTime * 1.0f);
            ship.GetComponent<Rigidbody>().AddForce(ship.transform.forward*2.0f);
            count++;
            if (count>=1600){
                navigatingAway = false;
                count=0;
                Destroy(ship);
            }
        }
    }

    void LateUpdate()
    {
        if( underAttack & Input.GetMouseButton(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 12)
                {
                    attackChoiceDialog.gameObject.SetActive(true);
                    attackingDialog.gameObject.SetActive(false);
                    hit.rigidbody.isKinematic = false;
                }
            }
        }
    }

    private void generateAttack()
    {
        // Generate random attacking ship among available ships
        shipSize = Random.Range(0,ships.Length);
        ship = ships[shipSize];

        // Spawn attacking ship at given position
        ship = (GameObject) Instantiate(ship, attackingShipPosition, ship.transform.rotation);
        ship.transform.parent = sea.transform;
        ship.AddComponent<MeshCollider>();
        ship.layer = 12;

        // Generate attacker strength
        float randomCoeff = Random.Range(1.0f,2.0f);
        attackerStrength = (int)((float) shipSize*randomCoeff);

        // Warn player that the village is under attack
        attackingDialog.gameObject.SetActive(true);
        underAttack = true;
    }

    public void fightOrSurrender(string userChoice) {
        attackResultDialog.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = generateAttackOutcome(userChoice);
        attackChoiceDialog.gameObject.SetActive(false);
        attackResultDialog.gameObject.SetActive(true);
        underAttack = false;
        moveShipAway();
    }

    private string generateAttackOutcome(string attackResponse) {
        if (attackResponse == "fight" && villageStrength >= attackerStrength) {
            //  Generate victory
            int rockWon = UnityEngine.Random.Range(1, shipSize*15);
            int woodWon = UnityEngine.Random.Range(1, shipSize*15);
            int goldWon = UnityEngine.Random.Range(1, shipSize*15);
            GetComponent<ControllerScript>().AddRock(rockWon);
            GetComponent<ControllerScript>().AddWood(woodWon);
            GetComponent<ControllerScript>().AddGold(goldWon);
            return string.Format("Yeah! You won the fight and received {0} rock, {1} wood, {2} gold.", rockWon, woodWon, goldWon);
        }
        int rockStolen = UnityEngine.Random.Range(0, GetComponent<ControllerScript>().GetRock());
        int woodStolen = UnityEngine.Random.Range(0, GetComponent<ControllerScript>().GetWood());
        int goldStolen = UnityEngine.Random.Range(0, GetComponent<ControllerScript>().GetGold());
        GetComponent<ControllerScript>().AddRock(rockStolen*-1);
        GetComponent<ControllerScript>().AddWood(woodStolen*-1);
        GetComponent<ControllerScript>().AddGold(goldStolen*-1);
        if (attackResponse == "fight"){
            // If you lose a fight some of the village might be destroyed
            int buildingsNumber = buildingsParentObject.GetComponentsInChildren<Transform>().Length-1;
            int buildingsToDestroy = UnityEngine.Random.Range(0,3);
            if (buildingsToDestroy >= buildingsNumber || buildingsToDestroy == 0) {
                return string.Format("Oh no! You lost the fight and the attacker plundered the village. They stole {0} rock, {1} wood, {2} gold but luckly they didn't destroy any building.", rockStolen, woodStolen, goldStolen);
            }
            for (int i = 0; i<buildingsToDestroy; i++) {
                int buildingNo = UnityEngine.Random.Range(0,buildingsNumber-i);
                Destroy(buildingsParentObject.transform.GetChild(buildingNo).gameObject);
            }
            return string.Format("Oh no! You lost the fight and the attacker plundered the village. They stole {0} rock, {1} wood, {2} gold and destroyed {3} buildings.", rockStolen, woodStolen, goldStolen, buildingsToDestroy);
        }
        return string.Format("You surrendered and lost {0} rock, {1} wood, {2} gold.", rockStolen, woodStolen, goldStolen);
    }

    public void closeResultDialog() {
        attackResultDialog.gameObject.SetActive(false);
    }

    public void moveShipAway() {
        navigatingAway = true;

        float nextAttackTime = Random.Range(30, 30+attackFrequencyInSeconds*2);
        Invoke("generateAttack", nextAttackTime);
    }
}
