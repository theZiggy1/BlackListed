using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SomeScript : MonoBehaviour
{

    [SerializeField] GameObject obj;
   public  float theta = 0f;
    public float speed = 1.0f;
    public float speed2 = 10.0f;
    public Vector3 newLocation;
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       theta += Time.deltaTime * speed;
        if(theta > 360)
        {
            theta = 0;
        }

        newLocation.x = obj.transform.position.x + (radius * Mathf.Cos(theta * Mathf.PI / 180));
        newLocation.y = obj.transform.position.y + 1;
        newLocation.z = obj.transform.position.z + (radius * Mathf.Sin(theta * Mathf.PI / 180));

        this.transform.position = Vector3.MoveTowards(this.transform.position, newLocation, Time.deltaTime * speed2);
    }
}
