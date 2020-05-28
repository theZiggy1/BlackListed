using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;


/*
 * Script Written by Anton Ziegler s1907905
 * and Farran Holmes s1712383
 */
public class PlayerControllerOldInput : MonoBehaviour
{
    // Input configuration
    [SerializeField]
    public int playerID = -1;
    private string buttonA; // Button 0
    private string buttonB; // Button 1
    private string buttonX; // Button 2
    private string buttonLB; // Button 4
    private string buttonY; // Button 3
    private string buttonRB; // Button 5
    private string buttonBack; // Button 6
    private string buttonStart; // Button 7
    private string buttonLeftStickIn; // Button 8
    private string buttonRightStickIn; // Button 9

    private string leftStickHorizontal; // X axis
    private string leftStickVertical; // Y axis
    private string rightStickHorizontal; // 4th axis
    private string rightStickVertical; // 5th axis
    private string dpadHorizontal; // 6th axis
    private string dpadVertical; // 7th axis
    
    private string LeftTrigger; // 9th axis
    private string RightTrigger; // 10th axis

    CharacterController Controller;
    public List <Animator> Animators;

    Vector2 movementVec; //depreciated from the 'old movement system
    Vector2 rotVec; //see above comment
    [SerializeField] float movespeed = 5f; //a float to denote how fast the character can move. 
    float rotSpeed = 1.0f; //The speed the chcracter is able to rotate in the direction around to where the character is facing. 
    [SerializeField] GameObject thisPlayerChild; //This is child obejct that has been seperated to house the visible portion of the character.
    [SerializeField] float jumpForce; //how hard the player can jump
    [SerializeField] GameObject projectile; //The 'bullet the character can spawn.
    [SerializeField] Transform projectileSpawn; //Thw location of the bullet
    [SerializeField] Transform meleeSpawn; //The location of the melee attack
    [SerializeField] float forceStrength; //How hard the bullet is pushed away from the character
    bool isRangedAttack = true; //whether or not the character is attacking with a ranged attack. This will be depreciated if we implement more then 2 wepaons

    private string GAMEMANAGER_TAG = "GameManager";
    private string GROUND_TAG = "Ground";
    bool canJump = true;
    private GameObject gameManager;


    public int playerNum;
    bool Attacking = false; // the right trigger is in fact an axis, and to keep the player from attacking each frame, once the trigger is depressed, this is called, and not reverted until the trigger is released completely. 
    private bool doingAbility = false;

    // Camera - gets set by the mainCamera in each level
    [SerializeField]
    private Vector3 mainCameraRotation;
    [SerializeField]
    private bool mainCameraSet;

    // Animation stuff
    [SerializeField]
    private Animator playerAnimator;

    [Space(10)]

    [Header("Audio")]
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip audioClipSwordSwing;
    [SerializeField]
    private AudioClip audioClipGunShoot;

    [Space(5)]

    [SerializeField]
    private AudioClip audioClipAttack;
    [SerializeField]
    private AudioClip audioClipAbility;
    [SerializeField]
    private AudioClip audioClipUltra;


    // Death stuff
    [Space(10)]

    [Header("Death")]
    public bool isDead; // If we're dead, our controls will lock
    [SerializeField]
    private GameObject reviveSprite; // Gets set to active when we die

    public GameObject playerInputManager;

    // Ability + Ultra UI stuff
    [Space(10)]

    [Header("UI")]
    [SerializeField]
    private GameObject playerUI; // The UI for this specific player
    public int characterID; // The ID of the character we have - gets set by the playerSelectionScript

    [SerializeField]
    private GameObject gunslingerUI;
    [SerializeField]
    private GameObject knightUI;
    [SerializeField]
    private GameObject rangerUI;
    [SerializeField]
    private GameObject wizardUI;

    [SerializeField]
    private GameObject abilityUI;
    [SerializeField]
    private GameObject ultraUI;

    [SerializeField]
    private GameObject pauseManager;

    [Space(10)]



    //AB Dyeable clothing
    public GameObject clothingPiece; //AB This will be changed to an array at some point - for now just quick implementation

    [SerializeField] BaseClass myclass;

