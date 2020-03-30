using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;


/*
 * Script Written by Anton Ziegler s1907905
 */
public class PlayerControllerOldInput : MonoBehaviour
{
    // Input configuration
    [SerializeField]
    public int playerID = -1;
    private string buttonA; // Button 0
    private string buttonB; // Button 1
    private string buttonX; // Button 2
    private string buttonY; // Button 3
    private string buttonLB; // Button 4
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
    private GameObject gameManager;

    public int playerNum;
    bool Attacking = false; // the right trigger is in fact an axis, and to keep the player from attacking each frame, once the trigger is depressed, this is called, and not reverted until the trigger is released completely. 

    // Tree stuff
    [SerializeField] GameObject insideTreeCamera;
    public bool insideTree = false;


    // Animation stuff
    [SerializeField]
    private Animator playerAnimator;


    //private Animator GetAnimator(string anim_name)
    //{
    //    foreach (Animator anim in Animators)
    //    {

    //        //If we found the desired animator
    //        if (anim.transform.gameObject.name == anim_name)
    //        {
    //            //Let's return the animator
    //            return anim;
    //        }

    //    }

    //    return null;
    //}

    ////setting animation, instead of setting a condition each time. It goes through each on all animation controller possible settings.
    //private void SetAnimationInteger(string condition, int integer)
    //{
    //    foreach (Animator anim in Animators)
    //    {
    //        anim.SetInteger(condition, integer);
    //    }
    //}

    //private void SetAnimationFloat(string condition, float floating_num)
    //{
    //    foreach (Animator anim in Animators)
    //    {
    //        anim.SetFloat(condition, floating_num);
    //    }
    //}

    //private void SetAnimationBool(string condition, bool boolean)
    //{
    //    foreach (Animator anim in Animators)
    //    {
    //        anim.SetBool(condition, boolean);
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        // Animation setup
        


        //Controller = GetComponent<CharacterController>();

        //////////////Let's grab all the animators in the children uwu
        ////////////Animator[] animators = gameObject.transform.Find("Brian").gameObject.transform.Find("Clothing").GetComponentsInChildren<Animator>();
        ////////////string[] animator_names = {"Trousers", "Boots", "Tunic"};
        //////////////Let's iterate through them all cause we just need brians
        ////////////foreach (Animator anim in animators)
        ////////////{

        ////////////    //If the gameobject of this anim is Brian, then we found the right animator!
        ////////////    if (animator_names.Contains(anim.transform.gameObject.name))
        ////////////    {
        ////////////        //Let's yoink this :D
        ////////////        Animators.Add (anim);
        ////////////        //Let's early out cause fuck the other animators
        ////////////    }
        ////////////}

        ////////////Animators.Add(gameObject.transform.Find("Brian").GetComponent<Animator>());


        ////////////SetAnimationInteger("Condition", 0);

