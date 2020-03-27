using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDamageScript : MonoBehaviour
{
    // This script is meant to be attached to anything that can damage an entity
    // e.g. A weapon, a bullet, a stalactite

    [SerializeField]
    [Tooltip("The amount of damage this object will do when it hits an entity, !Remember! This object should have a trigger on it!")]
    private float damage;

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
        // If we hit anything tagged with player, or enemy
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            // If it has an entity script on it
            if (other.GetComponent<EntityScript>() != null)
            {
                other.GetComponent<EntityScript>().TakeDamage(damage);
            }
            else
            {
                // Outputs this log, as players and enemies should have entity scripts
                Debug.Log("We've hit a player/enemy but they don't have an entity script!");
            }
        }
    }
}
