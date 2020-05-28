using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float forceStrength;
    public string OTHER_TAG = "Enemy";
    public string BELL_TAG = "Bell";
    public string DUMMY_TAG = "dummyEnemy";
    public int enemyDamage = 100;
    public bool isRangerUltra = false;

    // This is done so that attacks that don't stay around for long
    // can still play some audio without cutting off, if the attack
    // is destroyed fairly quickly
    [SerializeField]
    private GameObject objectToSpawn;

    void Start()
    {
        //this.GetComponent<Rigidbody>().AddRelativeForce(this.transform.forward * forceStrength);
        if (objectToSpawn != null)
        {
            GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation);
            Destroy(spawnedObject, 6.0f); // Destroys the object after 6 seconds, enough for any audio clip to play
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);

        if (other.gameObject.tag == OTHER_TAG)
        {
            Debug.Log("Hit a player");
            other.gameObject.GetComponent<EntityScript>().TakeDamage(enemyDamage);
            if (!isRangerUltra)
            {
                Destroy(this.gameObject); 
            }
            
        }

        if(other.gameObject.tag == BELL_TAG)
        {
            other.gameObject.GetComponent<Bell>().RingingBell();
            Destroy(this.gameObject);
        }

        if(other.gameObject.tag == DUMMY_TAG)
        {
            other.gameObject.GetComponent<TargetDummyScript>().HitDummy();
            Destroy(this.gameObject);

        }
    }
}
