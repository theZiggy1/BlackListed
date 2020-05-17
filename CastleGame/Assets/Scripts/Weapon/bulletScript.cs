using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float forceStrength;
    public string OTHER_TAG = "Enemy";
    public string BELL_TAG = "Bell";
    public int enemyDamage = 100;
    public bool isRangerUltra = false;
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
        if (other.gameObject.tag == OTHER_TAG)
        {
            other.gameObject.GetComponent<EntityScript>().TakeDamage(enemyDamage);
            if (!isRangerUltra) { Destroy(this.gameObject); }
        }

        if(other.gameObject.tag == BELL_TAG)
        {
            other.gameObject.GetComponent<Bell>().RingingBell();
            Destroy(this.gameObject);
        }
    }
}
