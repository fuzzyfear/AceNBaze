using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Parent class for the input system. 
/// player and ai will inherant from this class.
/// </summary>
public abstract class InputInterfaceParent : MonoBehaviour
{

    /* All actions/inputs have three versions: click, hold and release
     * witche will corespronde to the functons in Input.getkey..

     */

    public virtual bool Move_Click() { return false; }
    public virtual bool Move_Hold() { return false; }
    public virtual bool Move_Release() { return false; }


    public virtual bool Interact_Click() { return false; }
    public virtual bool Interact_Hold() { return false; }
    public virtual bool Interact_Release() { return false; }


    /// <summary>
    /// Base function to parry (click)
    /// </summary>
    /// <returns></returns>
    public virtual bool Parry_Click() { return false; }
    public virtual bool Parry_Hold() { return false; }
    public virtual bool Parry_Release() { return false; }

    #region abilitys

    /* Abilitys is things like dashe, teleport, status buff and so on
     * esentialy any thing that isent part of the grund functionality
     * (move, interact and parry) but isen an attack)
     */


    public virtual bool Ability_1_Click() { return false; }
    public virtual bool Ability_1_Hold() { return false; }
    public virtual bool Ability_1_Release() { return false; }

    public virtual bool Ability_2_Click() { return false; }
    public virtual bool Ability_2_Hold() { return false; }
    public virtual bool Ability_2_Release() { return false; }


    public virtual bool Ability_3_Click() { return false; }
    public virtual bool Ability_3_Hold() { return false; }
    public virtual bool Ability_3_Release() { return false; }


    public virtual bool Ability_4_Click() { return false; }
    public virtual bool Ability_4_Hold() { return false; }
    public virtual bool Ability_4_Release() { return false; }


    public virtual bool Ability_5_Click() { return false; }
    public virtual bool Ability_5_Hold() { return false; }
    public virtual bool Ability_5_Release() { return false; }


    public virtual bool Ability_6_Click() { return false; }
    public virtual bool Ability_6_Hold() { return false; }
    public virtual bool Ability_6_Release() { return false; }


    public virtual bool Ability_7_Click() { return false; }
    public virtual bool Ability_7_Hold() { return false; }
    public virtual bool Ability_7_Release() { return false; }


    public virtual bool Ability_8_Click() { return false; }
    public virtual bool Ability_8_Hold() { return false; }
    public virtual bool Ability_8_Release() { return false; }


    public virtual bool Ability_9_Click() { return false; }
    public virtual bool Ability_9_Hold() { return false; }
    public virtual bool Ability_9_Release() { return false; }
    #endregion


    #region Attacks

    public virtual bool Attack_1_Click() { return false; }
    public virtual bool Attack_1_Hold() { return false; }
    public virtual bool Attack_1_Release() { return false; }

    public virtual bool Attack_2_Click() { return false; }
    public virtual bool Attack_2_Hold() { return false; }
    public virtual bool Attack_2_Releasek() { return false; }

    public virtual bool Attack_3_Click() { return false; }
    public virtual bool Attack_3_Hold() { return false; }
    public virtual bool Attack_3_Release() { return false; }

    public virtual bool Attack_4_Click() { return false; }
    public virtual bool Attack_4_Hold() { return false; }
    public virtual bool Attack_4_Release() { return false; }

    public virtual bool Attack_5_Click() { return false; }
    public virtual bool Attack_5_Hold() { return false; }
    public virtual bool Attack_5_Release() { return false; }

    public virtual bool Attack_6_Click() { return false; }
    public virtual bool Attack_6_Hold() { return false; }
    public virtual bool Attack_6_Release() { return false; }

    public virtual bool Attack_7_Click() { return false; }
    public virtual bool Attack_7_Hold() { return false; }
    public virtual bool Attack_7_Release() { return false; }

    public virtual bool Attack_8_Click() { return false; }
    public virtual bool Attack_8_Hold() { return false; }
    public virtual bool Attack_8_Release() { return false; }

    public virtual bool Attack_9_Click() { return false; }
    public virtual bool Attack_9_Hold() { return false; }
    public virtual bool Attack_9_Release() { return false; }

    #endregion


    #region Functionality

    /* Inputs that is usefule to have 
     * but dosent do anny thing special 
     */


    public virtual bool AnyInput_Click() { return false; }
    public virtual bool AnyInput_Down()  { return false; }

    #endregion
}
