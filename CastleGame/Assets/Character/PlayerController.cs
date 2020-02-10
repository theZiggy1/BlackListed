using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/*
 * Script Written by Anton Ziegler s1907905
 */
public class PlayerController : MonoBehaviour
{
    Vector2 movementVec;
    Vector2 rotVec;
    [SerializeField] float movespeed = 5f;
    float rotSpeed = 1f;
   [SerializeField] GameObject thisPlayerChild;
    [SerializeField] float jumpForce;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform projectileSpawn;
    [SerializeField] float forceStrength;

    // Start is called before the first frame update
    void Start()
    {

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
         Vector3 movement = new Vector3(movementVec.y, 0.0f, -movementVec.x) * movespeed * Time.deltaTime;
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

    void OnAttackRightTrigger()
    {
        Debug.Log("Attacking!");
     GameObject Bullet =   GameObject.Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
        Bullet.GetComponent<Rigidbody>().AddForce(projectileSpawn.forward * forceStrength);
    }

    void OnSwitchWeapon()
    {
        Debug.Log("Switching Weapon!");
    }

    void OnJump()
    {

        Debug.Log("Jumping!");
        this.GetComponent<Rigidbody>().AddForce(0.0f, jumpForce, 0.0f);
    }
}
