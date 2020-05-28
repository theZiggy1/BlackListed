using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************
 * Anton Ziegler s1907905
 * ****************/
public class LightingStrikeScript : MonoBehaviour
{
    //when the boss calls this, we also want to make a hitbox that hits the ground, which this makes. 
    // Start is called before the first frame update
    [SerializeField] GameObject EnemyBullet;
    [SerializeField] float downForce;
    void Start()
    {
        GameObject Bullet = GameObject.Instantiate(EnemyBullet, this.transform.position, this.transform.rotation);
        Bullet.GetComponent<Rigidbody>().AddForce( new Vector3(0, downForce, 0));
        Destroy(Bullet, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
