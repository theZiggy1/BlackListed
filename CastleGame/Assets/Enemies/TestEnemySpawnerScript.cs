﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemySpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] enemyArray;
    [SerializeField] Transform[] spawnPoint;

    [SerializeField] GameObject gameManager;
    [SerializeField] enemyTypes[] enemyToSpawn;

    [SerializeField] int numEnemies;


    enum enemyTypes
    {
        meleeEnemy,
        rangedEnemy,
        numEnemies
    }

    void Start()
    {
        numEnemies = enemyToSpawn.Length;
        
      
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void playersHaveEntered()
    {
        int spawnPointNum = 0;
        foreach (enemyTypes enemy in enemyToSpawn)
        {
            spawnEnemies(enemy, spawnPoint[spawnPointNum]);
            spawnPointNum++;
        }
    }


    public void spawnEnemy(Transform spawnPoint, int numInArray)
    {
        GameObject Enemy = GameObject.Instantiate(enemyArray[numInArray], spawnPoint.position, spawnPoint.rotation);
        Enemy.GetComponent<EnemyScript>().Spawner = this.gameObject;
        Enemy.GetComponent<EnemyScript>().gameManager = gameManager;
        Enemy.GetComponent<EnemyScript>().playerToFight = Random.Range(0, gameManager.GetComponent<GameManagerScript>().numPlayers);
    }

 


    void spawnEnemies(enemyTypes switchEnemy, Transform locationtoSpawn)
    {
        
        switch(switchEnemy)
        {
            case enemyTypes.meleeEnemy:
                spawnEnemy(locationtoSpawn, 0);
                break;
            case enemyTypes.rangedEnemy:
                spawnEnemy(locationtoSpawn, 1);
                break;
        }
    }

    public void EnemyKilled()
    {
        numEnemies--;
        if (numEnemies == 0)
        {
          //  wallInfront.SetActive(false);
        }
    }
}
