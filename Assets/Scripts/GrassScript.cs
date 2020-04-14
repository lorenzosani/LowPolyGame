using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassScript : MonoBehaviour
{
    public int amountOfGrass;
    public GameObject[] grassPrefabs;
    public GameObject terrainObject;
    

    // Here I add grass objects to the scene
    void Start()
    {
        RaycastHit hit;
        for(int i=0; i<amountOfGrass; i++){
            // Generate a random position for each object
            int randGrass = Random.Range(0,3);
            Vector3 pos = new Vector3(Random.Range(-20.0f, 30.0f), -0.03f, Random.Range(-25.0f, 30.0f));

            // Check if object is on the terrain, if yes it is created and randomly rotated
            Physics.Raycast(pos, -Vector3.up, out hit);
            if (hit.transform.gameObject == terrainObject){
                GameObject grassObj = (GameObject) Instantiate(grassPrefabs[randGrass], pos, grassPrefabs[randGrass].transform.rotation);
                grassObj.transform.parent = terrainObject.transform;
                Vector3 rot = grassObj.transform.eulerAngles;
                rot.y = Random.Range(0, 360);
                grassObj.transform.eulerAngles = rot;
            }
        }
    }
}
