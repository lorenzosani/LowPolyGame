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

    private int randResources;
    private bool stop;
    private float spawnWait;
    private int objectsSpawned = 0;

    void Start() {
        StartCoroutine(waitSpawner());
    }

    void Update() {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
        if (maxObjects <= objectsSpawned){
            stop = true;
        } else {
            stop = false;
        }
    }

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
            Vector3 spawnPos = new Vector3(Random.Range(-26, 33), 0, Random.Range(-22, 30));
            GameObject newResource = Instantiate(resources[randResources], spawnPos + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
            newResource.layer = resType;
            newResource.transform.parent = resourcesParentObject;
            yield return new WaitForSeconds(spawnWait);
        }
    }
}
