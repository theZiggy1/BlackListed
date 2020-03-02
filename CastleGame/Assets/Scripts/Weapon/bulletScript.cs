using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float forceStrength;
    public string ENEMY_TAG = "Enemy";
    public int enemyDamage = 100;
    void Start()
    {
        //this.GetComponent<Rigidbody>().AddRelativeForce(this.transform.forward * forceStrength);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ENEMY_TAG)
        {
            other.gameObject.GetComponent<EntityScript>().TakeDamage(enemyDamage);
            Destroy(this.gameObject);
        }
    }
}
