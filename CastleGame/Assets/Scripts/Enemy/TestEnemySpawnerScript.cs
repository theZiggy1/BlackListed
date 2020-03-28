﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemySpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] [Tooltip("0: will spawn as melee enemy 1: ranged enemy")] GameObject[] enemyArray;
    [SerializeField] Transform[] spawnPoint;

    [SerializeField] GameObject gameManager;
    [SerializeField] [Tooltip("For each enmy here you need to put in an object into spawnPoint.")] enemyTypes[] enemyToSpawn;

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

        gameManager = GameObject.FindGameObjectWithTag("GameManager"); // Whenever a level loads in, it will find this from the PlayerScene that is loaded before it
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


    public void spawnEnemy(Transform spawnPoint, int numInArray, int stateNum)
    {
        GameObject Enemy = GameObject.Instantiate(enemyArray[numInArray], spawnPoint.position, spawnPoint.rotation);
       // Enemy.GetComponent<EnemyScript>().Spawner = this.gameObject;
       // Enemy.GetComponent<EnemyScript>().gameManager = gameManager;
      //  Enemy.GetComponent<EnemyScript>().playerToFight = Random.Range(0, gameManager.GetComponent<GameManagerScript>().numPlayers);
        Enemy.GetComponent<EnemyScript>().SpawningInfo(this.gameObject, gameManager, Random.Range(0, gameManager.GetComponent<GameManagerScript>().numPlayers), stateNum);
       
    }

 


    void spawnEnemies(enemyTypes switchEnemy, Transform locationtoSpawn)
    {
        
        switch(switchEnemy)
        {
            case enemyTypes.meleeEnemy:
                spawnEnemy(locationtoSpawn, 0, 0);
                break;
            case enemyTypes.rangedEnemy:
                spawnEnemy(locationtoSpawn, 1, 1);
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
