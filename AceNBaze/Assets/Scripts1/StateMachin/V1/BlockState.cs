using System;

using System.Collections;
using UnityEngine;

public class BlockState : BaseState
{

    AI _ai;
    bool startBlok = true;
    public BlockState(AI ai) : base(ai.gameObject)
    {
        _ai = ai;
    }


    public override Type Tick()
    {
        if (startBlok)
        {
            startBlok = false;
            _ai.blockDone = false;
            _ai.RapperstartCrution(AI_blocking());
        }
 

        if (_ai.blockDone)
        {
            _ai.TEMP_desitionValue = 0;
            startBlok = true;
            return typeof(AttackState);
        }



        return null;
    }




    IEnumerator AI_blocking()
    {
        _ai.blocking = true;
        while (_ai.TEMP_Blockbar.value != _ai.TEMP_Blockbar.minValue)
        {
            _ai.TEMP_Blockbar.value -= 0.1f;
            yield return new WaitForSeconds(_ai.TEMP_Stats.attackSpeed / 10f);
        }
        _ai.blocking = false;
        _ai.blockDone = true;
        //laddar dash energin
        while (_ai.TEMP_Blockbar.value != _ai.TEMP_Blockbar.maxValue)
        {
            _ai.TEMP_Blockbar.value += 0.1f;
            yield return new WaitForSeconds(_ai.TEMP_Stats.attackSpeed / 10f);
        }
    

    }





}
