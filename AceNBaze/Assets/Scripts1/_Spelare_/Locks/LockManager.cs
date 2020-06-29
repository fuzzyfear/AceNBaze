using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{
    #region DEBUG (DEBUG_GetLocks)
    /// <summary>
    /// Debug to eneable the displayment of the locks in the editor
    /// 
    /// </summary>
    /// <returns></returns>
    public Lock[] DEBUG_GetLocks()
    {
        return new Lock[] { SetAgentMovingDestination  ,
                            SetAgentIsStopped          ,
                            SetAgentIsStopped          ,
                            SetAgentMovingSpeed        ,

                            SetLife                    ,
                            SetStamina                 ,
                            SetAttackCollDown          ,
                            ApplayDamage               ,

                            SetAnimationVariableInt    ,
                            SetAnimationVariableFloat  ,
                            SetAnimationVariableBool   ,
                            SetAnimationVariableTrigger };

    }
    #endregion
    //NOT: DONT FORGET TO ADSetAnimationVariableTriggerD NEW LOCKS TO THE DEBUG_GetLocks FUNCTION!!!!!
    /// <summary>
    /// <para><b>_Action Input description_</b></para>
    /// <para>    Vector3 : the new destination</para>
    /// </summary>
    public Lock<Vector3>                SetAgentMovingDestination   = new Lock<Vector3>                ("SetAgentMovingDestination" , SetAgentMovingDestinationAction   );
    /// <summary>
    /// <para><b>_Action Input description_</b></para>
    /// <para>    bool  : agent.IsStopped sets to this value</para>
    /// </summary>
    public Lock<bool>                   SetAgentIsStopped           = new Lock<bool>                   ("SetAgentIsStopped"         , SetAgentIsStoppedAction           );
    /// <summary>
    /// <para><b>_Action Input description_</b></para>
    /// <para>    float : New speed, set to -1 to use default movment speed from the character stats</para>
    /// <para>    bool  : Shulde status effekt be applayd  defult true, (like -10% momvent)</para>
    /// </summary>
    public Lock<float,bool>             SetAgentMovingSpeed         = new Lock<float,bool>             ("SetAgentMovingSpeed"       , SetAgentMovingSpeedAction         );

    public Lock<int>                    SetLife                     = new Lock<int>                    ("SetLife"                   , SetLifeAction                     );
    public Lock<float>                  SetStamina                  = new Lock<float>                  ("SetStamina"                , SetStaminaAction                  );
    /// <summary>
    /// <para><b>_Action Input description_</b></para>
    /// <para>    float : the current "attack stamina"/attac colldown,</para>
    /// </summary>
    public Lock<float>                  SetAttackCollDown           = new Lock<float>                  ("SetAttackCollDown"         , SetAttackCollDownAction           );
    /// <summary>
    /// <para><b>_Action Input description_</b></para>
    /// <para>    CharackterStats.Weapon : the weapon that delt the attack</para>
    /// </summary>
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
    static void SetAgentMovingSpeedAction(CharacterBaseAbilitys characterBase, float newSpeed = -1.0f, bool applayStatusEffeks = true)
    {
        float speed = (newSpeed == -1) ? characterBase.characterStats.cStats.movmentSpeed: newSpeed;

        if (applayStatusEffeks)
        {
            //TODO: Do so status effekts is applayed
        }

        characterBase.agent.speed = speed;
    }

    static void SetLifeAction(CharacterBaseAbilitys characterBase, int hp)
    {
        CharackterStats.Stats stats            = characterBase.characterStats.cStats;
        stats.currentHP                        = hp;
        characterBase.characterStats.cStats = stats;
    }
    static void SetStaminaAction(CharacterBaseAbilitys characterBase, float stamina)
    {
        CharackterStats.Stats stats            = characterBase.characterStats.cStats;
        stats.staminaCurrent                   = stamina;
        characterBase.characterStats.cStats = stats;
    }
    static void SetAttackCollDownAction(CharacterBaseAbilitys characterBase, float colldown)
    {
        CharackterStats.Stats stats         = characterBase.characterStats.cStats;
        stats.weapon.attakcStamina          = colldown;
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
