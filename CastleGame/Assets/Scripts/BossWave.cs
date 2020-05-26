using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWave : Wave
{
    [SerializeField] Transform[] AddSpawnPoints;

    [SerializeField] GameObject winManager;
    [SerializeField] AudioSource audioSourceMusic; // The AudioSource playing the background music
    [SerializeField] AudioClip bossMusic;


    // Update is called once per frame

    public override void SpawnEnemies()
    {
        base.SpawnEnemies();
        // Play the boss music
        if (bossMusic != null)
        {
            audioSourceMusic.clip = bossMusic;
            audioSourceMusic.Play();
        }

        //Spawn the single boss.  
        GameObject FinalBoss = GameObject.Instantiate(ArrayofEnemies[0], spawnLocations[0].position, spawnLocations[0].rotation);
        FinalBoss.GetComponent<EnemyVarScript>().SpawningInfo(Spawner, gameManager, Random.Range(0, gameManager.GetComponent<GameManagerScript>().numPlayers));
        FinalBoss.GetComponent<BossPhase3AI>().EnemySpawnPoints = AddSpawnPoints;
        FinalBoss.GetComponent<EntityScript>().SetStartingHealth(1500 * numPlayers);
    }

    public override void OnEndWave()
    {
        base.OnEndWave();
        //Will need to play the dying animation here

        // Finds the win screen manager
        winManager = GameObject.FindGameObjectWithTag("WinManager");
        // Tells it to enable the win screen
        winManager.GetComponent<WinScreenManager>().EnableWinScreen();

        //In Coroutine, enable things after the yirld return
        StartCoroutine(CanvasFinish(6.0f));

    }

    public override void OnStartWave()
    {
        base.OnStartWave();
        //Will need to play the cutscene here for the boss spawning in 
    }

    IEnumerator CanvasFinish(float waitTimer)
    {
        yield return new WaitForSeconds(waitTimer);
        // spawn things after cutscene here. 
    }
}