    public bool isTumbling = false;

    [SerializeField] int idleInt;
    [SerializeField] int movingInt;
    [SerializeField] int genericAttackInt;
    [SerializeField] int abilityAttackInt;
    [SerializeField] int ultraAttackInt;
    [SerializeField] int deadInt;
    [SerializeField] int jumpStartInt;
    [SerializeField] int jumpLoopInt;
    [SerializeField] int jumpLandInt;
    [SerializeField] int dodgeStartInt;
    [SerializeField] int dodgeLoopInt;
    [SerializeField] int dodgeLandInt;


    private Animator GetAnimator(string anim_name)
    {
        foreach (Animator anim in Animators)
        {

            //AB If we found the desired animator
            if (anim.transform.gameObject.name == anim_name)
            {
                //AB Let's return the animator
                return anim;
            }

        }

        return null;
    }

    //AB setting animation, instead of setting a condition each time. It goes through each on all animation controller possible settings.
    private void SetAnimationInteger(string condition, int integer)
    {

        
        foreach (Animator anim in Animators)
        {
            anim.SetInteger(condition, integer);
        }
    }

    private void SetAnimationFloat(string condition, float floating_num)
    {
        foreach (Animator anim in Animators)
        {
            anim.SetFloat(condition, floating_num);
        }
    }

    private void SetAnimationBool(string condition, bool boolean)
    {
        foreach (Animator anim in Animators)
        {
            anim.SetBool(condition, boolean);
        }
    }

    //AB Start is called before the first frame update
    void Start()
    {
        //AB Animation setup



        //AB Controller = GetComponent<CharacterController>();

        //AB Let's grab all the animators in the children uwu
        Animator[] animators = gameObject.transform.Find("Brian").gameObject.transform.Find("Clothing").GetComponentsInChildren<Animator>();
        //AB string[] animator_names = { "Trousers", "Boots", "Tunic" };
        string[] animator_names = { "Trousers", "Boots", "Tunic", "Basic Plate Armor", "Wizard Robe", "Dark Knight Armor" };
        //AB Let's iterate through them all cause we just need brians
        foreach (Animator anim in animators)
        {

            //AB If the gameobject of this anim is Brian, then we found the right animator!
            if (animator_names.Contains(anim.transform.gameObject.name))
            {
                //AB Let's yoink this
                Animators.Add(anim);
                //AB Let's exit early we dont like the other animators
            }
        }

        Animators.Add(gameObject.transform.Find("Brian").GetComponent<Animator>());

        transform.Rotate(0, 45, 0); //The character is rotated 45 degrees when spawned in to help with the rotation of the level. it was either this, or leave each section rotated 45 degrees, and due to how the controller takes in input. 

        // Find the Game Manager
        gameManager = GameObject.FindGameObjectWithTag(GAMEMANAGER_TAG); //this is both the game manager and a blackboard for the player ai. 
        // Set ourselves to be in its array
        gameManager.GetComponent<GameManagerScript>().currentPlayers[gameManager.GetComponent<GameManagerScript>().numPlayers] = gameObject; //Updates the blackboard with info that this charcater has been added tot he world. 
        // Then increment that array
        gameManager.GetComponent<GameManagerScript>().numPlayers++; // increment the number of players that have been added to the world. 

        // This then sets our player ID to be how many players there are, e.g. if we are the first player, our playerID is now 1
        // if there's already a player there, our playerID is now 2
        playerID = gameManager.GetComponent<GameManagerScript>().numPlayers;
        playerNum = playerID - 1; //This is important for controller info. 

        // Audio
        audioSource = GetComponent<AudioSource>();


        // Gives us a default value to work with
        mainCameraRotation = new Vector3(0, 45, 0);


        // UI stuff
        SetupUI();
        pauseManager = GameObject.FindGameObjectWithTag("PauseManager");

    }

