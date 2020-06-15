using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{

    AI _ai;


    public AttackState(AI ai) : base(ai.gameObject)
    {
        _ai = ai;
    }


    public override Type Tick()
    {

        float dist = Vector3.Distance(a: _ai.Target.transform.position, b: transform.position);
        if (dist > AIsettings.AttackRadius)
            return typeof(ChaseState);




        if (_ai.TEMP_attackSpeed)
        {

            if(_ai.TEMP_desitionValue > _ai.TEMP_blockValue)
            {
                _ai.TEMP_attackSpeed = true;
   
                return typeof(BlockState);
            }

            _ai.TEMP_desitionValue += 1;



            Attack();
            _ai.TEMP_attackSpeed = false;
            _ai.RapperstartCrution(WaitForAttackSpeed());

        }


     
        return null;
    }



    private Transform InAttackDist()
    {
        return AIsettings.InAttackDist(origin: transform,
                                       direcion: Vector3.forward,
                                       tagTarget: "Player",
                                       numberOfrays: AIsettings.AiSenseCone,
                                       distance: AIsettings.AttackRadius);
    }





    //=========================================
    //TEMP
    //=========================================
    IEnumerator WaitForAttackSpeed()
    {

        _ai.TEMP_attackbar.value = 0;
     
        while (_ai.TEMP_attackbar.value != _ai.TEMP_attackbar.maxValue)
        {
            _ai.TEMP_attackbar.value += 0.1f;
            yield return new WaitForSeconds(_ai.TEMP_Stats.attackSpeed / 10f);
        }
        _ai.TEMP_attackSpeed = true;
    }
    
    

    void Attack()
    {
        _ai.TEMP_player.TakeDmg(_ai.TEMP_Stats.dmg);
        Debug.Log(_ai.Target.name + " takes " + _ai.TEMP_Stats.dmg + " dmg");
        _ai.TEMP_moveANDattack = false;
    }


}
