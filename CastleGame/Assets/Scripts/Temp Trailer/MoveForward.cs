using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************
 * Anton Ziegler s1907905
 * ****************/
public class MoveForward : MonoBehaviour
{
    //This script was used in the trailer to move the chacters forward


    //private Vector3 ThisObjectsLocal;
    public float speed = 0.05f;
    public Transform endlocation;

    //public float tiltAngleX = 0.0f;
    // float tiltAngleY = 60.0f;
    // float tiltAngleZ = 0.0f;
    //private Quaternion Myquat; 

    // Start is called before the first frame update
    void Start()
    {
        //Myquat = Quaternion.Euler(tiltAngleX, tiltAngleY, tiltAngleZ);
        //ThisObjectsLocal = transform.TransformPoint(Vector3.right * speed);
        //Instantiate(wHICHOBJECTDOYOUWANT, ThisObjectsLocal, wHICHOBJECTDOYOUWANT.transform.rotation);
        //this.transform.Rotate(tiltAngleX, tiltAngleY, tiltAngleZ, Space.Self);
        //this.gameObject.transform.Rotate(new Vector3(tiltAngleX, tiltAngleY, tiltAngleZ));
    }

    // Update is called once per frame
    void Update()
    {

        this.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, endlocation.position, speed* Time.deltaTime);
        this.transform.LookAt(endlocation);

        //Vector3 temp = new Vector3(speed, 0, 0);
        //this.transform.localPosition += temp;
        //.transform.rotation = Quaternion.RotateTowards(Myquat, this.transform.rotation, speed);
    }
}
