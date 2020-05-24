﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BossPhase3AI : MonoBehaviour
{
    [SerializeField] NavMeshAgent navAgent;
    [SerializeField] GameManagerScript gameManager;
    [SerializeField] Vector3 chosenPlayerPosition;
    bool performingAttack = false;
    bool choosingAttack = false;
    [SerializeField] Animator charAnimator;
    [SerializeField] Transform[] EnemySpawnPoints;
    [SerializeField] GameObject EnemyAdds;

    [SerializeField] GameObject swordAttackGround;
    [SerializeField] GameObject spinAttack;
    [SerializeField] Transform spinAttackLocation;
    [SerializeField] GameObject swordAttackSky;
    [SerializeField] GameObject handAttack;


    [SerializeField] int idleAnimation;
    [SerializeField] int walkAnimation;
    [SerializeField] int introAnimation;

    [SerializeField] int attack1Animation;
    [SerializeField] int attack2Animation;
    [SerializeField] int attack3Animation;
    [SerializeField] int attack4Animation;
    [SerializeField] int attack5Animation;
    enum States
    {
        Attack1Ground,
        Attack2Adds,
        Attack3SkySword,
        Attack4Rotate,
        Attack5Hand,
        numStates
    }

    [SerializeField] States currentState;
    // Start is called before the first frame update
    //the boss has 5 attacks
    //Attack one is the sword slam (at locaation) where some rocks come out
    //Attack 2 is to summon some minions, this one is easy
    //Skyward sword is also easy
    //360 sword swing is move to location, play anim again. 
    //reach into the ground and spawn hand. 
    //the attacks can be broken into 2 catagories
    // move to location, perform action
    // stand still, spawn prefab (while in an animation)

    void Start()
    {
        SetAnimationInteger("SkeletonKingCondition", introAnimation);
        StartCoroutine(ChooseAttack(5.0f));
    }


    private void SetAnimationInteger(string condition, int integer)
    {
        charAnimator.SetInteger(condition, integer);

      /*  foreach (Animator anim in Animators)
        {
            anim.SetInteger(condition, integer);
        }*/
    }
    // Update is called once per frame
    void Update()
    {
        if(performingAttack)
        {
            return;
        }

        if(choosingAttack)
        {
            return;
        }
       
        
        switch (currentState)
        {
            case States.Attack1Ground:
                if (HasReachedLocation(chosenPlayerPosition))
                {
                    StartCoroutine(Attack1(2.0f));
                }
                break;
            case States.Attack4Rotate:
                if (HasReachedLocation(chosenPlayerPosition))
                {
                    StartCoroutine(Attack4(1.5f));
                }
                break;
                
        }
    }

    //the boss has 5 attacks
    //Attack one is the sword slam (at locaation) where some rocks come out
    //Attack 2 is to summon some minions, this one is easy
    //Skyward sword is also easy
    //360 sword swing is move to location, play anim again. 
    //reach into the ground and spawn hand. 
    //the attacks can be broken into 2 catagories
    // move to location, perform action
    // stand still, spawn prefab (while in an animation)

    void Moveto(Vector3 location, bool LookAt)
    {
        this.navAgent.destination = location;
        if(LookAt)
        {
            this.transform.LookAt(location);
        }
        SetAnimationInteger("SkeletonKingCondition", walkAnimation);
    }

    void ChooseAPlayer()
    {
        int playerNum = Random.Range(0, gameManager.numPlayers); //inclusive of min, exclusive of max
        Debug.Log("PlayerNum" + playerNum);
       // Debug.Log(" " + gameManager.numPlayers);
        chosenPlayerPosition = gameManager.currentPlayers[playerNum].gameObject.transform.position;
    }

    bool HasReachedLocation(Vector3 location) 
    {

        if(Vector3.Distance(this.gameObject.transform.position, location) < 1.0f)
        {
            return true;
        }
        return false;
    }
    //Sword Attack
    IEnumerator Attack1(float waitTimer)
    {
        SetAnimationInteger("SkeletonKingCondition", attack1Animation);
        //Spawn rocks 
        performingAttack = true;
        Debug.Log("ATK 1");
        yield return new WaitForSeconds(waitTimer);
        performingAttack = false;
        Debug.Log(" Done ATK 1");
        StartCoroutine(ChooseAttack(3.0f));
    }
    //Spawn Adds
    IEnumerator Attack2(float waitTimer)
    {

        SetAnimationInteger("SkeletonKingCondition", attack2Animation);
        //Spawn rocks 
        performingAttack = true;
        Debug.Log("ATK 2");
        yield return new WaitForSeconds(waitTimer);
        performingAttack = false;
        Debug.Log(" Done ATK 2");
        StartCoroutine(ChooseAttack(3.0f));
    }
    //Sky sword
    IEnumerator Attack3(float waitTimer)
    {

        SetAnimationInteger("SkeletonKingCondition", attack3Animation);
       // ChooseAPlayer();
       // GameObject LightningStrike = GameObject.Instantiate(swordAttackSky, new Vector3(chosenPlayerPosition.x, 10, chosenPlayerPosition.z),  this.transform.rotation);
        //Spawn rocks 
        performingAttack = true;
        Debug.Log("ATK 3");
        yield return new WaitForSeconds(waitTimer);

       // Destroy(LightningStrike, 3.0f);
        performingAttack = false;
        Debug.Log(" Done ATK 3");
        StartCoroutine(ChooseAttack(3.0f));
    }

    //rotate 
    IEnumerator Attack4(float waitTimer)
    {
        GameObject SpinAttack = GameObject.Instantiate(spinAttack, spinAttackLocation.position, spinAttackLocation.rotation);
        Destroy(SpinAttack, 3.5f);
        SetAnimationInteger("SkeletonKingCondition", attack4Animation);
        //Spawn rocks 
        performingAttack = true;
        Debug.Log("ATK 4");
        yield return new WaitForSeconds(waitTimer);
        
        performingAttack = false;
        Debug.Log(" Done ATK 4");
        StartCoroutine(ChooseAttack(3.0f));
    }

    //Hand
    IEnumerator Attack5(float waitTimer)
    {

        SetAnimationInteger("SkeletonKingCondition", attack5Animation);
        //Spawn rocks 
        performingAttack = true;
        Debug.Log("ATK 5");
        yield return new WaitForSeconds(waitTimer);
        performingAttack = false;
        Debug.Log(" Done ATK 5");
        StartCoroutine(ChooseAttack(3.0f));
    }


    IEnumerator ChooseAttack(float waitTimer)
    {
        SetAnimationInteger("SkeletonKingCondition", idleAnimation);
        Debug.Log("Attack waiting");
        choosingAttack = true;
        yield return new WaitForSeconds(waitTimer);
        choosingAttack = false;
        //Choose an attack, update the state machine, call the correct functions
        int randNum = 4; // Random.Range(1, 6);
        Debug.Log(randNum);
        Debug.Log("CHose an attack");

        switch (randNum)
        {
            case 1:
                currentState = States.Attack1Ground;
                ChooseAPlayer();
                Moveto(chosenPlayerPosition, true);
                break;
            case 2:
                currentState = States.Attack2Adds;
                //This state bypasses having to call functions in update
                StartCoroutine(Attack2(3.0f));
                break;
            case 3:
                currentState = States.Attack3SkySword;
                //This state also bypasses having to call functions in update
                StartCoroutine(Attack3(3.0f));
                break;
            case 4:
                currentState = States.Attack4Rotate;
                ChooseAPlayer();
                Moveto(chosenPlayerPosition, true);
                break;
            case 5:
                currentState = States.Attack5Hand;
                StartCoroutine(Attack5(3.0f));
                //This state as well as the other two bypasses having to call update functions
                break;
        }
    }
}
