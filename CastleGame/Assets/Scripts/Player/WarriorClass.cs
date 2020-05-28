using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************
 * Anton Ziegler s1907905
 * ****************/
public class WarriorClass : BaseClass
{

    //This is the classes for the warrior. in PLayer controller we call into these functions
    public float forceStrength;
    //This spawns the moving wave
    public override void abilityAttack()
    {
        if (abilityCoolDown <= 0)
        {
            abilityCoolDown = abilityCoolDownReset;
            GameObject Ranged = GameObject.Instantiate(abilityAttackObj, abilityLocation.position, abilityLocation.rotation);
            Ranged.GetComponent<Rigidbody>().AddForce(abilityLocation.forward * forceStrength);
            //   Melee.transform.localScale = new Vector3(1.5f, 4.0f, 1.5f);
            Destroy(Ranged, 5.0f);
        }
    }
    //this is the regular attack, a close sword attack, with the corrisponding trigger 
    public override void genericAttack()
    {
        if (genericAattackCoolDown <= 0)
        {
            genericAattackCoolDown = genericAttackReset;
            GameObject Melee = GameObject.Instantiate(basicAttackObj, basicAttackLocation.position, basicAttackLocation.rotation);
            //Melee.transform.localScale = new Vector3(1.5f, 4.0f, 1.5f);
            Destroy(Melee, 0.5f);
        }
    }

    //This is a wall that is spawned, thst the enemies need to navigate around
    public override void ultraAttack()
    {
      if(ultraCoolDown<=0)
        {
            ultraCoolDown = ultraCoolDownReset;
            GameObject Ultra = GameObject.Instantiate(ultraAttackObj, ultraLocation.position, ultraLocation.rotation);
            Destroy(Ultra, 5.0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        abilityCoolDown -= Time.deltaTime;
        ultraCoolDown -= Time.deltaTime;
        genericAattackCoolDown -= Time.deltaTime;
    }
}
