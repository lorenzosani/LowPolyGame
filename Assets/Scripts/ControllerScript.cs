using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ControllerScript : MonoBehaviour
{
    public GameObject messageDialog;
    public GameObject messageText;
    public Text rockValue;
    public Text woodValue;
    public Text goldValue;
    public Text villageStrengthValue;
    public Text storageSpaceValue;
    
    private int rockOwned;
    private int woodOwned;
    private int goldOwned;

    private int storageLimit = 40;
    private int villageStrength = 2;
    private int factories = 0;


    void Start(){
        rockOwned = 0;
        woodOwned = 0;
        goldOwned = 0;
    }

    void LateUpdate(){
        rockValue.text = rockOwned.ToString();
        woodValue.text = woodOwned.ToString();
        goldValue.text = goldOwned.ToString();
        villageStrengthValue.text = villageStrength.ToString();
        storageSpaceValue.text = storageLimit.ToString();
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

    public Vector3 GetResources() {
        return new Vector3(rockOwned, woodOwned, goldOwned);
    }

    public void SetResources(Vector3 resources){
        rockOwned = (int) resources.x;
        woodOwned = (int) resources.y;
        goldOwned = (int) resources.z;
    }

    public int getStorageLeft() {
        return storageLimit-goldOwned-rockOwned-woodOwned;
    }

    public void updateStorageSpace(int n) {
        storageLimit += n;
    }

    public int getVillageStrength(){
        return villageStrength;
    }

    public void updateVillageStrength(int n) {
        villageStrength += n;
    }

    public void newFactory(){
        factories++;
        if (factories==1){
            int next = (int) Random.Range(0,60);
            Invoke("produceResources", next);
        }
    }

    private void produceResources(){
        if (getStorageLeft()>0){
            int resType = (int) Random.Range(0,3);
            if (resType==0) { AddWood(1); }
            else if (resType==1){ AddRock(1); }
            else{ AddGold(1); }
        }
        int next = (int) Random.Range(0,100/factories);
        Invoke("produceResources", next);
    }

    public void showDialog(string message){
        messageText.GetComponent<UnityEngine.UI.Text>().text = message;
        messageDialog.gameObject.SetActive(true);
    }

    public void closeDialog(){
        messageDialog.gameObject.SetActive(false);
    }
}
