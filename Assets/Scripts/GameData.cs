using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {

    internal int villageStrength;
    internal int storageSpace;
    internal int[] resourcesOwned;
    internal int[] buildingsType;
    internal float[,] buildingsPosition;

    // This class holds all the data that will be stored permanently when the game is saved
    public GameData(ControllerScript controller) {
        villageStrength = controller.villageStrength;
        storageSpace = controller.storageLimit;
        resourcesOwned = new int[3]{controller.GetRock(), controller.GetWood(), controller.GetGold()};
        buildingsType = controller.buildings.ToArray();
        buildingsPosition = new float[buildingsType.Length,3];
        for(int i=0; i<buildingsType.Length; i++){
            buildingsPosition[i, 0] = controller.buildingsPosition[i].x;
            buildingsPosition[i, 1] = controller.buildingsPosition[i].y;
            buildingsPosition[i, 2] = controller.buildingsPosition[i].z;
        }
    }
}
