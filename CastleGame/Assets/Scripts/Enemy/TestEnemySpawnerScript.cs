using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************
 * Anton Ziegler s1907905
 * ****************/
public class TestEnemySpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] [Tooltip("0: will spawn as melee enemy 1: ranged enemy")] GameObject[] enemyArray; //This has been depreciated 
    [SerializeField] Transform[] spawnPoint; //This has as well

    [SerializeField] GameObject gameManager;
    [SerializeField] [Tooltip("For each enmy here you need to put in an object into spawnPoint.")] enemyTypes[] enemyToSpawn; //This is also depreciated

    [SerializeField] int numEnemies; //and so is this
    [SerializeField] Transform[] attackLocations; //this can be handled by the bossaiSpawner.


    enum enemyTypes
    {
        meleeEnemy,
        rangedEnemy,
        bossEnemy,
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
        Enemy.GetComponent<EnemyVarScript>().SpawningInfo(this.gameObject, gameManager, Random.Range(0, gameManager.GetComponent<GameManagerScript>().numPlayers));
       
    }

    public void spawnBoss(Transform spawnPoint, int numInArray)
    {
        GameObject Boss = GameObject.Instantiate(enemyArray[numInArray], spawnPoint.position, spawnPoint.rotation);
        Boss.GetComponent<EnemyVarScript>().SpawningInfo(this.gameObject, gameManager);
      
            foreach(Transform location in attackLocations)
            {
            Boss.GetComponent<MiniBossAi>().AddLocationtoAttack(location);
        }
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
            case enemyTypes.bossEnemy:
                spawnBoss(locationtoSpawn, 2);
                break;
        }
    }

    public void EnemyKilled()
    {
        numEnemies--;
        if (numEnemies == 0)
        {

            Debug.Log("All done");
          //  wallInfront.SetActive(false);
        }
    }
}
