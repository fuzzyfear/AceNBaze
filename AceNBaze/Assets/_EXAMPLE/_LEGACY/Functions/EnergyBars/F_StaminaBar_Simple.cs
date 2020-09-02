using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class F_StaminaBar_Simple : _FunctionBase
{

    public Slider staminaBar;


    public F_StaminaBar_Simple() : base() { }


    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {
        //if nothing is handeling the stamina (like dashing)
        if (!modifier.lockManager.SetStamina.SoftLock())
        {
            float newStamina = baseAbilitys.characterStats.cStats.staminaCurrent;

            //TODO: addera så att status effekter påverkar återhämtningen 
            #region Calculate recovery speed 
            float staminaEecovery = baseAbilitys.characterStats.cStats.staminaRecovery;
            #endregion

            newStamina = Mathf.Clamp01(newStamina+ staminaEecovery*Time.deltaTime);
         
            #region Set new Stamina
#if UNITY_EDITOR
            modifier.lockManager.SetStamina.UseAction(baseAbilitys, newStamina, _keyName);
#else
            modifier.lockManager.SetStamina.UseAction(baseAbilitys, newStamina, _keyHash);
#endif
            #endregion

        }

        //Temporary 
        if(staminaBar != null)
        {
            staminaBar.maxValue = baseAbilitys.characterStats.cStats.staminMax;
            staminaBar.value    = baseAbilitys.characterStats.cStats.staminaCurrent;  
        }




    }


}
