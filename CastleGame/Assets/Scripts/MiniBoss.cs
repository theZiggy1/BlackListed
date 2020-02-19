using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///MiniBossScript
/// Catherine Burns
/// 19/02/2020
/// </summary>
public class MiniBoss : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private Rigidbody rigidBody;

    private int counter;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBullets();
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {

        counter++;

        if(counter <= 5)
        {
            SpawnBullets();
            rigidBody.AddForce(-0.5f, 0, 0, ForceMode.VelocityChange);
        }
        //Destroy();
    }

    private void SpawnBullets()
    {
        // creates a bullet from the prefab
        Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        bullet.transform.parent = gameObject.transform;
        // finds the rigidbodies of the bullets
        rigidBody = GetComponentInChildren<Rigidbody>();
    }
}
