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

    void Start(){
        rockOwned = 0;
        woodOwned = 0;
        goldOwned = 0;
    }

    void LateUpdate(){
        rockValue.text = rockOwned.ToString ();
        woodValue.text = woodOwned.ToString ();
        goldValue.text = goldOwned.ToString ();
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
