using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public string WaveInfo; //adding a bunch of waves to an object can get a little complex, so this makes it a bit easier to keep track
    public float waveStartTime = 0.0f; //The delay between when the last wave was, until when this one is. 
    public float waveEndTime = 0.0f; //Adding this in case we want to run cinematics or somethning at the end of the wave.
    public int numEnemies = 0; //how many enemies are in the wave. 
    public GameObject[] enemiesToSpawn; //the list of enemies to be spawned. 
    public Transform[] spawnLocations; //The locations for each enemy to be spawned. 

    public GameObject Spawner;
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
      
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

    public virtual void SpawnEnemies()
    {

    }

    public void setInfo(GameObject spawner, GameObject theManager)
    {
        Spawner = spawner;
        gameManager = theManager;
        numEnemies = enemiesToSpawn.Length;
    }

  


}
