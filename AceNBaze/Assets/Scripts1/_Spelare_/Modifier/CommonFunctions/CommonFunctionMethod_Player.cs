using System.Collections;
using System.Collections.Generic;
using UnityEngine;





/// <summary>
/// Common functionalitys for the player
/// </summary>
public class CommonFunctionMethod_Player : CommonFunctionMethods
{

    /// <summary>
    /// For the player is this a simple rapper for GetDirAgentToMouse
    /// </summary>
    /// <param name="baseAbilitys"></param>
    /// <returns>Direction between the agnet and the mouse, a.i. the looking dir</returns>
    protected override Vector3 GetLookingDir(CharacterBaseAbilitys baseAbilitys)
    {
        return GetDirAgentToMouse(baseAbilitys);
    }
}
