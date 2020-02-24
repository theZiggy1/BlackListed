using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Spawner;
    public int playerToFight = -1;
    enum States
    {
        fightingPlayer,
        flocking,
        numStates
    }

   [SerializeField]  States stateMachine;
   public GameObject gameManager;

    void Start()
    {
        stateMachine = States.flocking;
    }

    // Update is called once per frame
    void Update()
    {

        if(stateMachine == States.flocking)
        {
            if(gameManager.GetComponent<GameManagerScript>().isEngaged[playerToFight] == false)
            {
                gameManager.GetComponent<GameManagerScript>().isEngaged[playerToFight] = true;
                stateMachine = States.fightingPlayer;
                Debug.Log("switching states");
            }
        }

        switch (stateMachine)
        {
            case States.flocking:
                break;

            case States.fightingPlayer:
                break;

        }
    }

    private void OnDestroy()
    {
        Spawner.GetComponent<EnemySpawnerScript>().EnemyKilled();
        gameManager.GetComponent<GameManagerScript>().isEngaged[playerToFight] = false;
    }
}
