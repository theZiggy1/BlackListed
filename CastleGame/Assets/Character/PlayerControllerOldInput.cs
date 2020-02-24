using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/*
 * Script Written by Anton Ziegler s1907905
 */
public class PlayerControllerOldInput : MonoBehaviour
{
    // Input configuration
    [SerializeField]
    private int playerID = -1;
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
    

    Vector2 movementVec;
    Vector2 rotVec;
    [SerializeField] float movespeed = 5f;
    float rotSpeed = 0.1f;
    [SerializeField] GameObject thisPlayerChild;
    [SerializeField] float jumpForce;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform projectileSpawn;
    [SerializeField] Transform meleeSpawn;
    [SerializeField] float forceStrength;
    bool isRangedAttack = true;

    private string GAMEMANAGER_TAG = "GameManager";
    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        transform.Rotate(0, 45, 0);
        
        // Find the Game Manager
        gameManager = GameObject.FindGameObjectWithTag(GAMEMANAGER_TAG);
        // Set ourselves to be in its array
        gameManager.GetComponent<GameManagerScript>().currentPlayers[gameManager.GetComponent<GameManagerScript>().numPlayers] = gameObject;
        // Then increment that array
        gameManager.GetComponent<GameManagerScript>().numPlayers++;

        // This then sets our player ID to be how many players there are, e.g. if we are the first player, our playerID is now 1
        // if there's already a player there, our playerID is now 2
        playerID = gameManager.GetComponent<GameManagerScript>().numPlayers;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        LookAt();
    }
    /*
     * Movement is taken in as a 2D Vector from the controller, and then is used to translate the Player
     */
    void Movement()
    {
        //Vector3 movement = new Vector3(movementVec.y, 0.0f, -movementVec.x) * movespeed * Time.deltaTime;
        //transform.Translate(movement);

        Debug.Log("Joy1LeftStickVertical: " + Input.GetAxis("Joy1LeftStickVertical"));
        Debug.Log("Joy1LeftStickHorizontal: " + Input.GetAxis("Joy1LeftStickHorizontal"));

        Vector3 movement = new Vector3(Input.GetAxis("Joy"+ playerID +"LeftStickVertical") * 0.4f * movespeed, 0.0f, Input.GetAxis("Joy"+ playerID +"LeftStickHorizontal") * 0.4f * movespeed);
        transform.Translate(movement);
    }

    /*
     * Direction is taken at the same time as the movement, and using the same 2D input, is rotated around to get the player facing the correct direction when moving
     * If the vector is zero we also dont want to move the player, zero meaning the stick has been let go, and there is no input movement. If we didnt do this,
     * Any time the player let go of the stick the model would "reset" to facing the same original direction
     */
    void LookAt()
    {
       Vector3 LookDirection = new Vector3(rotVec.y, 0.0f, -rotVec.x);
        if (LookDirection.x > 0.11 || LookDirection.x < -0.11)
        {
            if (LookDirection.z > 0.11 || LookDirection.z < -0.11)
            {
            

                Quaternion lookRotation = Quaternion.LookRotation(LookDirection, Vector3.up);
                float step = rotSpeed * Time.deltaTime;
                thisPlayerChild.transform.rotation = Quaternion.RotateTowards(lookRotation, thisPlayerChild.transform.rotation, step);
           }
        }
    }

    /*
     * This handles the input from the left stick on all controllers. 
     */
    void OnLeftStick(InputValue value)
    {
        movementVec = value.Get<Vector2>();
      // Debug.Log(value.Get<Vector2>());
    }

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
        Debug.Log("Attacking!");

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
        Debug.Log("Switching Weapon!" + isRangedAttack);
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
