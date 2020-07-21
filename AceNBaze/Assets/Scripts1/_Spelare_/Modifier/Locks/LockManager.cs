using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{
#if UNITY_EDITOR
    #region DEBUG (DEBUG_GetLocks)
    /// <summary>
    /// Debug to eneable the displayment of the locks in the editor
    /// 
    /// </summary>
    /// <returns></returns>
    public Lock[] DEBUG_GetLocks()
    {
        return new Lock[] {
                           SetTransformLookAt                 ,
                           SetTransformRotationFromVector     ,
                           SetTransformRotationFromQuaternion ,
                                                              
                            SetAgentMovingDestination         ,
                            SetAgentIsStopped                 ,
                            SetAgentUppdateRotation           ,
                            SetAgentMovingSpeed               ,
                            SetAgentMove                      ,
                                                              
                            SetLife                           ,
                            SetStamina                        ,
                            SetAttackCollDown                 ,
                            ApplayDamage                      ,
                                                              
                            SetAnimationVariableInt           ,
                            SetAnimationVariableFloat         ,
                            SetAnimationVariableBool          ,
                            SetAnimationVariableTrigger       ,

                            SetParry                           };

    }
    #endregion
#endif
    //NOT: DONT FORGET TO ADSetAnimationVariableTriggerD NEW LOCKS TO THE DEBUG_GetLocks FUNCTION!!!!!

    #region functions on Transform locks 
    /// <summary>
    /// <para>_Summarey: rapper for LookAt</para>
    /// <para><b>_Action Input description_</b></para>
    /// <para>    Vector3 : the point to lok att</para>
    /// </summary> 
    public Lock<Vector3>                SetTransformLookAt                 = new Lock<Vector3>                ("SetTransformLookAt"                 , SetTransformLookAtAction                 );
    /// <summary>
    /// <para>_Summarey: read the name</para>
    /// <para><b>_Action Input description_</b></para>
    /// <para>    Vector3 : the new rotation</para>
    /// </summary> 
    public Lock<Vector3>                SetTransformRotationFromVector     = new Lock<Vector3>                ("SetTransformRotationFromVector"     , SetTransformRotationFromVectorAction     );
    /// <summary>
    /// <para>_Summarey: read the name</para>
    /// <para><b>_Action Input description_</b></para>
    /// <para>    Quaternion : the new rotation</para>
    /// </summary>                                       
    public Lock<Quaternion>             SetTransformRotationFromQuaternion = new Lock<Quaternion>             ("SetTransformRotationFromQuaternion" , SetTransformRotationFromQuaternionAction );
    #endregion

    #region functions on agnet locks 
    /// <summary>
    /// <para>_Summarey: rapper for agent.SetDestination</para>
    /// <para><b>_Action Input description_</b></para>
    /// <para>    Vector3 : the new destination</para>
    /// </summary>                                                                                                 
    public Lock<Vector3>                SetAgentMovingDestination          = new Lock<Vector3>                ("SetAgentMovingDestination"          , SetAgentMovingDestinationAction          );
    /// <summary>
    /// <para>_Summarey: rapper for agent.isStopped  </para>
    /// <para><b>_Action Input description_</b></para>
    /// <para>    bool  : agent.IsStopped sets to this value</para>
    /// </summary>                                                                                                
    public Lock<bool>                   SetAgentIsStopped                  = new Lock<bool>                   ("SetAgentIsStopped"                  , SetAgentIsStoppedAction                  );
    /// <summary>
    /// <para>_Summarey: rapper for agent.updateRotation  </para>
    /// <para><b>_Action Input description_</b></para>
    /// <para>    bool  : aagent.updateRotation sets to this value</para>
    /// </summary>    
    public Lock<bool>                   SetAgentUppdateRotation            = new Lock<bool>                   ("SetAgentUppdateRotation"            , SetAgentUppdateRotationAction            );
    /// <summary>
    /// <para>_Summarey: rapper for agent.speed,can applay status effekts on speedefor setting it  </para>
    /// <para><b>_Action Input description_</b></para>
    /// <para>    float : New speed, set to -1 to use default movment speed from the character stats</para>
    /// <para>    bool  : Shulde status effekt be applayd  defult true, (like -10% momvent)</para>
    /// </summary>                                                                                                
    public Lock<float,bool>             SetAgentMovingSpeed                = new Lock<float,bool>             ("SetAgentMovingSpeed"                , SetAgentMovingSpeedAction                );
    /// <summary>
    /// <para>_Summarey: rapper for agent.Move</para>
    /// <para><b>_Action Input description_</b></para>
    /// <para>    float : New speed, set to -1 to use default movment speed from the character stats</para>
    /// <para>    bool  : Shulde status effekt be applayd  defult true, (like -10% momvent)</para>
    /// </summary>                                                                                                
    public Lock<Vector3>                SetAgentMove                       = new Lock<Vector3>                ("SetAgentMove"                       , SetAgentMoveAction                       );
    #endregion


    public Lock<int>                    SetLife                            = new Lock<int>                    ("SetLife"                            , SetLifeAction                            );
    public Lock<float>                  SetStamina                         = new Lock<float>                  ("SetStamina"                         , SetStaminaAction                         );

    #region weapons and damaga locks
    /// <summary>
    /// <para>_Summarey: uppdates curent weapon cooldonw</para>
    /// <para><b>_Action Input description_</b></para>
    /// <para>    float : the current "attack stamina"/attac colldown,</para>
    /// </summary>                                                                                                
    public Lock<float>                  SetAttackCollDown                  = new Lock<float>                  ("SetAttackCollDown"                  , SetAttackCollDownAction                  );
    /// <summary>
    /// <para>_Summarey: applays damage on character by hiting it with the given weapon</para>
    /// <para><b>_Action Input description_</b></para>
    /// <para>    CharackterStats.Weapon : the weapon that delt the attack</para>
    /// </summary>                                                                                                
    public Lock<CharackterStats.Weapon> ApplayDamage                       = new Lock<CharackterStats.Weapon> ("ApplayDamage"                       , ApplayDamageAction                       );
    #endregion


    #region animations locks
    public Lock<string, int>            SetAnimationVariableInt            = new Lock<string, int>            ("SetAnimationVariableInt"            , SetAnimationVariableIntAction            );
    public Lock<string, float>          SetAnimationVariableFloat          = new Lock<string, float>          ("SetAnimationVariableFloat"          , SetAnimationVariableFloatAction          );
    public Lock<string, bool>           SetAnimationVariableBool           = new Lock<string, bool>           ("SetAnimationVariableBool"           , SetAnimationVariableBoolAction           );
    public Lock<string>                 SetAnimationVariableTrigger        = new Lock<string>                 ("SetAnimationVariableBool"           , SetAnimationVariableTriggerAction        );
    #endregion



    #region Working stats functions locks
    public Lock<bool>                   SetParry                           = new Lock<bool>                  ("SetParryA"                          , SetParryAction                            );
    #endregion



    //===============================================================================================================
    //Actions
    //===============================================================================================================



    #region transform Actions
    static void SetTransformLookAtAction(CharacterBaseAbilitys characterBase, Vector3 lookAtMe)
    {
        characterBase.mainTransform.LookAt(lookAtMe);
    }
    static void SetTransformRotationFromVectorAction(CharacterBaseAbilitys characterBase, Vector3 dir)
    {
        characterBase.mainTransform.rotation = Quaternion.LookRotation(dir);
    }
    static void SetTransformRotationFromQuaternionAction(CharacterBaseAbilitys characterBase, Quaternion dir)
    {
        characterBase.mainTransform.rotation = dir;
    }
    #endregion

    #region movment agent stuff Actions
    static void SetAgentMovingDestinationAction(CharacterBaseAbilitys characterBase, Vector3 pos)
    {
         characterBase.agent.SetDestination(pos);
    }
    static void SetAgentIsStoppedAction(CharacterBaseAbilitys characterBase, bool stopped)
    {
        characterBase.agent.isStopped = stopped;
    }
    static void SetAgentUppdateRotationAction(CharacterBaseAbilitys characterBase, bool uppdate)
    {
        characterBase.agent.updateRotation = uppdate;
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
    static void SetAgentMoveAction(CharacterBaseAbilitys characterBase, Vector3 dir)
    {
        characterBase.agent.Move(dir);
    }
    #endregion

    #region animation Actions
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
    #endregion



    #region stamina, life Actions
    static void SetLifeAction(CharacterBaseAbilitys characterBase, int hp)
    {
        CharackterStats.Stats stats = characterBase.characterStats.cStats;
        stats.currentHP = hp;
        characterBase.characterStats.cStats = stats;
    }
    static void SetStaminaAction(CharacterBaseAbilitys characterBase, float stamina)
    {
        CharackterStats.Stats stats = characterBase.characterStats.cStats;
        stats.staminaCurrent = stamina;
        characterBase.characterStats.cStats = stats;
    }
    #endregion

    #region weapons and damage Actions
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
        CharackterStats.Stats stats         = characterBase.characterStats.cStats;
        stats.currentHP                    -= damage.weaponDamage;
        characterBase.characterStats.cStats = stats;

    }
    #endregion



    #region Working stats functions Actions


    static void SetParryAction(CharacterBaseAbilitys characterBase, bool isParry)
    {
        characterBase.characterStats.cWstats.parry = isParry;
    }



    #endregion


}

#region not used now
/*Användes inte just nu pga satsar på modularitet, så räknar 
* om värdet istället. tar dock inte bort än pga kan tänkas 
* behöva användas om det visar sig vara för innefektivt med
* det andra.
*/
//public Lock<Vector3>                SetDirLook                         = new Lock<Vector3>                ("SetDirLook"                         , SetDirLookAction                         );
//public Lock<Vector3>                SetDirMoving                       = new Lock<Vector3>                ("SetDirMoving"                       , SetDirMovingAction                       );
#endregion

#region not used now
/*Användes inte just nu pga satsar på modularitet, så räknar 
 * om värdet istället. tar dock inte bort än pga kan tänkas 
 * behöva användas om det visar sig vara för innefektivt med
 * det andra.
 */
//static void SetDirLookAction(CharacterBaseAbilitys characterBase, Vector3 dir)
//{
//    characterBase.characterStats.dirLooking = dir;
//}
//static void SetDirMovingAction(CharacterBaseAbilitys characterBase, Vector3 dir)
//{
//    characterBase.characterStats.dirMoving = dir;
//}
#endregion