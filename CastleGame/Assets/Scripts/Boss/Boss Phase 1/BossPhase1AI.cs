using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossPhase1AI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameManagerScript gameManager;
    [SerializeField] EnemyVarScript enemyVariables;
    [SerializeField] EntityScript Entity;
    [SerializeField] Vector3 locationToReturn;
    [SerializeField] Vector3 chosenPlayerPosition;
    [SerializeField] GameObject AttackObj;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {
        //Choose a player to attack, and find its location
        //Place a hitbox in front of the crystal, and move towards that position
        //wait some time before coming back up
       
    }

    void ChooseAPlayer()
    {

    }

    void SpawnAttack()
    {
        //Dont forget to make it a child, so that it can remain in front the whole time. 
    }

    void MoveTo(Vector3 location)
    {

    }

  
}
