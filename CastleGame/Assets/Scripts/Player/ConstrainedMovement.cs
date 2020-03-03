using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainedMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
        Vector3 inputVector = new Vector3( Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //call transform vector to convert the local space into world space to calculate movement
        inputVector = transform.TransformVector(inputVector);
        transform.position += (inputVector * Time.deltaTime * 2) + new Vector3( 0, 0.002f, 0);
        //of course the above could have been done with the following line without the need to transform the vector
        //transform.position += transform.forward + (inputVector * Time.deltaTime * 2);

    }
}
