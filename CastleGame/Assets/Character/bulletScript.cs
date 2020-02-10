using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float forceStrength;
    void Start()
    {
        //this.GetComponent<Rigidbody>().AddRelativeForce(this.transform.forward * forceStrength);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