    private void SetupUI()
    {
        // Gets the UI parent object with the relevant tag
        playerUI = GameObject.FindGameObjectWithTag("Player" + playerID + "UI");

        if (playerUI == null)
        {
            Debug.Log("PlayerUI not set up!");
        }
        else
        {
            foreach (Transform childObject in playerUI.transform)
            {
                if (characterID == 0) // Gunslinger
                {
                    if (childObject.CompareTag("GunslingerUI"))
                    {
                        gunslingerUI = childObject.gameObject;
                        gunslingerUI.SetActive(true);

                        foreach (Transform secondChild in childObject)
                        {
                            if (secondChild.CompareTag("AbilityUI"))
                            {
                                abilityUI = secondChild.gameObject;
                            }
                            if (secondChild.CompareTag("UltraUI"))
                            {
                                ultraUI = secondChild.gameObject;
                            }
                        }
                    }
                }
                if (characterID == 1) // Knight
                {
                    if (childObject.CompareTag("KnightUI"))
                    {
                        knightUI = childObject.gameObject;
                        knightUI.SetActive(true);

                        foreach (Transform secondChild in childObject)
                        {
                            if (secondChild.CompareTag("AbilityUI"))
                            {
                                abilityUI = secondChild.gameObject;
                            }
                            if (secondChild.CompareTag("UltraUI"))
                            {
                                ultraUI = secondChild.gameObject;
                            }
                        }
                    }
                }
                if (characterID == 2) // Wizard
                {
                    if (childObject.CompareTag("WizardUI"))
                    {
                        wizardUI = childObject.gameObject;
                        wizardUI.SetActive(true);

                        foreach (Transform secondChild in childObject)
                        {
                            if (secondChild.CompareTag("AbilityUI"))
                            {
                                abilityUI = secondChild.gameObject;
                            }
                            if (secondChild.CompareTag("UltraUI"))
                            {
                                ultraUI = secondChild.gameObject;
                            }
                        }
                    }
                }
                if (characterID == 3) // Ranger
                {
                    if (childObject.CompareTag("RangerUI"))
                    {
                        rangerUI = childObject.gameObject;
                        rangerUI.SetActive(true);

                        foreach (Transform secondChild in childObject)
                        {
                            if (secondChild.CompareTag("AbilityUI"))
                            {
                                abilityUI = secondChild.gameObject;
                            }
                            if (secondChild.CompareTag("UltraUI"))
                            {
                                ultraUI = secondChild.gameObject;
                            }
                        }
                    }
                }
                
            }

        }


    }

