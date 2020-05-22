using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObjectScript : MonoBehaviour
{
    [SerializeField]
    private Vector3 originalPosition; // The original position of the door

    [SerializeField]
    private Transform transformToMoveTo; // Will be an object in the world that we lerp towards

    [SerializeField]
    private float moveSpeed = 2.0f; // How quickly we raise and lower

    [SerializeField]
    private bool lerping; // If we're lerping

    [SerializeField]
    private bool moveUp; // Tells us to move up
    [SerializeField]
    private bool moveDown; // Tells us to move down

    [SerializeField]
    [Tooltip("Set to true if we want the door to lower at the start")]
    private bool startsLowered;

    [Space(5)]

    [SerializeField]
    private bool useAnimation;
    [SerializeField]
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (useAnimation)
        {
            if (GetComponent<Animator>() != null)
            {
                animator = GetComponent<Animator>();
            }
            else
            {
                Debug.Log("There's no animator attached to this door but it's set to use animation!");
            }
        }

        // Finds our position at the start
        originalPosition = transform.position;

        // Lowers the door at the start if it's set to
        if (startsLowered)
        {
            LowerDoor();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moveUp)
        {
            if (useAnimation)
            {
                animator.Play("Close");
            }
            else // If we're not doing an animation, instead move us up
            {
                //transform.position = Vector3.Lerp(transformToMoveTo.position, originalPosition, Time.deltaTime * moveSpeed);
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, Time.deltaTime * moveSpeed);
            }

            Debug.Log("Moving up!");

            // Seems to be some kind of issue with Vector3.Distance
            //// If the distance becomes this small, we're basically there already
            //if (Vector3.Distance(transform.position, transformToMoveTo.position) < 0.1f)
            //{
            //    // 'Clamps' our position to the correct one
            //    transform.position = transformToMoveTo.position;
            //    moveUp = false;
            //}
        }
        if (moveDown)
        {
            if (useAnimation)
            {
                animator.Play("Open");
            }
            else // If we're not doing an animation, instead move us down
            {
                //transform.position = Vector3.Lerp(originalPosition, transformToMoveTo.position, Time.deltaTime * moveSpeed);
                transform.position = Vector3.MoveTowards(transform.position, transformToMoveTo.position, Time.deltaTime * moveSpeed);
            }

            Debug.Log("Moving down!");

            // Seems to be some kind of issue with Vector3.Distance
            //// If the distance becomes this small, we're basically there already
            //if (Vector3.Distance(transform.position, originalPosition) < 0.1f)
            //{
            //    // 'Clamps' our position to the correct one
            //    transform.position = originalPosition;
            //    moveDown = false;
            //}
        }
    }

    // Doors get raised when we enter a room
    public void RaiseDoor()
    {
        moveUp = true;
        moveDown = false;

        //// Lerps between transformToMoveTo and original position
        //Vector3.Lerp(transformToMoveTo.position, originalPosition, Time.deltaTime * moveSpeed);
    }

    // Doors get lowered when we've finished the wave
    public void LowerDoor()
    {
        moveDown = true;
        moveUp = false;

        // Lerps between original position and transformToMoveTo
        //Vector3.Lerp(originalPosition, transformToMoveTo.position, Time.deltaTime * moveSpeed);
    }
}
