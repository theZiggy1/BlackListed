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
        bullet.transform.Translate(Vector3.forward * Time.deltaTime * speed);

        switch (stateMachine)
        {
            case States.fightingPlayer:
                if (counter == 1)
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
        for(int i = 0; i < numBullets; i++)
        {
            Vector3 position = RandomCircle(center, 2.0f);
            Quaternion rotation = Quaternion.LookRotation(position, center);
            Instantiate(bullet, position, rotation);
            
        }
    }

    // create a random circle
    private Vector3 RandomCircle(Vector3 center, float radius)
    {
        float angle = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        return pos;
    }
}