    // Update is called once per frame
    void Update()
    {
        // If we're alive, we can do stuff, otherwise, our controls are locked
        if (!isDead)
        {

            if (isTumbling)
            {
                return;
            }

            //our update loop due to the controller rewrite now handles what was originally sendmessage from the input system. Now we need to manually handle sending in these inputs
            //The first is to mandle movement. We also specifically do it in this order in case the right stick is being engaged to look a different direction, otherwise in movement, the player will naturally look towards the direction the player is walking towards. 

            // With these functions we'll combine it with what direction the character is looking at
            // to determine which direction of walk animation to use

            // Character is moving right (or looking right)
            if ((Input.GetAxis("Joy" + playerID + "LeftStickHorizontal") < -0.1f) || (Input.GetAxis("Joy" + playerID + "RightStickHorizontal") < -0.1f))
            {
            //    Debug.Log("Moving right");

                Movement();
                LookAt();

                // Do the animation for movement
                //playerAnimator.Play("Sword Run Forward");
                SetAnimationInteger("Condition", movingInt);
            }
            // Character is moving left (or looking left)
            else if ((Input.GetAxis("Joy" + playerID + "LeftStickHorizontal") > 0.1f) || (Input.GetAxis("Joy" + playerID + "RightStickHorizontal") > 0.1f))
            {
             //   Debug.Log("Moving left");

                Movement();
                LookAt();

                // Do the animation for movement
                //playerAnimator.Play("Sword Run Forward");
                SetAnimationInteger("Condition", movingInt);
            }
            // Character is moving up (or looking up)
            else if ((Input.GetAxis("Joy" + playerID + "LeftStickVertical") > 0.1f) || (Input.GetAxis("Joy" + playerID + "RightStickVertical") > 0.1f))
            {
              //  Debug.Log("Moving up");

                Movement();
                LookAt();

                // Do the animation for movement
                //playerAnimator.Play("Sword Run Forward");
                SetAnimationInteger("Condition", movingInt);
            }
            // Character is moving down (or looking down)
            else if ((Input.GetAxis("Joy" + playerID + "LeftStickVertical") < -0.1f) || (Input.GetAxis("Joy" + playerID + "RightStickVertical") < -0.1f))
            {
              //  Debug.Log("Moving down");

                Movement();
                LookAt();

                // Do the animation for movement
                //playerAnimator.Play("Sword Run Forward");
                SetAnimationInteger("Condition", movingInt);
            }
            else
            {
                if (!Attacking)
                {
                //    Debug.Log("Not moving");

                    //playerAnimator.Play("Sword Idle");
                    SetAnimationInteger("Condition", idleInt);
                }
                else
                {
                    Debug.Log("We are attacking, so we're not going to revert the animation to idle yet");
                }
            }



            //Handles the attacking from the player. This is called only the first frame, as a bool is set to true, before it can be called again. 
            if (Input.GetAxis("Joy" + playerID + "RightTrigger") != 0.0f)
            {
                if (Attacking == false)
                {
                    // Does the attack
                    OnAttackRightTrigger();
                    // Sets us so we're attacking
                    Attacking = true;
                }
            }
            //This links with the above section. attacking needs to be reset when the player is done pressing the trigger, but as it is a a float value, it needs to check to see if its the whole way depressed/ 
            if (Attacking == true)
            {
                if (Input.GetAxis("Joy" + playerID + "RightTrigger") == 0.0f)
                {
                    Attacking = false;

                    //SetAnimationInteger("Condition", 0);
                }
            }

            //Handles the ability using from the player. This is called only the first frame, as a bool is set to true, before it can be called again. 
            if (Input.GetAxis("Joy" + playerID + "LeftTrigger") != 0.0f)
            {
                if (doingAbility == false)
                {
                    // Does the attack
                    OnAbilityLeftTrigger(); // Currently doesn't do anything
                    Debug.Log("Perform ability");
                    // Plays the animation
              

                    // Sets us so we're attacking
                    doingAbility = true;
                }
            }
            //This links with the above section. attacking needs to be reset when the player is done pressing the trigger, but as it is a a float value, it needs to check to see if its the whole way depressed/ 
            if (doingAbility == true)
            {
                if (Input.GetAxis("Joy" + playerID + "LeftTrigger") == 0.0f)
                {
                    doingAbility = false;

                    //SetAnimationInteger("Condition", 0);
                }
            }


            //buttons only return true the frame they are called with button down, so the above isnt true here. This lets us handle switching weapons.
            if (Input.GetButtonDown("Joy" + playerID + "ButtonY"))
            {
                OnSwitchWeapon();
               // SetAnimationInteger("Condition", ultraAttackInt);
            }

            //This lets us handle jumping
            if (Input.GetButtonDown("Joy" + playerID + "ButtonA"))
            {
                OnJump();
                Debug.Log(" Jumping");
            }

            // Ability + Ultra UI stuff
            // If Ability has finished cooldown
            if (myclass.abilityCoolDown <= 0)
            {
                if (abilityUI != null)
                {
                    //if (!abilityUIOn)
                    //{
                    abilityUI.SetActive(true);
                    //abilityUIOn = true;
                    //}
                }
            }
            else
            {
                if (abilityUI != null)
                {
                    abilityUI.SetActive(false);
                }
            }
            if (myclass.ultraCoolDown <= 0)
            {
                if (ultraUI != null)
                {
                    //if (!ultraUIOn)
                    //{
                    ultraUI.SetActive(true);
                    //ultraUIOn = true;
                    //}
                }
            }
            else
            {
                if (ultraUI != null)
                {
                    ultraUI.SetActive(false);
                }
            }

            
        }
        else // We've died so we need to play the death animation
        {
            // Sets us back to idle
            SetAnimationInteger("Condition", idleInt);
            // Then does the death animation
            SetAnimationInteger("Condition", deadInt);
        }

        // Pause button
        if (Input.GetButtonDown("Joy1ButtonStart"))
        {
            pauseManager.GetComponent<PauseScreenManagerScript>().PauseGame();
        }

    }
    /*
     * Movement is taken in as a 2D Vector from the controller, and then is used to translate the Player
     */
    void Movement()
    {

        if(isTumbling)
        {
            return;
        }

        //The new concesion to the input system, instead of getting a vector we need to build it ourselves from both the horizontal and vertical axis. 
        //outside tree movement
        Vector3 movement = new Vector3(Input.GetAxis("Joy"+ playerID +"LeftStickVertical"), 0.0f, Input.GetAxis("Joy"+ playerID +"LeftStickHorizontal"));
        movement = movement * Time.deltaTime * movespeed;

        transform.rotation = Quaternion.Euler(new Vector3(0, mainCameraRotation.y - 90, 0));

        //If the player is only moving with one stick, and not both, we want the character to look in the direction that the player is walking, if they are using both sticks, then this gets overwritten.
        Vector3 LookDirection = new Vector3(Input.GetAxis("Joy" + playerID + "RightStickVertical"), 0.0f, Input.GetAxis("Joy" + playerID + "RightStickHorizontal"));

        //we only want the player to look in a direction if moving, and not if its in the deadzone. vector3.zero is the deadzone. 
        if (LookDirection == Vector3.zero && movement != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(movement, Vector3.up);


            // This means that the movement now reflects the direction the camera is looking
            lookRotation *= Quaternion.Euler(0, mainCameraRotation.y - 90, 0);

       

            float step = rotSpeed * Time.deltaTime;
          thisPlayerChild.transform.rotation = Quaternion.RotateTowards(lookRotation, thisPlayerChild.transform.rotation, step);
           // Debug.Log(LookDirection+" Left Stick Look Direction");
        }
        
        // Actually do the movement
        transform.Translate(movement);
        //   Debug.Log(movement+" Left Stick Movement");

        

    }

