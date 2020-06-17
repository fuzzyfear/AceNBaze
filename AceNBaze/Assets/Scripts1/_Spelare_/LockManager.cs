using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{




    Lock<Vector3>       SetMovingDestination        = new Lock<Vector3>      ("SetMovingDestination"      , SetMovingDestinationAction        );
    Lock<int>           SetLife                     = new Lock<int>          ("SetLife"                   , SetLifeAction                     );
    Lock<int>           SetStamina                  = new Lock<int>          ("SetStamina"                , SetStaminaAction                  );

    Lock<string, int>   SetAnimationVariableInt     = new Lock<string, int>  ("SetAnimationVariableInt"   , SetAnimationVariableIntAction     );
    Lock<string, float> SetAnimationVariableFloat   = new Lock<string, float>("SetAnimationVariableFloat" , SetAnimationVariableFloatAction   );
    Lock<string, bool>  SetAnimationVariableBool    = new Lock<string, bool> ("SetAnimationVariableBool"  , SetAnimationVariableBoolAction    );
    Lock<string>        SetAnimationVariableTrigger = new Lock<string>       ("SetAnimationVariableBool"  , SetAnimationVariableTriggerAction );




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
