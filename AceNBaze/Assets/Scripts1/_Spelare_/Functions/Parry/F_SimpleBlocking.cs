using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_SimpleBlocking : _FunctionBase
{
    [Tooltip("in sec")]
    public float temp_blockTime = 0.5f;


  

    

    public F_SimpleBlocking() : base() { }

    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {
        if (!baseAbilitys.characterStats.cWstats.parry && Input.GetKeyDown(Controlls.instanse.block))
        {
            StartCoroutine(Blocking(baseAbilitys, modifier));
        }
       
    }


    private IEnumerator Blocking(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {

        modifier.lockManager.SetParry.UseAction(baseAbilitys, true, _keyHash);

        yield return new WaitForSeconds(temp_blockTime);


        modifier.lockManager.SetParry.UseAction(baseAbilitys, false, _keyHash);


    }
}
