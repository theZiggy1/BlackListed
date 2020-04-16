using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawning : MonoBehaviour
{

    [SerializeField] Wave[] waveScripts;
    [SerializeField] GameObject gameManager;
    public int numWaves;
    public int currentWave = 0;
        
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
            //were done
        }
        else
        {
            StartCoroutine("WaitUntilWaveStarts", waveScripts[currentWave].timeUntilWave);
        }
    }

    public void EnemyKilled()
    {
        //When enemies of current weave get to 0, call Waituntilwave ends. 
    }

    public void PlayersHaveEntered()
    {

    }
    //Call this when we want to start the next wave. it will call the start wave function, and at the end, call spawnenemies on the wave. 
    IEnumerator WaitUntilWaveStarts(float timeToWait)
    {
        waveScripts[currentWave].OnStartWave();
        yield return new WaitForSeconds(timeToWait);
        waveScripts[currentWave].SpawnEnemies();
    }

    //When we get to 0 enemies this will get called before the next wave is called, in case we want to do anything before the wave ends. at the end it will call a function that selects the next wave. 
    IEnumerator WaitUntilWaveEnds(float timeToWait)
    {
        waveScripts[currentWave].OnEndWave();
        yield return new WaitForSeconds(timeToWait);
        currentWave++;
    }
}
