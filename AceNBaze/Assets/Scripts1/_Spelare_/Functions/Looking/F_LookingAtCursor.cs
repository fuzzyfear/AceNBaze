using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Uppdastes the looking diraction, makeing the player looke in
/// the mouses direction. 
/// </summary>
public class F_LookingAtCursor : _FunctionBase
{
    public F_LookingAtCursor() : base() { }

    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {

        Vector3 dir = modifier.commonFunctionMethods.GetDirAgentToMouse(baseAbilitys);
        modifier.lockManager.SetTransformRotationFromVector.UseAction(baseAbilitys, dir, _keyHash);

    }

  

}
