using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// Refiles the attack stamina and uppdates the attack bar (for now)
/// </summary>
public class F_AttackCollDownBar : _FunctionBase
{
    public Slider AttackBar;

    private bool _onGoingCorutin = false;

    public F_AttackCollDownBar() : base() { }

    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {

        if (!baseAbilitys.characterStats.cStats.weapon.NotColldown && !_onGoingCorutin)
        {
            StartCoroutine(RefileAttackBare(baseAbilitys, modifier));
        }


        if(AttackBar != null)
        {
            AttackBar.value = baseAbilitys.characterStats.cStats.weapon.attakcStamina;
            
        }
    }







    IEnumerator RefileAttackBare(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {
        _onGoingCorutin = true;
        float colldown = baseAbilitys.characterStats.cStats.weapon.attakcStamina;

        float colldownSpeed = baseAbilitys.characterStats.cStats.weapon.collDownSpeed;
 

        while (!baseAbilitys.characterStats.cStats.weapon.NotColldown)
        {



            yield return new WaitForSeconds(colldownSpeed);

            colldown = Mathf.MoveTowards(colldown, 1f, 0.1f);//  Mathf.Clamp01(colldown + colldownSpeed);
            modifier.lockManager.SetAttackCollDown.UseAction(baseAbilitys, colldown, _keyHash);

        }
        _onGoingCorutin = false;
    }


}
