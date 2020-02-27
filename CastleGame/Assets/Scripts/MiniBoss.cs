using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///MiniBossScript
/// Catherine Burns
/// 19/02/2020
/// </summary>
public class MiniBoss : MonoBehaviour
{
    [SerializeField] States stateMachine;
    [SerializeField] private GameObject bullet;
    [SerializeField] private int numBullets;
    [SerializeField] private float speed;

    private Rigidbody rigidBody;
    private int counter;
    
    // miniboss game states in the state machine
    enum States
    {
        fightingPlayer,
        attackedByPlayer,
        numStates
    }

    void Start()
    {
        stateMachine = States.fightingPlayer;
        counter = 0;
    }

    void Update()
    {
        counter++;

        foreach(Transform enemyFire in transform)
        {
            if(enemyFire.name == "EnemyFire")
            {
                // make the bullets move
                enemyFire.transform.Translate(transform.right * Time.deltaTime * speed);
            }
        }

        switch (stateMachine)
        {
            case States.fightingPlayer:
                if (counter % 100 == 0)
                {
                    SpawnBullets();
                }
                break;

            case States.attackedByPlayer:

                break;

            default:
                break;
        }
    }

    private void SpawnBullets()
    {
        Vector3 center = transform.position;

        //
        for (int i = 0; i < numBullets; i++)
        {
            Vector3 position = RandomCircle(center, 2.0f);
            Quaternion rotation = Quaternion.LookRotation(position, center);

            //Instantiate bullets as the miniboss children and give them the name "EnemyFire"
            GameObject enemyFire = Instantiate(bullet, position, rotation);
            enemyFire.transform.parent = gameObject.transform;
            enemyFire.name = "EnemyFire";
        }
    }

    // create a random circle
    private Vector3 RandomCircle(Vector3 center, float radius)
    {
        float angle = Random.value * 360;

        // vector 3 to store the x,y,z positions of the circle
        Vector3 position;
        position.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        position.y = center.y;
        position.z = center.z + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        return position;
    }
}
