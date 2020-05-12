﻿using FMOD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunslingerClass : BaseClass
{
    public float intialBulletVelocity;
    public float intialDodgeVelocity;
    public float jumpForce;
    public float timeUntilAnimFinished;
    public string ENEMY_TAG = "Enemy";
    [SerializeField] PlayerControllerOldInput inputScript;
    public float enemyDamage = 100;
    [SerializeField] Rigidbody gunslingerBody;
    [SerializeField] GameObject Brian;
    public override void abilityAttack()
    {
        if (abilityCoolDown <= 0)
        {
            abilityCoolDown = genericAttackReset;
            Vector3 forceVector = -Brian.transform.forward * intialDodgeVelocity;
            forceVector += new Vector3(0.0f, jumpForce, 0.0f);
            gunslingerBody.AddForce(forceVector);
            genericAattackCoolDown = 0.0f;

            inputScript.isTumbling = true;
            StartCoroutine("WaitUntilTumble", timeUntilAnimFinished);
        }
    }

    public override void genericAttack()
    {
        if (genericAattackCoolDown <= 0)
        {
            genericAattackCoolDown = genericAttackReset;
            GameObject Bullet = GameObject.Instantiate(basicAttackObj, basicAttackLocation.position, basicAttackLocation.rotation);
            Bullet.GetComponent<Rigidbody>().AddForce(basicAttackLocation.forward * intialBulletVelocity);
            Destroy(Bullet, 0.5f);
        }
    }

    public override void ultraAttack()
    {
        if (ultraCoolDown <= 0)
        {
            ultraCoolDown = ultraCoolDownReset;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(ENEMY_TAG);

            foreach( GameObject EnemyObj in enemies)
            {
                EnemyObj.GetComponent<EntityScript>().TakeDamage(enemyDamage);
                //
            }
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

    IEnumerator WaitUntilTumble(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        inputScript.isTumbling = false;
    }
}