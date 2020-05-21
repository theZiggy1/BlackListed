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

        if(Vector3.Distance(this.gameObject.transform.position, location) > 1.0f)
        {
            return true;
        }
        return false;
    }

    void Attack1()
    {
        //Spawn Attacks at location

    }

}
