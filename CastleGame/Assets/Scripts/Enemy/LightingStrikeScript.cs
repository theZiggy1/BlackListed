using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingStrikeScript : MonoBehaviour
{
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
