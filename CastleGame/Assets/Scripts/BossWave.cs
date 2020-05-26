using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWave : Wave
{
    [SerializeField] Transform[] AddSpawnPoints;




    // Update is called once per frame

    public override void SpawnEnemies()
    {
        base.SpawnEnemies();

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
