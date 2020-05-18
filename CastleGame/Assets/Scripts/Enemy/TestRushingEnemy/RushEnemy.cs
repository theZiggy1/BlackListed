using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RushEnemy : MonoBehaviour
{
    [SerializeField] EnemyVarScript inheritedScript;
    [SerializeField] Transform targetLocation;
    public GameObject gameManager;//dep

    [SerializeField] NavMeshAgent thisAgent;
    bool waitingForLocation = true;
    [SerializeField] float timeWaiting = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        ChooseALocation();
        MoveToLocation();
    }

    // Update is called once per frame
    void Update()
    {

        if (CheckifArrived() && waitingForLocation )
        {
            StartCoroutine(WaitforNewTarget(timeWaiting));
        }
    }

   void ChooseALocation()
    {
        //We choose a point for the enemy to go to, using the transforms of the players. 
        int randomPlayer = Random.Range(0, gameManager.GetComponent<GameManagerScript>().numPlayers); //this should min inclusive, max exclusive, so we dont end up picking an element out of the array. 
        targetLocation = gameManager.GetComponent<GameManagerScript>().currentPlayers[randomPlayer].gameObject.transform;
    }

    void MoveToLocation()
    {
        this.transform.LookAt(targetLocation);
        this.thisAgent.destination = targetLocation.position;
        waitingForLocation = true;
    }

    bool CheckifArrived()
    {
        if (this.thisAgent.pathStatus == NavMeshPathStatus.PathComplete && thisAgent.remainingDistance == 0)
        {
            return true;
        }
        return false;
    }

    IEnumerator WaitforNewTarget(float timeToWait)
    {
        waitingForLocation = false;
        yield return new WaitForSeconds(timeToWait);
        ChooseALocation();
        MoveToLocation();
       
    }
}
