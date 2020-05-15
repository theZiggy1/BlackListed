using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerChildScript : MonoBehaviour
{
    [SerializeField]
    private Vector3 originalPosition;
    [SerializeField]
    private Vector3 originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.eulerAngles;

        // For some reason the z rotation isn't 0 (it's 1.2etc)
        originalRotation.z = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPosition()
    {
        //transform.position = originalPosition;
        transform.rotation = Quaternion.Euler(originalRotation);
    }

}
