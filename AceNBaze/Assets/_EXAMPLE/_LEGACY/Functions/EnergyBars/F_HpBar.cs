using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class F_HpBar : _FunctionBase
{

    public Slider HpSlider;


    public F_HpBar() : base() { }


    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {
      

        //Temporary 
        if (HpSlider != null)
        {
            HpSlider.maxValue = baseAbilitys.characterStats.cStats.maxHP;
            HpSlider.value    = baseAbilitys.characterStats.cStats.currentHP;
        }




    }

}
