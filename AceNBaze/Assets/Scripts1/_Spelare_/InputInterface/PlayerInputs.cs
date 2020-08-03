using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : InputInterfaceParent
{
    

    [System.Serializable]
    public struct PlayerGameControlls
    {
        public KeyCode MOVMENT_KEY;
        public KeyCode PARRY_KEY;
        public KeyCode INTERACT_KEY;
        public KeyCode DASH_KEY;
        public KeyCode ATTACK_KEY;

    }



    [SerializeField]private PlayerGameControlls _controlls;



    //TODO: do so that the game 
    /// <summary>
    /// Sets the controlls for the player, 
    /// will be cald from setting script 
    /// </summary>
    /// <param name="controlls"></param>
    public void SetControlls(PlayerGameControlls controlls)
    {
        _controlls = controlls;
    }


    #region movment

    public override bool Move_Click()   { return Input.GetKey(_controlls.MOVMENT_KEY);     }
    public override bool Move_Hold()    { return Input.GetKeyDown(_controlls.MOVMENT_KEY); }
    public override bool Move_Release() { return Input.GetKeyUp(_controlls.MOVMENT_KEY);   }
  
    #endregion


    #region interact

    public override bool Interact_Click()   { return Input.GetKey(_controlls.INTERACT_KEY);     }
    public override bool Interact_Hold ()   { return Input.GetKeyDown(_controlls.INTERACT_KEY); }
    public override bool Interact_Release() { return Input.GetKeyUp(_controlls.INTERACT_KEY);   }
  
    #endregion


    #region parry

    public override bool Parry_Click()   { return Input.GetKey(_controlls.PARRY_KEY);     }
    public override bool Parry_Hold ()   { return Input.GetKeyDown(_controlls.PARRY_KEY); }
    public override bool Parry_Release() { return Input.GetKeyUp(_controlls.PARRY_KEY);   }
  
    #endregion


    #region abilitys

        #region dash
        public override bool Ability_1_Click()   { return Input.GetKey(_controlls.DASH_KEY);     }
        public override bool Ability_1_Hold ()   { return Input.GetKeyDown(_controlls.DASH_KEY); }
        public override bool Ability_1_Release() { return Input.GetKeyUp(_controlls.DASH_KEY);   }
        #endregion

    #endregion


    #region attack

        #region normal attack
        public override bool Attack_1_Click()   { return Input.GetKey(_controlls.ATTACK_KEY);     }
        public override bool Attack_1_Hold ()   { return Input.GetKeyDown(_controlls.ATTACK_KEY); }
        public override bool Attack_1_Release() { return Input.GetKeyUp(_controlls.ATTACK_KEY);   }
        #endregion

    #endregion


}
