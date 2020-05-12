﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : Wave
{
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }

    public override void SpawnEnemies()
    {
        base.SpawnEnemies();
        Debug.Log("Spawned Enemies");

        for (int i = 0; i < ArrayofEnemies.Length; i++)
        {
            Debug.Log("Spawned Enemies" + i);
            GameObject Enemy = GameObject.Instantiate(ArrayofEnemies[i], spawnLocations[i].position, spawnLocations[i].rotation);
            Enemy.GetComponent<EnemyVarScript>().SpawningInfo(Spawner, gameManager, Random.Range(0, gameManager.GetComponent<GameManagerScript>().numPlayers));
        }

    }
}
