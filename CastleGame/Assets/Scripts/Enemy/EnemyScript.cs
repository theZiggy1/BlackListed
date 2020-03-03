using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Spawner;
    public int playerToFight = -1;
    public float damping;
    public float moveSpeed;
    public float numEnemies;

    //the different states in the finite state machine. each is written out in the update loop. 

    enum States
    {
        fightingPlayer,
        flocking,
        attackedByPlayer,
        numStates
    }


    //This denotes the type of enemy you are fighting, melee or ranged, and is part of the nested state machine. 
   public enum fighterType
    {
        melee,
        ranged,
        numStates
    }

   [SerializeField]  States stateMachine;
    public fighterType enemyState;
   public GameObject gameManager;
    public GameObject playerObj;
    public float theta = 0f;
    public float speed = 1.0f;
    public float speed2 = 10.0f;
    public Vector3 newLocation;
    public float radius = 5;
    public float rangedRadius = 7;
    

    void Start()
    {
        stateMachine = States.flocking;
        playerObj = gameManager.GetComponent<GameManagerScript>().currentPlayers[playerToFight];

    }

    // Update is called once per frame
    void Update()
    {
        //This checks the blackboard, which is part of the game manager, to see if the selected player is engaged. It might be changed in the future to check thorugh all players, and see if any of them are missig an engagement. 
        //for now it simply checks the one it has been assigned, and then if the player isnt fighting the first one that checks fights said player. 
        if(stateMachine == States.flocking)
        {
            if(gameManager.GetComponent<GameManagerScript>().isEngaged[playerToFight] == false)
            {
                gameManager.GetComponent<GameManagerScript>().isEngaged[playerToFight] = true;
                stateMachine = States.fightingPlayer;
                Debug.Log("switching states");
            }
        }
        Quaternion rotation = Quaternion.LookRotation(playerObj.transform.position - this.transform.position);
        //This is a nested switch case state machine. If the enemy is not engaged with a player its job is to circle around the players, waaiting for a n oppertunity to join in. 
        //If its engaged, based on what kind od enemy it is, melee or ranged, it has different effects,
        //This is also the case if it isnt engaged with a player and is intead attacked by a player. 
        switch (stateMachine)
        {
            case States.flocking:
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * damping);
                theta += Time.deltaTime * speed;
                if (theta > 360)
                {
                    theta = 0;
                }

                newLocation.x = playerObj.transform.position.x + (radius * Mathf.Cos(theta * Mathf.PI / 180));
                newLocation.y = playerObj.transform.position.y + 1;
                newLocation.z = playerObj.transform.position.z + (radius * Mathf.Sin(theta * Mathf.PI / 180));

                this.transform.position = Vector3.MoveTowards(this.transform.position, newLocation, Time.deltaTime * speed2);



                break;

            case States.fightingPlayer:
                switch (enemyState)
                {
                    case fighterType.melee:
                        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * damping);
                        this.transform.position = Vector3.MoveTowards(this.transform.position,new Vector3(playerObj.transform.position.x, playerObj.transform.position.y + 1, playerObj.transform.position.z), Time.deltaTime * moveSpeed);
                        break;
                    case fighterType.ranged:

                        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * damping);
                        theta += Time.deltaTime * speed;
                        if (theta > 360)
                        {
                            theta = 0;
                        }

                        newLocation.x = playerObj.transform.position.x + (rangedRadius * Mathf.Cos(theta * Mathf.PI / 180));
                        newLocation.y = playerObj.transform.position.y + 1;
                        newLocation.z = playerObj.transform.position.z + (rangedRadius * Mathf.Sin(theta * Mathf.PI / 180));

                        this.transform.position = Vector3.MoveTowards(this.transform.position, newLocation, Time.deltaTime * speed2);

                        break;
                }
                break;

            case States.attackedByPlayer:

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * damping);
                break;

        }
    }

    //when thsi charcter is destoryed, it updates the blackboard, that it has in fact finished fighting that player
    private void OnDestroy()
    {
        Spawner.GetComponent<TestEnemySpawnerScript>().EnemyKilled();
        gameManager.GetComponent<GameManagerScript>().isEngaged[playerToFight] = false;
    }


    public void SpawningInfo(GameObject a_spawner, GameObject a_gameManager, int playerNum, int type)

    {
      switch(type)
        {
            case 0:
                enemyState = fighterType.melee;
                break;
            case 1:
                enemyState = fighterType.ranged;
                break;
        }

        Spawner = a_spawner;
        gameManager = a_gameManager;
        playerToFight = playerNum;

    }
}
