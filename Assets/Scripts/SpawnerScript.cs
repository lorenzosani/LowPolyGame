using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] trees;
    public GameObject[] rocks;
    public GameObject[] goldStones;
    public GameObject[] villagers;
    public Transform villagersParentObject;
    public Transform resourcesParentObject;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;
    public int maxObjects;
    public LayerMask islandLayer;

    private int randResources;
    private bool stop;
    private float spawnWait;
    private int objectsSpawned = 0;
    private Collider[] hitColliders;

    void Start() {
        StartCoroutine(waitSpawner());
    }

    // This allows resources to be spawned randomly
    void Update() {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
        if (maxObjects <= objectsSpawned){
            stop = true;
        } else {
            stop = false;
        }
    }

    // This spawns n villagers onto the island
    public void spawnVillagers(int amount){
        for(int i=0; i<amount; i++){
            GameObject villager = Instantiate(villagers[Random.Range(0, villagers.Length)], new Vector3(-21.0f, 0.0f, 10.0f), Quaternion.identity);
            villager.transform.parent = villagersParentObject;
        }
    }

    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startWait);

        while (!stop) {
            GameObject[] resources = new GameObject[0];
            int resType = Random.Range(8,11);
            switch(resType){
                case 9: resources = trees;
                    break;
                case 8: resources = rocks;
                 break;
                case 10: resources = goldStones;
                    break;
            }
            randResources = Random.Range(0, resources.Length);
            Vector3 spawnPos;

            // Check if spawned resource overlaps with other objects
            do{
                spawnPos = new Vector3(Random.Range(-26, 33), 0, Random.Range(-22, 30));
                hitColliders = Physics.OverlapBox(spawnPos, resources[randResources].transform.localScale/2, resources[randResources].transform.rotation, islandLayer);
            } while (hitColliders.Length > 3); // This is three because the terrain is made by three different colliders

            // Instantiate a new resource in the scene
            GameObject newResource = Instantiate(resources[randResources], spawnPos + transform.TransformPoint(0, 0, 0), resources[randResources].transform.rotation);
            newResource.layer = resType;
            newResource.transform.parent = resourcesParentObject;
            yield return new WaitForSeconds(spawnWait);
        }
    }
}
