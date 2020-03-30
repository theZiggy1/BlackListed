using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentsToEnableandDisable : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject[] WallComponents;
    [SerializeField] GameObject[] RoomComponents;
    
  [SerializeField] bool wallDisabled = false;
  [SerializeField]  bool roomDisabled = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void WallComponentsTrigger()
    {
      //  Debug.Log("Wall");
        if (wallDisabled)
        {
            foreach (GameObject obj in WallComponents)
            {
                if (obj.GetComponent<MeshRenderer>() != null)
                {
                    obj.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
        else
        {
            foreach (GameObject obj in WallComponents)
            {
                if (obj.GetComponent<MeshRenderer>() != null)
                {
                    obj.GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }

        wallDisabled = !wallDisabled;
    }

    public void RoomComponentsTrigger()
    {

        Debug.Log("Room");
        if (roomDisabled)
        {
            foreach (GameObject obj in RoomComponents)
            {
                if (obj.GetComponent<MeshRenderer>() != null)
                {
                    obj.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
        else
        {
            foreach (GameObject obj in RoomComponents)
            {

                if (obj.GetComponent<MeshRenderer>() != null)
                { 
                obj.GetComponent<MeshRenderer>().enabled = false;
            }
            }
        }

        roomDisabled = !roomDisabled;
    }
}
