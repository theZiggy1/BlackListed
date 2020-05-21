using System.Collections;
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
        ChooseAPlayer();
        Moveto(chosenPlayerPosition, true);
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
                    Debug.Log("Got you");
                }
                break;
            case States.Attack4Rotate:
                if (HasReachedLocation(chosenPlayerPosition))
                {
                    Debug.Log("Got you");
                }
                break;
                
        }


        if(HasReachedLocation(chosenPlayerPosition))
        {
            Debug.Log("Got you");
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
    }

    void ChooseAPlayer()
    {
        int playerNum = Random.Range(0, gameManager.numPlayers); //inclusive of min, exclusive of max
        Debug.Log(" God damn it" + playerNum);
        Debug.Log(" " + gameManager.numPlayers);
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

    IEnumerable Attack1(float waitTimer)
    {
        //Spawn rocks 
        performingAttack = true;
    
        yield return new WaitForSeconds(waitTimer);
        performingAttack = false;
    }

    IEnumerable Attack2(float waitTimer)
    {
        //Spawn rocks 
        performingAttack = true;

        yield return new WaitForSeconds(waitTimer);
        performingAttack = false;
    }
    IEnumerable Attack3(float waitTimer)
    {
        //Spawn rocks 
        performingAttack = true;

        yield return new WaitForSeconds(waitTimer);
        performingAttack = false;
    }
    IEnumerable Attack4(float waitTimer)
    {
        //Spawn rocks 
        performingAttack = true;

        yield return new WaitForSeconds(waitTimer);
        performingAttack = false;
    }

    IEnumerable Attack5(float waitTimer)
    {
        //Spawn rocks 
        performingAttack = true;

        yield return new WaitForSeconds(waitTimer);
        performingAttack = false;
    }


    IEnumerable ChooseAttack(float waitTimer)
    {
        yield return new WaitForSeconds(waitTimer);

        //Choose an attack, update the state machine, call the correct functions
        int randNum = Random.Range(1, 6);
        switch(randNum)
        {
            case 1:
                currentState = States.Attack1Ground;
                ChooseAPlayer();
                Moveto(chosenPlayerPosition, true);
                break;
            case 2:
                currentState = States.Attack2Adds;
                //This state bypasses having to call functions in update
                break;
            case 3:
                currentState = States.Attack3SkySword;
                //This state also bypasses having to call functions in update
                break;
            case 4:
                currentState = States.Attack4Rotate;
                ChooseAPlayer();
                Moveto(chosenPlayerPosition, true);
                break;
            case 5:
                currentState = States.Attack5Hand;
                //This state as well as the other two bypasses having to call update functions
                break;
        }
    }
}
