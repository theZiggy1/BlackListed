using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerClass : BaseClass
{ 
    public override void abilityAttack()
    {
        if (abilityCoolDown <= 0)
        {
            abilityCoolDown = genericAttackReset;
            GameObject Melee = GameObject.Instantiate(abilityAttackObj, abilityLocation.position, abilityLocation.rotation);
            //   Melee.transform.localScale = new Vector3(1.5f, 4.0f, 1.5f);
            Destroy(Melee, 5.0f);
        }
    }
    public override void genericAttack()
    {
        if (genericAattackCoolDown <= 0)
        {
            genericAattackCoolDown = genericAttackReset;
            GameObject Melee = GameObject.Instantiate(basicAttackObj, basicAttackLocation.position, basicAttackLocation.rotation);
            Melee.transform.localScale = new Vector3(1.5f, 4.0f, 1.5f);
            Destroy(Melee, 0.5f);
        }
    }

    public override void ultraAttack()
    {
        if (ultraCoolDown <= 0)
        {
            ultraCoolDown = ultraCoolDownReset;
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
