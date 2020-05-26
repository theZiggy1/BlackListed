using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Spawner;//dep
    public int playerToFight = -1;//dep
    public GameObject gameManager;//dep
    public GameObject playerObj;//dep

    [SerializeField] EnemyVarScript inheritedScript;
    public float damping;
    public float moveSpeed;
    public float numEnemies;

    //the different states in the finite state machine. each is written out in the update loop. 

    enum States
    {
        fightingPlayer,
        flocking,
        attackedByPlayer,
        numStates
    }


    //This denotes the type of enemy you are fighting, melee or ranged, and is part of the nested state machine. 
   public enum fighterType
    {
        melee,
        ranged,
        numStates
    }

   [SerializeField]  States stateMachine;
    public fighterType enemyState;
    public float theta = 0f;
    public float speed = 1.0f;
    public float speed2 = 10.0f;
    public float speedWhileGoing = 0.5f;
    public Vector3 newLocation;
    public Transform tempTransform;
    public float radius = 5;
    public float rangedRadius = 7;
    [SerializeField] GameObject rangedEnemyAttack;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] float forceStrength;
    [SerializeField] NavMeshAgent thisAgent;
    [SerializeField] float rangedAttackCoolDown = 0;
    [SerializeField] float rangedAttackReset = 5;
    public float addMinRandomTheta = -20.0f;
    public float addMaxRandomTheta = 20.0f;
    [SerializeField] float thetaMaxThreshold;
    [SerializeField] float thetaMinThreshold;


    [Space(20)] // 20 pixels of spacing in inspector

    // Animation stuff
    [SerializeField]
    private Animator enemyAnimator;

    [SerializeField]
    private string conditionName;
    
    [SerializeField] int idleAnimation;
    [SerializeField] int walkAnimation;
    [SerializeField] int attackAnimation;
    [SerializeField] int attackAnimationRanged;

    [Space(5)]

    // Audio stuff
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip audioClipAttack;
    [SerializeField]
    private AudioClip audioClipAttackRanged;

    [Space(10)]
    
    public bool MoveTo = false;
    public bool MoveToRanged = false;

    public bool performingAttack = false;



    private void SetAnimationInteger(string condition, int integer)
    {
        enemyAnimator.SetInteger(condition, integer);

        /*  foreach (Animator anim in Animators)
          {
              anim.SetInteger(condition, integer);
          }*/
    }

    GameObject checkClosestPlayer()
    {
        GameObject closestPlayer = playerObj;


        foreach(GameObject player in inheritedScript.gameManager.GetComponent<GameManagerScript>().currentPlayers)
        {
            if (player != null)
            {
                //other position this position
                if (Vector3.Distance(player.transform.position, this.transform.position) < Vector3.Distance(playerObj.transform.position, this.transform.position))
                {
                    closestPlayer = player;
                }
            }
        }
        return closestPlayer;
    }


    void Start()
    {
        stateMachine = States.flocking;
        playerObj = inheritedScript.playerObj;
        playerToFight = inheritedScript.playerToFight;
        gameManager = inheritedScript.gameManager;
        thetaMaxThreshold = inheritedScript.thetaMax;
        thetaMinThreshold = inheritedScript.thetaMin;
        theta += Random.Range(addMinRandomTheta, addMaxRandomTheta);
        // Whenever a level loads in, it will find this from the PlayerScene that is loaded before it

        // Audio stuff
        if (GetComponent<AudioSource>() != null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Debug.Log("Can't find AudioSource on enemy!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Animation for Walking
        //enemyAnimator.Play("Walk");

        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnBullet();
        }
        //This checks the blackboard, which is part of the game manager, to see if the selected player is engaged. It might be changed in the future to check thorugh all players, and see if any of them are missig an engagement. 
        //for now it simply checks the one it has been assigned, and then if the player isnt fighting the first one that checks fights said player. 
        if(stateMachine == States.flocking)
        {
            for(int x = 0; x< gameManager.GetComponent<GameManagerScript>().numPlayers; ++x)
            {
                if (gameManager.GetComponent<GameManagerScript>().isEngaged[x] == false)
                {
                    gameManager.GetComponent<GameManagerScript>().isEngaged[x] = true;
                    stateMachine = States.fightingPlayer;
                    playerObj = gameManager.GetComponent<GameManagerScript>().currentPlayers[x];
                    MoveTo = true;
                    Debug.Log("switching states");
                    break;
                }

         /*   if (gameManager.GetComponent<GameManagerScript>().isEngaged[playerToFight] == false)
            {
                gameManager.GetComponent<GameManagerScript>().isEngaged[playerToFight] = true;
                stateMachine = States.fightingPlayer;
                MoveTo = true;
                Debug.Log("switching states");*/
            }
        }
        Quaternion rotation = Quaternion.LookRotation(playerObj.transform.position - this.transform.position);
        //This is a nested switch case state machine. If the enemy is not engaged with a player its job is to circle around the players, waaiting for a n oppertunity to join in. 
        //If its engaged, based on what kind od enemy it is, melee or ranged, it has different effects,
        //This is also the case if it isnt engaged with a player and is intead attacked by a player. 
        switch (stateMachine)
        {
            case States.flocking:
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * damping);
               if(theta >= thetaMaxThreshold)
                {
                    theta = thetaMaxThreshold;
                    speed *= -1;
                }
                if(theta <= thetaMinThreshold)
                {
                    theta = thetaMinThreshold;
                    speed *= -1;
                }
                
                theta += Time.deltaTime * speed;
              /*  if (theta > 360)
                {
                    theta = 0;
                }*/
                playerObj = checkClosestPlayer();
                if(Vector3.Distance(playerObj.transform.position, this.transform.position) < 4.0f)
                {
                    stateMachine = States.fightingPlayer;
                    enemyState = fighterType.melee;
                    inheritedScript.gotTooClose = true;
                }
                newLocation.x = playerObj.transform.position.x + (radius * Mathf.Cos(theta * Mathf.PI / 180));
                newLocation.y = playerObj.transform.position.y + 1;
                newLocation.z = playerObj.transform.position.z + (radius * Mathf.Sin(theta * Mathf.PI / 180));

                this.transform.position = Vector3.MoveTowards(this.transform.position, newLocation, Time.deltaTime * speed2);

                // Animation for Walking
                //enemyAnimator.Play("Walk");
                SetAnimationInteger(conditionName, walkAnimation);

                break;

            case States.fightingPlayer:
                switch (enemyState)
                {
                    case fighterType.melee:
                        if(MoveTo == true)
                        {
                            isMovingTo( playerObj.transform, 10.0f);
                        }
                        else if (performingAttack == false)
                        {
                            StartCoroutine(meleeAttack(1.0f));
                        }

                     //   this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * damping);
                      //  this.transform.position = Vector3.MoveTowards(this.transform.position,new Vector3(playerObj.transform.position.x, playerObj.transform.position.y + 1, playerObj.transform.position.z), Time.deltaTime * moveSpeed);

                        // Animation for Biting
                        //enemyAnimator.Play("Bite");
                        //SetAnimationInteger(conditionName, attackAnimation);

                        break;
                    case fighterType.ranged:
                        if (MoveToRanged == true)
                        {
                            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * damping);
                            theta += Time.deltaTime * speed;
                            if (theta > 360)
                            {
                                theta = 0;
                            }

                            newLocation.x = playerObj.transform.position.x + (rangedRadius * Mathf.Cos(theta * Mathf.PI / 180));
                            newLocation.y = playerObj.transform.position.y + 1;
                            newLocation.z = playerObj.transform.position.z + (rangedRadius * Mathf.Sin(theta * Mathf.PI / 180));

                            tempTransform.position = newLocation;
                            tempTransform.rotation = playerObj.transform.rotation;
                            
                            isMovingTo(tempTransform, 10.0f);
                            rangedAttackCoolDown -= Time.deltaTime;
                        }

                        if(rangedAttackCoolDown <= 0 && performingAttack == false)
                        {
                            MoveToRanged = false;
                            thisAgent.isStopped = true;
                            StartCoroutine(rangedAttack(1.0f));
                        }


                        // Animation for Ranged Spit
                        //enemyAnimator.Play("Ranged Spit");
                        //SetAnimationInteger(conditionName, attackAnimationRanged);

                        break;
                }
                break;

            case States.attackedByPlayer:

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * damping);
                break;

        }
    }

    //when thsi charcter is destoryed, it updates the blackboard, that it has in fact finished fighting that player
  /*  private void OnDestroy()
    {
        Spawner.GetComponent<TestEnemySpawnerScript>().EnemyKilled();
        gameManager.GetComponent<GameManagerScript>().isEngaged[playerToFight] = false;
    }


    public void SpawningInfo(GameObject a_spawner, GameObject a_gameManager, int playerNum, int type)

    {
      switch(type)
        {
            case 0:
                enemyState = fighterType.melee;
                break;
            case 1:
                enemyState = fighterType.ranged;
                break;
        }

        Spawner = a_spawner;
        gameManager = a_gameManager;
        playerToFight = playerNum;

    }
    */

    public void SpawnBullet()
    {
        GameObject Bullet = GameObject.Instantiate(rangedEnemyAttack, bulletSpawn.position, bulletSpawn.rotation);
        Bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * forceStrength);
        Bullet.GetComponent<bulletScript>().OTHER_TAG = "Player";
        Bullet.GetComponent<bulletScript>().enemyDamage = 20;
        Destroy(Bullet, 4.0f);
    }

    IEnumerator meleeAttack(float timeToWait)
    {
        performingAttack = true;
        this.transform.LookAt(playerObj.transform);
        yield return new WaitForSeconds(timeToWait);
        GameObject Bullet = GameObject.Instantiate(rangedEnemyAttack, bulletSpawn.position, bulletSpawn.rotation);
        Bullet.GetComponent<bulletScript>().OTHER_TAG = "Player";
        Bullet.GetComponent<bulletScript>().enemyDamage = 20;
        Destroy(Bullet, 1.0f);
        MoveTo = true;

        // Animation
        SetAnimationInteger(conditionName, attackAnimation);
        // Audio
        if (audioClipAttack != null && audioSource != null)
        {
            audioSource.clip = audioClipAttack;
            audioSource.Play();
        }

        performingAttack = false;
    }


    IEnumerator rangedAttack(float timeToWait)
    {
        performingAttack = true;

        this.transform.LookAt(playerObj.transform);
        yield return new WaitForSeconds(timeToWait);
        this.transform.LookAt(playerObj.transform);
        SpawnBullet();
        MoveToRanged = true;
        rangedAttackCoolDown = rangedAttackReset;
        
        // Animation
        SetAnimationInteger(conditionName, attackAnimationRanged);
        // Audio
        if (audioClipAttack != null && audioSource != null)
        {
            audioSource.clip = audioClipAttackRanged;
            audioSource.Play();
        }

        performingAttack = false;

    }

    void isMovingTo(Transform location , float speed)
    {
        Debug.Log("isMoving");
        thisAgent.isStopped = false;
        this.thisAgent.destination = location.position;
        this.transform.LookAt(location);

        // Animation
        SetAnimationInteger(conditionName, walkAnimation);

        float distance = Vector3.Distance(location.position, this.transform.position);
        Debug.Log("Distance: " + distance);
        if (distance <= 4.0f)
        {
            Debug.Log("Done Moving");
            MoveTo = false;
            thisAgent.isStopped = true;
        }
    }

   public void BossSpawned()
    {
        stateMachine = States.fightingPlayer;
    }

    public void DestoryedByBoss()
    {
        Destroy(this.gameObject);
    }
}
