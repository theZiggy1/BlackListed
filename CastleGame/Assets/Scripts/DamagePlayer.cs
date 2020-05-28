using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************
 * Anton Ziegler s1907905
 * ****************/
public class DamagePlayer : MonoBehaviour
{
    public int damagePlayer = 20;
    public string PLAYER_TAG = "Player";

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
        if(other.gameObject.tag == PLAYER_TAG)
        {
            other.gameObject.GetComponent<EntityScript>().TakeDamage(damagePlayer);
            Debug.Log("Flesh Wound");
        }
    }
}
