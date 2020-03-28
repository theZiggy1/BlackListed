using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script has been created to hold all the variables that are needed for all the enemies.
//I concluded that having a script to hold all the information, and then simply creating another script that handles the ai itself would be a better solution.
//Currently if i want to pass information into the miniboss, id have to pass it into the 'EnemyScript" as that holds information like player variables. 
// By splitting them, it lets me write more legible code. 
public class EnemyVarScript : MonoBehaviour
{
    //This script holds a couple generic functions that all enemies will need, such as an onDestroy, and an onSpawn

    public GameObject Spawner; //The object that spawned the enemy.
    public int playerToFight = -1; //If your fighting a specific player, it pulls this from the blackboard.
    public GameObject gameManager; //This is the blackboard.
    public GameObject playerObj; // This is the player we are fighting. 

    private void OnDestroy()
    {
        Spawner.GetComponent<TestEnemySpawnerScript>().EnemyKilled();
        if (playerToFight != -1)
        {
            gameManager.GetComponent<GameManagerScript>().isEngaged[playerToFight] = false;
        }
        else
        {
            Debug.Log("not fighting a player");
        }
    }
   //This one handles the generic enemies, while the second overloaded one handles bosses.
    public void SpawningInfo(GameObject a_spawner, GameObject a_gameManager, int playerNum)
    {
        Spawner = a_spawner;
        gameManager = a_gameManager;
        playerToFight = playerNum;
        playerObj = gameManager.GetComponent<GameManagerScript>().currentPlayers[playerToFight];
    }


    //As bosses are based on using the blackboard, they only generically need this info, not the third, and i have no need to pass in the third variable. 
    public void SpawningInfo(GameObject a_spawner, GameObject a_gameManager)
    {
        Spawner = a_spawner;
        gameManager = a_gameManager;
    }
}
