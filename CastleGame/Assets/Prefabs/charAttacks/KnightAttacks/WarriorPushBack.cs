using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorPushBack : MonoBehaviour
{
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