     //  transform.position = new Vector3(0, 0, 0);

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
    }

    // Update is called once per frame
    void Update()
    {
        //our update loop due to the controller rewrite now handles what was originally sendmessage from the input system. Now we need to manually handle sending in these inputs
        //The first is to mandle movement. We also specifically do it in this order in case the right stick is being engaged to look a different direction, otherwise in movement, the player will naturally look towards the direction the player is walking towards. 
        //Movement();
        //LookAt();
        // With these two functions above always getting called every frame, instead of input being checked in Update *then* the functions being called,
        // it means that these will always override any other animation that the player is doing
        // So therefore input checking needs to be switched to be here, *then* call these functions after

        //Debug.Log("Left stick horizontal = " + Input.GetAxis("Joy" + playerID + "LeftStickHorizontal"));
        //Debug.Log("Left stick vertical = " + Input.GetAxis("Joy" + playerID + "LeftStickVertical"));

        // With these functions we'll combine it with what direction the character is looking at
        // to determine which direction of walk animation to use

        // Character is moving right
        if (Input.GetAxis("Joy" + playerID + "LeftStickHorizontal") < -0.1f)
        {
            Debug.Log("Moving right");

            Movement();
            LookAt();

            // Do the animation for movement
            playerAnimator.Play("Sword Run Forward");
        }
        // Character is moving left
        else if (Input.GetAxis("Joy" + playerID + "LeftStickHorizontal") > 0.1f)
        {
            Debug.Log("Moving left");

            Movement();
            LookAt();

            // Do the animation for movement
            playerAnimator.Play("Sword Run Forward");
        }
        // Character is moving up
        else if (Input.GetAxis("Joy" + playerID + "LeftStickVertical") > 0.1f)
        {
            Debug.Log("Moving up");

            Movement();
            LookAt();

            // Do the animation for movement
            playerAnimator.Play("Sword Run Forward");
        }
        // Character is moving down
        else if (Input.GetAxis("Joy" + playerID + "LeftStickVertical") < -0.1f)
        {
            Debug.Log("Moving down");

            Movement();
            LookAt();

            // Do the animation for movement
            playerAnimator.Play("Sword Run Forward");
        }
        else
        {
            if (!Attacking)
            {
                Debug.Log("Not moving");

                playerAnimator.Play("Sword Idle");
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

                // Plays the animation
                // Naming for this is absolute right now, will probably change later
                playerAnimator.Play("Sword Swing 1");

                // Sets us so we're attacking
                Attacking = true;
            }
        }
         //This links with the above section. attacking needs to be reset when the player is odne pressing the trigger, but as it is a a float value, it needs to check to see if its the whole way depressed/ 
        if(Attacking == true)
        {
            if(Input.GetAxis("Joy" + playerID + "RightTrigger") == 0.0f)
            {
                Attacking = false;
            }
        }

        //buttons only return true the frame they are called with button down, so the above isnt true here. This lets us handle switching weapons.
        if (Input.GetButtonDown("Joy" + playerID + "ButtonY"))
        {
            OnSwitchWeapon();
        }

        //This lets us handle jumping
        if (Input.GetButtonDown("Joy" + playerID + "ButtonA"))
        {
            OnJump();
        }


    }
    /*
     * Movement is taken in as a 2D Vector from the controller, and then is used to translate the Player
     */
    void Movement()
    {
        //Vector3 movement = new Vector3(movementVec.y, 0.0f, -movementVec.x) * movespeed * Time.deltaTime;
        //transform.Translate(movement);

      //  Debug.Log("Joy1LeftStickVertical: " + Input.GetAxis("Joy1LeftStickVertical"));
       // Debug.Log("Joy1LeftStickHorizontal: " + Input.GetAxis("Joy1LeftStickHorizontal"));

        //The new concesion to the input system, instead of getting a vector we need to build it ourselves from both the horizontal and vertical axis. 
        //outside tree movement
        Vector3 movement = new Vector3(Input.GetAxis("Joy"+ playerID +"LeftStickVertical"), 0.0f, Input.GetAxis("Joy"+ playerID +"LeftStickHorizontal"));
        movement = movement * Time.deltaTime * movespeed;


        if (insideTree && movement.magnitude > 0.0)
        {
            Transform camTx = Camera.main.transform; //get camera's transform
                                                     //we want it so that the camera is always to the players right
            Vector3 newFwd = camTx.right;
            Vector3 camPos = camTx.position;
            camPos.y = transform.position.y;
            Vector3 newRight = camPos - transform.position;
            newRight.Normalize();
            Vector3 newUp = Vector3.Cross(newFwd, newRight);

            Quaternion newRotation = Quaternion.LookRotation(newFwd, newUp);

            transform.rotation = newRotation;
            //now that the object is facing the right direction move it appropriately.
            //get the input vector -- this is in local space to the character not in world space
            Vector3 inputVector = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
            //call transform vector to convert the local space into world space to calculate movement
            //inputVector = transform.TransformVector(inputVector);
           // transform.position += (inputVector * Time.deltaTime * 2);
            //of course the above could have been done with the following line without the need to transform the vector
            transform.position += Vector3.Scale(transform.forward,  (inputVector * Time.deltaTime * this.movespeed));


        }

        //If the player is only moving with one stick, and not both, we want the character to look in the direction that the player is walking, if they are using both sticks, then this gets overwritten.
        Vector3 LookDirection = new Vector3(Input.GetAxis("Joy" + playerID + "RightStickVertical"), 0.0f, Input.GetAxis("Joy" + playerID + "RightStickHorizontal"));

        //we only want the player to look in a direction if moving, and not if its in the deadzone. vector3.zero is the deadzone. 
        if (LookDirection == Vector3.zero && movement != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(movement, Vector3.up);


                        lookRotation *= Quaternion.Euler(0, 45, 0);

            //////Use this for setting animation. 
            //////SetAnimationInteger("Condition", 1);

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
       
        //like in movement, instead of being given the vector we need to build it ourselves. 
       Vector3 LookDirection = new Vector3(Input.GetAxis("Joy" + playerID + "RightStickVertical"), 0.0f, Input.GetAxis("Joy" + playerID + "RightStickHorizontal"));
      //  Debug.Log(LookDirection+" Right Stick Look around");
       // if (LookDirection.x > 0.11 || LookDirection.x < -0.11)
        //{
          //  if (LookDirection.z > 0.11 || LookDirection.z < -0.11)
           // {
            //the deadzone of a stick is 0.0, and we dont want the character to look at that direction. if you let go of the stick, it would always look at 0,0, instead of the last direction it was looking in.
        if(LookDirection == Vector3.zero)
        {
            return;
        }
                Quaternion lookRotation = Quaternion.LookRotation(LookDirection, Vector3.up);
       
        lookRotation *= Quaternion.Euler(0, 45, 0); //As the camera is roated 45 degrees, if we didnt also do this, it makes movement wierd. when you moved left it would go left and slightly up from the camera, which while true left, instead left in relation to the camera. This fixes that. 
        float step = rotSpeed * Time.deltaTime;
                thisPlayerChild.transform.rotation = Quaternion.RotateTowards(lookRotation, thisPlayerChild.transform.rotation, step);
          // }
      //  }
    }

    /*
     * This handles the input from the left stick on all controllers. This has been depreciated. 
     */
    void OnLeftStick(InputValue value)
    {
        movementVec = value.Get<Vector2>();
      // Debug.Log(value.Get<Vector2>());
    }

    //This is also depreciated. 
    void OnRightStick(InputValue value)
    {
        rotVec = value.Get<Vector2>();
        Debug.Log(value.Get<Vector2>());
    }

    /*
     * This handles the attacking of the game. It is split into two parts, a melee and a ranged attack, using a bool value to control which controls
     * the attacks which is being done.
     * */
    void OnAttackRightTrigger()
    {
      //  Debug.Log("Attacking!");

        if (isRangedAttack)
        {
            GameObject Bullet = GameObject.Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
            Bullet.GetComponent<Rigidbody>().AddForce(projectileSpawn.forward * forceStrength);
            Destroy(Bullet, 4.0f);
        }
        else
        {
            GameObject Melee = GameObject.Instantiate(projectile, meleeSpawn.position, meleeSpawn.rotation);
            Melee.transform.localScale = new Vector3(1.5f, 4.0f, 1.5f);
            Destroy(Melee, 0.5f);
        }
    }

    /*
     * simple control to change if the attack is melee or ranged, we control that using a bool value. 
     */
    void OnSwitchWeapon()
    {
        isRangedAttack = !isRangedAttack;
      //  Debug.Log("Switching Weapon!" + isRangedAttack);
    }


    /*
     * Our jump. currently it is unimpeded, you can jump infinte number of times, will need to be edited in the future
     * TODO: add Ground to each object, lock the jumping to when you touch the ground only. 
     */ 
    void OnJump()
    {
        Debug.Log("Jumping!");
        this.GetComponent<Rigidbody>().AddForce(0.0f, jumpForce, 0.0f);
    }


    private void SetupControls()
    {
        
    }
}
