using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideTreeCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject LookAt;
    public float cameraTransformHeight;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LookAt !=null)
        {
            this.gameObject.transform.LookAt(LookAt.transform);
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, LookAt.transform.position.y + cameraTransformHeight, this.gameObject.transform.position.z);
        }
    }
}
