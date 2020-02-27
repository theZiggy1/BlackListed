using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideTreeCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject LookAt;
    public float cameraTransformHeight;
    public float xOffset;
    public float zOffset;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LookAt !=null)
        {
          //  this.gameObject.transform.LookAt(LookAt.transform);
            this.gameObject.transform.position = new Vector3(LookAt.transform.position.x + xOffset, LookAt.transform.position.y + cameraTransformHeight, LookAt.transform.position.z + zOffset);
        }
    }
}
