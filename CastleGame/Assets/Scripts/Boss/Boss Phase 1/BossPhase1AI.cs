
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
    bool returnToBase = false;
    [SerializeField] float moveSpeed = 50;
    [SerializeField] float returnToBaseSpeed = 10.0f;
    [SerializeField] float attackPatternTime = 5.0f;
    [SerializeField] float returnToBaseTime = 5.0f;
    void Start()
    {
        StartCoroutine(Test());
    }

    // Update is called once per frame
    void Update()
    {
        if(choseALocation == true)
        {
            MoveTo(chosenPlayerPosition, moveSpeed, true);
        }

        if(returnToBase == true)
        {
            MoveTo(locationToReturn, returnToBaseSpeed, false);
        }
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

        StartCoroutine(AttackPattern(attackPatternTime));
    }

    IEnumerator AttackPattern(float waitTimer)
    {
        ChooseAPlayer();
        SpawnAttack();
        choseALocation = true;
        yield return new WaitForSeconds(waitTimer); //This is how long we want to wait on the ground for
        //At the end of this, we call Return to Home
        choseALocation = false;
        StartCoroutine(ReturntoHome(returnToBaseTime));
    }

    IEnumerator ReturntoHome(float waitTimer)
    {
        returnToBase = true;
        yield return new WaitForSeconds(waitTimer); //This is how long we want to wait in the air for. 
        returnToBase = false;
        //At the end of this we call attack Pattern
        StartCoroutine(AttackPattern(attackPatternTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.tag);
        //Things to do
    // Alos stop anim if you collide with the player. 
        if(collision.collider.tag == "Ground")
        {
            Debug.Log("Touched theGround");
        }
    }
}
