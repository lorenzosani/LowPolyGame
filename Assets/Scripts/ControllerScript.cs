using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    private int rockOwned;
    private int woodOwned;
    private int goldOwned;
    
    void Start(){
        rockOwned = 0;
        woodOwned = 0;
        goldOwned = 0;
    }
    
    public void AddRock(int n){
        rockOwned += n;
    }

    public void AddWood(int n){
        woodOwned += n;
    }

    public void AddGold(int n){
        goldOwned += n;
    }
}
