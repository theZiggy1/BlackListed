using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************
 * Anton Ziegler s1907905
 * ****************/
public class BossWave : Wave
    //This wave handles spawning the boss, and making sure the boss has the relevant parts of information, mainly the points to spawn the aditional enemies around the boss. 
{
    [SerializeField] Transform[] AddSpawnPoints;

    [SerializeField] GameObject winManager;
    [SerializeField] AudioSource audioSourceMusic; // The AudioSource playing the background music
    [SerializeField] AudioClip bossMusicIntro;
    [SerializeField] AudioClip bossMusic;
    [SerializeField] GameObject audioChanger;


    // Update is called once per frame

    public override void SpawnEnemies()
    {
        base.SpawnEnemies();
        // Play the boss music
        if (bossMusic != null)
        {
            //audioSourceMusic.clip = bossMusic;
            audioChanger.GetComponent<AudioLoopScript>().ChangeAudio(bossMusicIntro, bossMusic);
            //audioSourceMusic.Play();
        }

        //Spawn the single boss.  
        GameObject FinalBoss = GameObject.Instantiate(ArrayofEnemies[0], spawnLocations[0].position, spawnLocations[0].rotation);
        FinalBoss.GetComponent<EnemyVarScript>().SpawningInfo(Spawner, gameManager, Random.Range(0, gameManager.GetComponent<GameManagerScript>().numPlayers));
        FinalBoss.GetComponent<BossPhase3AI>().EnemySpawnPoints = AddSpawnPoints;
        FinalBoss.GetComponent<EntityScript>().SetStartingHealth(1500 * numPlayers);
    }


    //This also plays the cutscene after the boss is dead. 
    public override void OnEndWave()
    {
        base.OnEndWave();
        //Will need to play the dying animation here

        // Finds the win screen manager
        winManager = GameObject.FindGameObjectWithTag("WinManager");
        // Tells it to enable the win screen
        winManager.GetComponent<WinScreenManager>().EnableWinScreen();

        // Tell the music to stop
        audioChanger.GetComponent<AudioLoopScript>().StopAudio();

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
