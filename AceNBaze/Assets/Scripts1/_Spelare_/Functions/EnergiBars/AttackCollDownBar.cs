using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// Refiles the attack stamina and uppdates the attack bar (for now)
/// </summary>
public class AttackCollDownBar : _FunctionBase
{
    public Slider AttackBar;

    private Coroutine recovering;

    public AttackCollDownBar() : base() { }

    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {

        if (!baseAbilitys.characterStats.cStats.weapon.NotColldown)
        {
            if (recovering != null)
                StopCoroutine(recovering);

            StartCoroutine(RefileAttackBare(baseAbilitys, modifier));
        }


        if(AttackBar != null)
        {
            AttackBar.value = baseAbilitys.characterStats.cStats.weapon.attakcStamina;
            
        }
    }







    IEnumerator RefileAttackBare(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {
        float colldown = baseAbilitys.characterStats.cStats.weapon.attakcStamina;


        float colldownSpeed = baseAbilitys.characterStats.cStats.weapon.collDownSpeed;

        while (!baseAbilitys.characterStats.cStats.weapon.NotColldown)
        {

            yield return new WaitForSeconds(colldownSpeed);
            colldown = Mathf.MoveTowards(colldown, 1f, 0.1f);//  Mathf.Clamp01(colldown + colldownSpeed);
            Debug.Log(colldown);
            modifier.lockManager.SetAttackCollDown.UseAction(baseAbilitys, colldown, _keyHash);


        }

    }


}