    /*
     * Direction is taken at the same time as the movement, and using the same 2D input, is rotated around to get the player facing the correct direction when moving
     * If the vector is zero we also dont want to move the player, zero meaning the stick has been let go, and there is no input movement. If we didnt do this,
     * Any time the player let go of the stick the model would "reset" to facing the same original direction
     */
    void LookAt()
    {
       if(isTumbling)
        {
            return;
        }
        //like in movement, instead of being given the vector we need to build it ourselves. 
       Vector3 LookDirection = new Vector3(Input.GetAxis("Joy" + playerID + "RightStickVertical"), 0.0f, Input.GetAxis("Joy" + playerID + "RightStickHorizontal"));
        if(LookDirection == Vector3.zero)
        {
            return;
        }
                Quaternion lookRotation = Quaternion.LookRotation(LookDirection, Vector3.up);

        //lookRotation *= Quaternion.Euler(0, 45, 0); //As the camera is roated 45 degrees, if we didnt also do this, it makes movement wierd. when you moved left it would go left and slightly up from the camera, which while true left, instead left in relation to the camera. This fixes that. 

        // This means that the movement now reflects the direction the camera is looking
        lookRotation *= Quaternion.Euler(0, mainCameraRotation.y - 90, 0);

        float step = rotSpeed * Time.deltaTime;
                thisPlayerChild.transform.rotation = Quaternion.RotateTowards(lookRotation, thisPlayerChild.transform.rotation, step);
          // }
      //  }
    }



