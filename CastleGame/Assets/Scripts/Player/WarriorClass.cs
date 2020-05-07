using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorClass : BaseClass
{

    public float forceStrength;
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
