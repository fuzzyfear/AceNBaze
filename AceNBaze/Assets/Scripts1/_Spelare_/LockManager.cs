using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{

    /// <summary>
    /// Debug to eneable the displayment of the locks in the editor
    /// 
    /// </summary>
    /// <returns></returns>
    public Lock[] DEBUG_GetLocks()
    {
        return new Lock[] { SetMovingDestination       ,
                            SetLife                    ,
                            SetStamina                 ,
                            SetAnimationVariableInt    ,
                            SetAnimationVariableFloat  ,
                            SetAnimationVariableBool   ,
                            SetAnimationVariableTrigger };

    }
    //NOT: DONT FORGET TO ADSetAnimationVariableTriggerD NEW LOCKS TO THE DEBUG_GetLocks FUNCTION!!!!!
    public Lock<Vector3>       SetMovingDestination        = new Lock<Vector3>      ("SetMovingDestination"      , SetMovingDestinationAction        );
    public Lock<int>           SetLife                     = new Lock<int>          ("SetLife"                   , SetLifeAction                     );
    public Lock<int>           SetStamina                  = new Lock<int>          ("SetStamina"                , SetStaminaAction                  );

    public Lock<string, int>   SetAnimationVariableInt     = new Lock<string, int>  ("SetAnimationVariableInt"   , SetAnimationVariableIntAction     );
    public Lock<string, float> SetAnimationVariableFloat   = new Lock<string, float>("SetAnimationVariableFloat" , SetAnimationVariableFloatAction   );
    public Lock<string, bool>  SetAnimationVariableBool    = new Lock<string, bool> ("SetAnimationVariableBool"  , SetAnimationVariableBoolAction    );
    public Lock<string>        SetAnimationVariableTrigger = new Lock<string>       ("SetAnimationVariableBool"  , SetAnimationVariableTriggerAction );

    
    static void SetMovingDestinationAction(CharacterBaseAbilitys characterBase, Vector3 pos)
    {
        characterBase.agent.SetDestination(pos);
    }

    static void SetLifeAction(CharacterBaseAbilitys characterBase, int hp)
    {
        CharackterStats.Stats stats            = characterBase.character.characterStats;
        stats.currentHP                        = hp;
        characterBase.character.characterStats = stats;
    }
    static void SetStaminaAction(CharacterBaseAbilitys characterBase, int stamina)
    {
        CharackterStats.Stats stats            = characterBase.character.characterStats;
        stats.currentStamina                   = stamina;
        characterBase.character.characterStats = stats;
    }

    static void SetAnimationVariableIntAction(CharacterBaseAbilitys characterBase, string variableName, int value)
    {
        characterBase.animator.SetInteger(variableName, value);
    }
    static void SetAnimationVariableFloatAction(CharacterBaseAbilitys characterBase, string variableName, float value)
    {
        characterBase.animator.SetFloat(variableName, value);
    }
    static void SetAnimationVariableBoolAction(CharacterBaseAbilitys characterBase, string variableName, bool value)
    {
        characterBase.animator.SetBool(variableName, value);
    }
    static void SetAnimationVariableTriggerAction(CharacterBaseAbilitys characterBase, string variableName)
    {
        characterBase.animator.SetTrigger(variableName);
    }
}