    /*
     * This handles the attacking of the game. It is split into two parts, a melee and a ranged attack, using a bool value to control which controls
     * the attacks which is being done.
     * */
    void OnAttackRightTrigger()
    {
        //  Debug.Log("Attacking!");
        if (myclass != null) 
        {     
            myclass.genericAttack();
            SetAnimationInteger("Condition", genericAttackInt);
            
            // Audio
            if (audioSource != null && audioClipAttack != null)
            {
                audioSource.clip = audioClipAttack;
                audioSource.Play();
            }

            return; 
        }

        if (isRangedAttack)
        {
            // Sets us to have the 'GunShoot' audio clip
            if (audioSource != null) { audioSource.clip = audioClipGunShoot; }
            // Then plays that clip
            if (audioSource != null)
            {
                audioSource.Play();
            }

            GameObject Bullet = GameObject.Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
            Bullet.GetComponent<Rigidbody>().AddForce(projectileSpawn.forward * forceStrength);
            Destroy(Bullet, 4.0f);
        }
        else
        {
            // Sets us to have the 'SwordSwing' audio clip
            if (audioSource != null) { audioSource.clip = audioClipSwordSwing; }

            if (audioSource != null)
            { // Then plays that clip
                audioSource.Play();
            }
            GameObject Melee = GameObject.Instantiate(projectile, meleeSpawn.position, meleeSpawn.rotation);
            Melee.transform.localScale = new Vector3(1.5f, 4.0f, 1.5f);
            Destroy(Melee, 0.5f);
        }
    }

    private void OnAbilityLeftTrigger()
    {

        if (myclass != null)
        {
            myclass.abilityAttack();
            SetAnimationInteger("Condition", abilityAttackInt);

            // Audio
            if (audioSource != null && audioClipAbility != null)
            {
                audioSource.clip = audioClipAbility;
                audioSource.Play();
            }

            return;
        }
        Debug.Log("Left Trigger pressed by player"+ playerID +", using ability");

        if (abilityUI != null)
        {
            // Sets the ability UI to be false, comes back when cooldown finished
            abilityUI.SetActive(false);
            //abilityUIOn = false;
        }
    }

    /*
     * simple control to change if the attack is melee or ranged, we control that using a bool value. 
     */
    void OnSwitchWeapon()
    {
        if (myclass != null) 
        {
            myclass.ultraAttack();
            SetAnimationInteger("Condition", ultraAttackInt);

            // Audio
            if (audioSource != null && audioClipUltra != null)
            {
                audioSource.clip = audioClipUltra;
                audioSource.Play();
            }

            return;
        }
        isRangedAttack = !isRangedAttack;
        //  Debug.Log("Switching Weapon!" + isRangedAttack);

        if (ultraUI != null)
        {
            // Sets the ultra UI to be false, comes back when cooldown finished
            ultraUI.SetActive(false);
            //ultraUIOn = false;
        }
    }


    /*
     * Our jump. currently it is unimpeded, you can jump infinte number of times, will need to be edited in the future
     * TODO: add Ground to each object, lock the jumping to when you touch the ground only. 
     */ 
    void OnJump()
    {
        if (canJump)
        {
            SetAnimationInteger("Condition", jumpStartInt);
            canJump = false;
            Debug.Log("Jumping!");
            this.GetComponent<Rigidbody>().AddForce(0.0f, jumpForce, 0.0f);
        }
    }


    // Called by the entity script when our health reaches 0
    public void Die()
    {
        isDead = true;

        // Used so that people can revive us
        GetComponent<BoxCollider>().enabled = true;

        // Shows the revive button sprite
        reviveSprite.SetActive(true);

        // Tells our playerInputManager to check if there's a game over - if all players are dead
        playerInputManager.GetComponent<PlayerSelectionScript>().CheckGameOver();
    }
    // Called by the entity script when we get revived
    public void Revive()
    {
        isDead = false;

        // Once we're revived, this can be disabled again
        GetComponent<BoxCollider>().enabled = false;

        // Hides the revive button sprite
        reviveSprite.SetActive(false);

        // Sets us back to idle
        SetAnimationInteger("Condition", idleInt);
    }

    private void SetupControls()
    {
        
    }

    public void SetCamera(Transform camera)
    {
        //mainCamera = camera;
        mainCameraRotation = camera.rotation.eulerAngles;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == GROUND_TAG)
        {
            canJump = true;
            SetAnimationInteger("Condition", jumpLandInt);
        }
        if(isTumbling == true)
        {
            isTumbling = false;
            this.GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine(WaitUntilTuronKin(0.001f));
        }
    }

    IEnumerator WaitUntilTuronKin(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        this.GetComponent<Rigidbody>().isKinematic = false;
    }


    public void DodgeAnim()
    {
        SetAnimationInteger("Condition", dodgeStartInt);
    }
}
