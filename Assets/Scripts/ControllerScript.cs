using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ControllerScript : MonoBehaviour
{
    public SettingsScript settings;
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
    private string saveFileName = "/gamedata.fun";

    internal List<int> buildings = new List<int>();
    internal List<Vector3> buildingsPosition  = new List<Vector3>();
    internal int storageLimit = 40;
    internal int villageStrength;
    internal int factories = 0;

    void Start(){
        if (PlayerPrefs.GetInt("LoadScene", 0)>0) {
            // Called if the user wants to load a game
            LoadGame();
        } else {
            // Called if the user starts a new game from scratch
            rockOwned = 0;
            woodOwned = 0;
            goldOwned = 0;
            villageStrength = 0;
            GetComponent<BuildingsScript>().newBuilding(0, new Vector3(-22.0f, 0.0f, 8.0f));
        }
    }

    void LateUpdate(){
        rockValue.text = rockOwned.ToString();
        woodValue.text = woodOwned.ToString();
        goldValue.text = goldOwned.ToString();
        villageStrengthValue.text = villageStrength.ToString();
        storageSpaceValue.text = storageLimit.ToString();
    }

    // This serializes and saves the game to local storage
    public void SaveGame(){
        // This creates a binary file in the system
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + saveFileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        // This serializes the game data into the file
        GameData data = new GameData(this.GetComponent<ControllerScript>());
        formatter.Serialize(stream, data);
        stream.Close();

        settings.hideSettings();
        showDialog("Your game is saved. We'll take care of it!");
    }

    // This loads a saved game from a binary file
    public void LoadGame(){
        string path = Application.persistentDataPath + saveFileName;

        // It first checks if a saved game exists
        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            // If it exists, the game data is deserialized and loaded
            GameData data = (GameData) formatter.Deserialize(stream);
            stream.Close();
            loadData(data);
        } else {
            showDialog("Error! No saved game can be found.");
        }
    }

    public void AddRock(int n) {
        rockOwned = rockOwned + n;
        rockValue.text = rockOwned.ToString ();

    }

    public void AddWood(int n) {
        woodOwned = woodOwned + n;
        woodValue.text = woodOwned.ToString ();

    }

    public void AddGold(int n) {
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

    public void AddNewBuilding(int buildingID, Vector3 pos){
        buildings.Add(buildingID);
        buildingsPosition.Add(pos);
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

    // This loads saved data into the scene
    public void loadData(GameData data){
        storageLimit = data.storageSpace;
        AddRock(data.resourcesOwned[0]);
        AddWood(data.resourcesOwned[1]);
        AddGold(data.resourcesOwned[2]);
        BuildingsScript bs = this.GetComponent<BuildingsScript>();
        for(int i=0; i<data.buildingsType.Length; i++){
            Vector3 pos = new Vector3(data.buildingsPosition[i,0], data.buildingsPosition[i,1], data.buildingsPosition[i,2]);
            AddNewBuilding(data.buildingsType[i], pos);
            bs.newBuilding(data.buildingsType[i], pos);
        }
    }
}
