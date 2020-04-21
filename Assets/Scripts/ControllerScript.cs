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
    public InfoScript info;
    
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
            info.hideInfoScreen();
        } else {
            // Called if the user starts a new game from scratch
            rockOwned = 0;
            woodOwned = 0;
            goldOwned = 0;
            villageStrength = 0;
            GetComponent<BuildingsScript>().newBuilding(0, new Vector3(-22.0f, 0.0f, 8.0f));
            info.showInfoScreen();
        }
    }

    void LateUpdate(){
        rockValue.text = rockOwned.ToString();
        woodValue.text = woodOwned.ToString();
        goldValue.text = goldOwned.ToString();
        villageStrengthValue.text = villageStrength.ToString();
        storageSpaceValue.text = getStorageLeft().ToString();
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

    // Adds n rocks to those owned by the user
    public void AddRock(int n) {
        rockOwned = rockOwned + n;
        rockValue.text = rockOwned.ToString ();
    }

    // Adds n wood to that owned by the user
    public void AddWood(int n) {
        woodOwned = woodOwned + n;
        woodValue.text = woodOwned.ToString ();
    }

    // Adds n gold to that owned by the user
    public void AddGold(int n) {
        goldOwned = goldOwned + n;
        goldValue.text = goldOwned.ToString ();
    }

    // Returns the number of rock owned by the user
    public int GetRock() {
        return rockOwned;
    }

    // Returns the number of wood owned by the user
    public int GetWood() {
        return woodOwned;
    }

    // Returns the number of gold owned by the user
    public int GetGold() {
        return goldOwned;
    }

    // Returns the number of each resource owned by the user in a Vector3
    public Vector3 GetResources() {
        return new Vector3(rockOwned, woodOwned, goldOwned);
    }

    // Set all the resources owned by the user by using a Vector3 of resources
    public void SetResources(Vector3 resources){
        rockOwned = (int) resources.x;
        woodOwned = (int) resources.y;
        goldOwned = (int) resources.z;
    }

    // Returns how much storage space is left
    public int getStorageLeft() {
        return storageLimit-goldOwned-rockOwned-woodOwned;
    }

    // Set how much storage space is left
    public void updateStorageSpace(int n) {
        storageLimit += n;
    }

    // Returns the strength of the village
    public int getVillageStrength(){
        return villageStrength;
    }

    // Set the strength of the village
    public void updateVillageStrength(int n) {
        villageStrength += n;
    }

    // Adds a new building to the list of constructed buildings
    public void AddNewBuilding(int buildingID, Vector3 pos){
        buildings.Add(buildingID);
        buildingsPosition.Add(pos);
    }

    // Adds 1 to the counter of the number of factories built
    public void newFactory(){
        factories++;
        if (factories==1){
            int next = (int) Random.Range(0,60);
            Invoke("produceResources", next);
        }
    }

    // Allows a new factory to produce resources
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

    // This shows a standard message dialog, the message to be shown is passed as a parameter
    public void showDialog(string message){
        messageText.GetComponent<UnityEngine.UI.Text>().text = message;
        messageDialog.gameObject.SetActive(true);
    }

    // This closes an open message dialog
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
