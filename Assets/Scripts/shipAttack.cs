using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipAttack : MonoBehaviour
{   
    
    public GameObject[] ships = new GameObject[3];
    public float attackFrequencyInSeconds = 150.0f;
    public Vector3 attackingShipPosition;
    public GameObject attackingDialog;
    public GameObject attackChoiceDialog;
    public GameObject attackResultDialog;
    public GameObject sea;
    public GameObject buildingsParentObject;
    private bool underAttack = false;
    private int attackerStrength;
    private int villageStrength;
    private int shipSize;

    void Start()
    {
        float nextAttackTime = Random.Range(30, 30+attackFrequencyInSeconds*2);
        Invoke("generateAttack", nextAttackTime);
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
                }
            }
        }
    // TODO:
	//3. SHIP GOES AWAY
    //4. CALCULATE VILLAGE STRENGTH
    }

    private void generateAttack()
    {
        // Generate random attacking ship among available ships
        shipSize = Random.Range(0,ships.Length);
        GameObject ship = ships[shipSize];

        // Spawn attacking ship at given position
        ship = (GameObject) Instantiate(ship, attackingShipPosition, new Quaternion(0,0,0,1));
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

    public void fight() {
        attackResultDialog.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = generateAttackOutcome("fight");
        attackChoiceDialog.gameObject.SetActive(false);
        attackResultDialog.gameObject.SetActive(true);
        underAttack = false;

        //Navigate ship away
        float nextAttackTime = Random.Range(30, 30+attackFrequencyInSeconds*2);
        Invoke("generateAttack", nextAttackTime);
    }

    public void surrender() {
        attackResultDialog.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = generateAttackOutcome("surrender");
        attackChoiceDialog.gameObject.SetActive(false);
        attackResultDialog.gameObject.SetActive(true);
        underAttack = false;

        //Navigate ship away
        float nextAttackTime = Random.Range(30, 30+attackFrequencyInSeconds*2);
        Invoke("generateAttack", nextAttackTime);
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
        int rockStolen = UnityEngine.Random.Range(1, GetComponent<ControllerScript>().GetRock());
        int woodStolen = UnityEngine.Random.Range(1, GetComponent<ControllerScript>().GetWood());
        int goldStolen = UnityEngine.Random.Range(1, GetComponent<ControllerScript>().GetGold());
        GetComponent<ControllerScript>().AddRock(-rockStolen);
        GetComponent<ControllerScript>().AddWood(-woodStolen);
        GetComponent<ControllerScript>().AddGold(-goldStolen);
        if (attackResponse == "fight"){
            // If you lose a fight some of the village might be destroyed
            int buildingsNumber = buildingsParentObject.GetComponentsInChildren<Transform>().Length;
            int buildingsToDestroy = UnityEngine.Random.Range(0,3);
            if (buildingsToDestroy > buildingsNumber || buildingsToDestroy == 0) {
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
}
