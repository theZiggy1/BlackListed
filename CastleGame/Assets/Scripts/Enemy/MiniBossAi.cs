﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossAi : MonoBehaviour
{

    enum States
    {
        Phase1Slow,
        Phase2Fast,
        Phase3Enraged,
        Invulnerable,
        ChangingPhases,
        numStates
    }

    enum Attacks
    {
        Swipe, 
        ThrowRock,
        GroundPound,
        MultiSwipe,
        MultiThrow,
        MultiPound,
        numStates
    }

   [SerializeField] States stateMachine; //thestart of our enumStateMachine
    [SerializeField] Attacks currentAttack;

    //These are how each of the attacks is handled, even if its just a collider with an animation played over it. 
    [SerializeField] GameObject groundAttack; 
    [SerializeField] GameObject rockAttack;
    [SerializeField] GameObject swipeAttack;

    [SerializeField] EntityScript entityScript; //This reference makes handling invulnerability easier as well. 
    //This is where the attacks will be performed. The ground attack lets out 4 attacks, in each direction, so it holds an array of the locations
    [SerializeField] Transform meleeSpawn;
    [SerializeField] Transform rangedSpawn;
    [SerializeField] Transform[] groundSpawn;

    //After health drops to these numbers, it changes phases to the next one. 
    //A third phase threshold isnt needed as lower then thirs phase is the boss being dead.
    [SerializeField] float phase1Threshhold;
    [SerializeField] float phase2Threshhold;


    public float attackCoolDown = 1.0f; //How long until the miniboss decides what attack to perform.
    public bool isAttacking; //if the boss is in the middle of attacking we have no need to keep checking other things. 
    public float InVulnerableTime = 3.0f;


    public List<Transform> groundAttackLocations;

    public bool MoveTo = false;
    public bool performingAttack = false;
    public int attackNum;
    public int playerToAttack;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = States.Phase1Slow;
        currentAttack = Attacks.Swipe;
    }

    // Update is called once per frame
    void Update()
    {
        float entityHealth = entityScript.GetCurHealth();
      //If the boss is in these states, then we know to only check on these phases. if the health is below the thresholds. 
        if (stateMachine == States.Phase1Slow)
        {
            if (entityHealth <= phase1Threshhold && entityHealth > phase2Threshhold)
            {
                stateMachine = States.Invulnerable;
                InVulnerableTime = 1.0f;
            }
        }
        else if(stateMachine == States.Phase2Fast)
        {
             if (entityHealth <= phase2Threshhold)
            {
                stateMachine = States.Invulnerable;
                InVulnerableTime = 5.0f;
            }
        }
        
          switch(stateMachine)
        {
            case States.Invulnerable:

                Debug.Log("Cant touch this");
                StartCoroutine(InvlunerablePhase(InVulnerableTime));
                //This phase starts the transitions from going to one phase to another to help ease moving around. 
                break;
            case States.ChangingPhases:

                Debug.Log("Animation Phase");
                //This is an empty phase that we go to so the miniboss can perform animations in peace.
                break;
        }

        if (isAttacking)
        {
            switch(currentAttack)
            {
                case Attacks.Swipe:
                    break;
                case Attacks.ThrowRock:
                    break;
                case Attacks.GroundPound:
                    //Calls one of two functions
                    //a move to location, with information provided in advance. 

                    if(MoveTo == true)
                    {
                        isMovingTo(groundAttackLocations[attackNum], 10.0f);
                    }
                    else if(performingAttack == false)
                    {
                        StartCoroutine(Attack1GroundPound(1, 2.0f));
                    }

                    break;
                case Attacks.MultiThrow:
                    break;
                case Attacks.MultiSwipe:
                    break;
                case Attacks.MultiPound:
                    break;
            }
            //Will call the attack code here,before returning. 
            return;
        }


        if (attackCoolDown >= 0.0f)
        {
            attackCoolDown -= Time.deltaTime;
            return;
        }

        int chooseRandomAttack = Random.Range(0, 100);
        

        Debug.Log(attackNum);
        Debug.Log(chooseRandomAttack);
        //This statement only decides which attack is being perfromed, the attacks themselves are called elsewhere.
        //The game decides on a rando m number between 0 and 99. It then based on that number chooses an attack
        switch (stateMachine)
        {
            case States.Phase1Slow:
                //In the first stage, if the number is under 40 it chooses the first option
                //If its between 40 and 80 it chooses the second option
                //and if its over 80 it chooses the third. 
                if(chooseRandomAttack > 40)
                {
                    currentAttack = Attacks.GroundPound;
                    MoveTo = true;
                    isAttacking = true;
                    attackNum = Random.Range(0, groundAttackLocations.Count);
                }
                else if(chooseRandomAttack>80 || chooseRandomAttack <= 40)
                {

                }
                else
                {

                }
                Debug.Log("Going Slow");
                break;
            case States.Phase2Fast:
                //In thisgroup, the rate at which each attack happens changes. 
                if (chooseRandomAttack > 40)
                {

                }
                else if (chooseRandomAttack > 80 || chooseRandomAttack <= 40)
                {

                }
                else
                {

                }
                Debug.Log("Going Fast");
                break;
            case States.Phase3Enraged:
                //In this group the attack patterns change to do more of the other ones, over and over. 
                if (chooseRandomAttack > 40)
                {

                }
                else if (chooseRandomAttack > 80 || chooseRandomAttack <= 40)
                {

                }
                else
                {

                }
                Debug.Log("Im mad");
                break;
        }

    }

    IEnumerator Attack1GroundPound(float timeTioWait, float a_attackCoolDown) //This attack creates 4 walls that the players must jump over.
    {
        Debug.Log("Starting Attack");
        performingAttack = true;
        yield return new WaitForSeconds(timeTioWait);
        float forceStrenght = 100.0f;
        for (int x = 0; x< groundSpawn.Length; x++)
        {
            GameObject attack = GameObject.Instantiate(groundAttack, groundSpawn[x].position, groundSpawn[x].rotation);
            attack.GetComponent<Rigidbody>().AddForce(groundSpawn[x].forward * forceStrenght);
            Destroy(attack, 4.0f);
            Debug.Log("Spawned Wall " + x);
        }
        attackCoolDown = a_attackCoolDown;
        isAttacking = false;
        performingAttack = false;
    }

    void Attack2ThrowRock() //This attack has the boss throw a rock at the current location of a randomly chosen player
    {

    }

    //The boss moves slowly in the first phase, but moves more quickly, and does this attack more in the second phase. 
    void Attack3Melee() //The boss moves quickly towards a current player, and then when close enough swipes at the player
    {

    }
    //The two multi attacks are for the third phase of the boss. 

    void Attack4MultiThrow() //The boss performs the Throw rock attack multiple times, between x-y times, at randomly chosen players
    {

    }

    void Attack5MultiSwipe() //The boss rushes a random player and swipes at them x-y times. 
    {

    }

    void Attack6MultiPound()
    {

        //Call a coroutine a bunch, with intervals set.
    }

    void isMovingTo(Transform Location, float speed)
    {
        Debug.Log("isMoving");
        this.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, Location.position, Time.deltaTime * speed);
        this.transform.LookAt(Location);

        float distance = Vector3.Distance(Location.position, this.transform.position);

        if(distance <= 0.5f)
        {
            Debug.Log("Done Moving");
            MoveTo= false;
            
        }
    }
    IEnumerator InvlunerablePhase(float timeTioWait)
    {
        entityScript.SetInvulnerable(true);
        stateMachine = States.ChangingPhases;
        yield return new WaitForSeconds(timeTioWait);
        entityScript.SetInvulnerable(false);
        float entityHealth = entityScript.GetCurHealth();
        if(entityHealth<= phase1Threshhold && entityHealth > phase2Threshhold)
        {
            stateMachine = States.Phase2Fast;
        }
        else if (entityHealth <= phase2Threshhold)
        {
            stateMachine = States.Phase3Enraged;
        }
    }

    public void AddLocationtoAttack(Transform location)
    {
        Debug.Log("added attack location");
        groundAttackLocations.Add(location);
    }
}
