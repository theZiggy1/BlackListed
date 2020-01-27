using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{ 
    Vector2 movementVec;
    float movespeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 movement = new Vector3(movementVec.x, 0.0f, movementVec.y) * movespeed * Time.deltaTime;
        transform.Translate(movement);
    }

    void OnLeftStickMovement(InputValue value)

    {
        movementVec = value.Get<Vector2>();
    }

    void OnAttackRightTrigger()
    {

    }
}
