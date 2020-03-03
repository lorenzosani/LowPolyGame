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
}
