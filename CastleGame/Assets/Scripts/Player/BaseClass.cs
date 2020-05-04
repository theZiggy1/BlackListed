using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseClass : MonoBehaviour
{
    // Start is called before the first frame update
    public  Transform basicAttackLocation;
    public Transform abilityLocation;
    public Transform ultraLocation;

    public GameObject basicAttackObj;
    public GameObject abilityAttackObj;
    public GameObject ultraAttackObj;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void genericAttack();

    public abstract void abilityAttack();

    public abstract void ultraAttack();
    

    

    

    


}
