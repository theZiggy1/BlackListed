
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
    [SerializeField] Transform AttackLocation;
    bool choseALocation = false;
    [SerializeField] float moveSpeed = 50;
    [SerializeField] float returnToBaseSpeed = 10.0f;
    void Start()
    {
        StartCoroutine(Test());
    }

    // Update is called once per frame
    void Update()
    {
        if(choseALocation == true)
        {
            MoveTo(chosenPlayerPosition, moveSpeed, false);
        }
    }

    void Attack()
    {
        //Choose a player to attack, and find its location
        //Place a hitbox in front of the crystal, and move towards that position
        //wait some time before coming back up
       
    }

    void ChooseAPlayer()
    {
        int playerNum = Random.Range(0, gameManager.numPlayers); //inclusive of min, exclusive of max
        Debug.Log(" God damn it" + playerNum);
        Debug.Log(" " + gameManager.numPlayers);
        chosenPlayerPosition = gameManager.currentPlayers[playerNum].gameObject.transform.position;
    }

    void SpawnAttack()
    {
        GameObject Attack = GameObject.Instantiate(AttackObj, AttackLocation.position, AttackLocation.rotation);
        Attack.transform.parent = this.transform;
       // Destroy(Attack, 5.0f);
        //Dont forget to make it a child, so that it can remain in front the whole time. 
        // collision.collider.gameObject.transform.parent = transform;
    }

    void MoveTo(Vector3 location, float speed, bool LookAt)
    {
        this.transform.position = Vector3.MoveTowards(transform.position, location, speed * Time.deltaTime);
        if(LookAt)
        {
            this.transform.LookAt(location);
        }
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(1.0f);

        ChooseAPlayer();
        SpawnAttack();
        choseALocation = true;
    }


}
