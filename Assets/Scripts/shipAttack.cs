using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipAttack : MonoBehaviour
{   
    public float attackFrequencyInSeconds = 150.0f;
    public GameObject[] ships = new GameObject[3];
    public Vector3 attackingShipPosition;
    public GameObject attackingDialog;
    public GameObject attackResultDialog;

    void Start()
    {
        float nextAttackTime = Random.Range(30, 30+attackFrequencyInSeconds*2);
        Invoke("generateAttack", nextAttackTime);
    }

    void LateUpdate()
    {
    // TODO:
    //1. CLICK ON THE SHIP HIDES THE WARNING
	//2. CLICK ON THAT SHIP WILL PROVIDE YOU INFORMATION ABOUT WHO WON THE FIGHT
	//3. CLICK MAKES THE SHIP GO AWAY

/*
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
*/
    }

    private void generateAttack()
    {
        // Generate random attacking ship among available ships
        int shipSize = Random.Range(0,ships.Length-1);
        GameObject ship = ships[shipSize];

        // Spawn attacking ship at given position
        Instantiate(ship, attackingShipPosition, new Quaternion(0,0,0,1));

        // Warn player that the village is under attack
        attackingDialog.gameObject.SetActive(true);

        float nextAttackTime = Random.Range(30, 30+attackFrequencyInSeconds*2);
        Invoke("generateAttack", nextAttackTime);
    }

/*
    private string generateAttackOutcome() {
        int rockStolen = (int) UnityEngine.Random.Range(1.0F, (float) rockOwned);
        int woodStolen = (int) UnityEngine.Random.Range(1.0F, (float) woodOwned);
        int goldStolen = (int) UnityEngine.Random.Range(1.0F, (float) goldOwned);

        rockOwned -= rockStolen;
        woodOwned -= woodStolen;
        goldOwned -= goldStolen;

        return string.Format("Your village has been attacked by pirates! You lost {0} rock, {1} wood, {2} gold.", rockStolen, woodStolen, goldStolen);
    }
*/
}
