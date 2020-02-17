using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float forceStrength;
    public string ENEMY_TAG = "Enemy";
    void Start()
    {
        //this.GetComponent<Rigidbody>().AddRelativeForce(this.transform.forward * forceStrength);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.tag == ENEMY_TAG)
        {
            Destroy(collision.collider.gameObject);
            Destroy(this.gameObject);
        }
    }
}
