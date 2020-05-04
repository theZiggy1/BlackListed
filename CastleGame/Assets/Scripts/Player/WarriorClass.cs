using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorClass : BaseClass
{
    public override void abilityAttack()
    {
        GameObject Melee = GameObject.Instantiate(basicAttackObj, basicAttackLocation.position, basicAttackLocation.rotation);
        Melee.transform.localScale = new Vector3(1.5f, 4.0f, 1.5f);
        Destroy(Melee, 0.5f);
     
    }

    public override void genericAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void ultraAttack()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
