using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/******************
 * Anton Ziegler s1907905
 * ****************/

public class WarriorPushBack : MonoBehaviour
{
    //this was meant to be a crowd control ability for the warrior, but was changed to his blade wave. 
    public string ENEMY_TAG = "Enemy";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ENEMY_TAG)
        {
            Vector3 direction = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            direction = -direction.normalized;
        }
    }
}
