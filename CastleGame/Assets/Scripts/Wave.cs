using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wave : MonoBehaviour
{
    public string WaveInfo; //adding a bunch of waves to an object can get a little complex, so this makes it a bit easier to keep track
    public float waveStartTime = 0.0f; //The delay between when the last wave was, until when this one is. 
    public float waveEndTime = 0.0f; //Adding this in case we want to run cinematics or somethning at the end of the wave.
    public int numEnemies = 0; //how many enemies are in the wave. 
    public GameObject[] enemiesToSpawn1Player; //the list of enemies to be spawned if there is one player. 
    public GameObject[] enemiesToSpawn2Player; //These all add enemies not in other arrays
    public GameObject[] enemiesToSpawn3Player;
    public GameObject[] enemiesToSpawn4Player;
    public GameObject[] ArrayofEnemies; //This array will be initalized as the array of enemies that need to be put into the game. Instead of trying to control each individual array, we simply set this, and forget about it for the next step
    public Transform[] spawnLocations; //The locations for each enemy to be spawned. 

    public GameObject Spawner;
    public GameObject gameManager;
    public int numPlayers;
 

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Debug.Log("Actually Run");
     //   numEnemies = enemiesToSpawn.Length;
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
        numPlayers = gameManager.GetComponent<GameManagerScript>().numPlayers;
        
        switch(numPlayers)
        {
            case 1:
                ArrayofEnemies = new GameObject[enemiesToSpawn1Player.Length];
                ArrayofEnemies = enemiesToSpawn1Player;
                break;
            case 2:

                ArrayofEnemies = new GameObject[enemiesToSpawn2Player.Length];
                ArrayofEnemies =enemiesToSpawn2Player;
                break;
            case 3:

                ArrayofEnemies = new GameObject[enemiesToSpawn3Player.Length];
                ArrayofEnemies = enemiesToSpawn3Player;
                break;
            case 4:

                ArrayofEnemies = new GameObject[enemiesToSpawn4Player.Length];
                ArrayofEnemies = enemiesToSpawn4Player;
                break;
        }
        numEnemies = ArrayofEnemies.Length;
    }

  


}
