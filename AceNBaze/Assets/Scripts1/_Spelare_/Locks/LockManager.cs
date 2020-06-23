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
        return new Lock[] { SetAgentMovingDestination  ,
                            SetLife                    ,
                            SetStamina                 ,
                            SetAnimationVariableInt    ,
                            SetAnimationVariableFloat  ,
                            SetAnimationVariableBool   ,
                            SetAnimationVariableTrigger };

    }
    //NOT: DONT FORGET TO ADSetAnimationVariableTriggerD NEW LOCKS TO THE DEBUG_GetLocks FUNCTION!!!!!
    public Lock<Vector3>                SetAgentMovingDestination   = new Lock<Vector3>                ("SetAgentMovingDestination" , SetAgentMovingDestinationAction   );
    public Lock<bool>                   SetAgentIsStopped           = new Lock<bool>                   ("SetAgentIsStopped"         , SetAgentIsStoppedAction           );
                                                                                                       
    public Lock<int>                    SetLife                     = new Lock<int>                    ("SetLife"                   , SetLifeAction                     );
    public Lock<int>                    SetStamina                  = new Lock<int>                    ("SetStamina"                , SetStaminaAction                  );
    public Lock<float>                  SetAttackCollDown           = new Lock<float>                  ("SetAttackCollDown"         , SetAttackCollDownAction           );
    public Lock<CharackterStats.Weapon> ApplayDamage                = new Lock<CharackterStats.Weapon> ("ApplayDamage"              , ApplayDamageAction                );
                                                        
                                                           
    public Lock<string, int>            SetAnimationVariableInt     = new Lock<string, int>            ("SetAnimationVariableInt"   , SetAnimationVariableIntAction     );
    public Lock<string, float>          SetAnimationVariableFloat   = new Lock<string, float>          ("SetAnimationVariableFloat" , SetAnimationVariableFloatAction   );
    public Lock<string, bool>           SetAnimationVariableBool    = new Lock<string, bool>           ("SetAnimationVariableBool"  , SetAnimationVariableBoolAction    );
    public Lock<string>                 SetAnimationVariableTrigger = new Lock<string>                 ("SetAnimationVariableBool"  , SetAnimationVariableTriggerAction );
                                                           

    static void SetAgentMovingDestinationAction(CharacterBaseAbilitys characterBase, Vector3 pos)
    {
        characterBase.agent.SetDestination(pos);
    }
    static void SetAgentIsStoppedAction(CharacterBaseAbilitys characterBase, bool stopped)
    {
        characterBase.agent.isStopped = stopped;
    }


    static void SetLifeAction(CharacterBaseAbilitys characterBase, int hp)
    {
        CharackterStats.Stats stats            = characterBase.characterStats.cStats;
        stats.currentHP                        = hp;
        characterBase.characterStats.cStats = stats;
    }
    static void SetStaminaAction(CharacterBaseAbilitys characterBase, int stamina)
    {
        CharackterStats.Stats stats            = characterBase.characterStats.cStats;
        stats.currentStamina                   = stamina;
        characterBase.characterStats.cStats = stats;
    }
    static void SetAttackCollDownAction(CharacterBaseAbilitys characterBase, float colldown)
    {
        CharackterStats.Stats stats = characterBase.characterStats.cStats;
        stats.weapon.attakcStamina = colldown;
        characterBase.characterStats.cStats = stats;
    }
    static void ApplayDamageAction(CharacterBaseAbilitys characterBase, CharackterStats.Weapon damage)
    {
        //TODO: uppdate weapon type later 
        //TODO: do so stats effekt plays in the damage resived
        CharackterStats.Stats stats = characterBase.characterStats.cStats;
        stats.currentHP -= damage.weaponDamage;
        characterBase.characterStats.cStats = stats;

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
