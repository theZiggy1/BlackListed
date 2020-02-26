using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideTreeCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject LookAt;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LookAt !=null)
        {
            this.gameObject.transform.LookAt(LookAt.transform);
        }
    }
}
