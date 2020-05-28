using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************
 * Anton Ziegler s1907905
 * ****************/
public abstract class BaseClass : MonoBehaviour
{

    public float genericAttackReset;
    public float abilityCoolDownReset;
    public float ultraCoolDownReset;
    // Start is called before the first frame update
    public  Transform basicAttackLocation;
    public Transform abilityLocation;
    public Transform ultraLocation;

    public GameObject basicAttackObj;
    public GameObject abilityAttackObj;
    public GameObject ultraAttackObj;

    public float abilityCoolDown;
    public float ultraCoolDown;
    public float genericAattackCoolDown;

   
   

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
