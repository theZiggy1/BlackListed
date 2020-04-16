using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float timeUntilWave = 0.0f; //The delay between when the last wave was, until when this one is. 
    public float timeAtEndofWave = 0.0f; //Adding this in case we want to run cinematics or somethning at the end of the wave.
    public int numEnemies = 0; //how many enemies are in the wave. 
    public GameObject[] enemiesToSpawn; //the list of enemies to be spawned. 
    public Transform[] spawnLocations; //The locations for each enemy to be spawned. 
    // Start is called before the first frame update
    void Start()
    {
        numEnemies = enemiesToSpawn.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //If we want to do anything prior to the wave startin, run cutscenes, etc. 
    public virtual void OnStartWave()
    {

    }

    //Same as for if we want to do anything after the wave.
    public virtual void OnEndWave()
    {

    }

    public void SpawnEnemies()
    {
        for(int i = 0; i < numEnemies; i++)
        {
            //Spawn Enemies here
        }
    }


}
