using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************
 * Anton Ziegler s1907905
 * ****************/
public class WaveSpawning : MonoBehaviour
{

    public Wave[] waveScripts;
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject[] Doors;
    public int numWaves;
    public int currentWave = 0;
    public int numEnemies;

    [Space(10)]

    [SerializeField]
    private GameObject doorLocker;
        
    // Start is called before the first frame update
    void Start()
    {
        numWaves = waveScripts.Length;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WaveSelector()
    {

        if (currentWave == numWaves)
        {
            // Once waves are done, unlock the room
            doorLocker.GetComponent<LockRoomScript>().UnlockRoom();
        }
        else
        {
            StartCoroutine("WaitUntilWaveStarts", waveScripts[currentWave].waveStartTime); //The delay between when the last wave was, until when this one is. 

        }
    }

    public void EnemyKilled()
    {
        numEnemies--;
        if (numEnemies == 0)
        {

            StartCoroutine("WaitUntilWaveEnds", waveScripts[currentWave].waveEndTime);
        }
    }

    public void playersHaveEntered()
    {
        foreach (GameObject door in Doors)
        {
            door.SetActive(true);
        }
        //Also need to enable the objects that keep the players from leaving the area here. 
        WaveSelector();
    }
    //Call this when we want to start the next wave. it will call the start wave function, and at the end, call spawnenemies on the wave. 
    IEnumerator WaitUntilWaveStarts(float timeToWait)
    {
        waveScripts[currentWave].OnStartWave();
        yield return new WaitForSeconds(timeToWait);
        waveScripts[currentWave].setInfo(this.gameObject, this.gameManager);
        waveScripts[currentWave].SpawnEnemies();
        numEnemies = waveScripts[currentWave].ArrayofEnemies.Length;
    }

    //When we get to 0 enemies this will get called before the next wave is called, in case we want to do anything before the wave ends. at the end it will call a function that selects the next wave. 
    IEnumerator WaitUntilWaveEnds(float timeToWait)
    {
        waveScripts[currentWave].OnEndWave();
        yield return new WaitForSeconds(timeToWait);
        currentWave++;
        WaveSelector();
    }
}
