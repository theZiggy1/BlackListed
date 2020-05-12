﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardClass : BaseClass
{
    public float forceStrength;
    public override void abilityAttack()
    {
        if (abilityCoolDown <= 0)
        {
            abilityCoolDown = abilityCoolDown;
            GameObject Rift = GameObject.Instantiate(abilityAttackObj, abilityLocation.position, abilityLocation.rotation);
            //   Melee.transform.localScale = new Vector3(1.5f, 4.0f, 1.5f);
            Destroy(Rift, 5.0f);
        }
    }

    public override void genericAttack()
    {
        if (genericAattackCoolDown <= 0)
        {
            genericAattackCoolDown = genericAttackReset;
            GameObject Bullet = GameObject.Instantiate(basicAttackObj, basicAttackLocation.position, basicAttackLocation.rotation);
            Bullet.GetComponent<Rigidbody>().AddForce(basicAttackLocation.forward * forceStrength);
            Destroy(Bullet, 2.0f);
        }
    }

    public override void ultraAttack()
    {
        if (ultraCoolDown <= 0)
        {
            ultraCoolDown = ultraCoolDownReset;
            GameObject Bullet = GameObject.Instantiate(ultraAttackObj, ultraLocation.position, ultraLocation.rotation);
            Bullet.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
            Bullet.GetComponent<Rigidbody>().AddForce(basicAttackLocation.forward * forceStrength);
            Destroy(Bullet, 2.0f);
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