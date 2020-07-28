using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Contains common functions that will be by more then one function.
/// </summary>
public  class CommonFunctionMethods : MonoBehaviour
{

    /// <summary>
    /// Returns the movment direction
    /// </summary>
    /// <param name="baseAbilitys"></param>
    /// <returns> direction the agent is movint in </returns>
    public Vector3 GetAgentMovingDir(CharacterBaseAbilitys baseAbilitys)
    {
        //Dosent ned to be its own method, but it make it 
        //easier to read the code, and easyer to change this 
        //part if needed
        return baseAbilitys.agent.velocity.normalized;
    }


    /// <summary>
    /// Gets the direction between the agent and the mouse cursor on plane that 
    /// gous throug the agents position 
    /// </summary>
    /// <param name="baseAbilitys"></param>
    /// <returns> the direction between the agent and the mouse cursor</returns>
    public Vector3 GetDirAgentToMouse(CharacterBaseAbilitys baseAbilitys)
    {
        Vector3 playerPos = baseAbilitys.agent.transform.position;
        Plane playerPlane = new Plane(baseAbilitys.mainTransform.up, playerPos);

        Ray castPoint = baseAbilitys.camar.ScreenPointToRay(Input.mousePosition);
        float dist;

        //Asume that all the raycast will hit
        playerPlane.Raycast(castPoint, out dist);

        Vector3 mheading = (castPoint.GetPoint(dist) - playerPos);// (castPoint.GetPoint(dist) - playerPos);
        float   mdist    = mheading.magnitude;

        return mheading / mdist;
    }

    /// <summary>
    /// <para>Returns how mutch parry/attack strenght the character currently has in diffrent directions </para>
    /// <para>Totaly 8 directions and the index of the looking dir </para>
    /// <para>returns a float array(direction facing data):         </para>
    /// <para>parydata[0] world space forward                       </para>
    /// <para>parydata[1] world space forward-right                 </para>
    /// <para>parydata[2] world space right                         </para>
    /// <para>parydata[3] world space backlwards-right              </para>
    /// <para>parydata[4] world space backlwards                    </para>
    /// <para>parydata[5] world space backlwards-leaft              </para>
    /// <para>parydata[6] world space leaft                         </para>
    /// <para>parydata[7] world space forward-leaft                 </para>
    /// <para>parydata[8] index of looking dir value 0->7           </para>
    /// <para> Each dir range between 0 -> 1, grades how strong the parry/attack is in that dir</para>
    /// </summary> 
    /// <returns>float list with pary data</returns>
    public float[] GetCharacterDirectionData(Vector3 LookingDir)
    {
        float[] dirFacingData  = new float[9];

        #region dirFacingData_visulated
        // X is the character and [0] -> [7] is the data from the list
        //           [0]
        //        [7]   [1]
        //     [6]    X    [2]
        //        [5]   [3]
        //           [4]
        #endregion
  

        #region diraction variables
        const float forward_x  =  0f, forward_z  = 1f;
        const float backward_x =  0f, backward_z = -1f;
        const float right_x    =  1f, right_z    = 0f;
        const float leaft_x    = -1f, leaft_z    = 0f;

        const float forward_right_x  =  0.7f, forward_right_z = 0.7f;
        const float forward_leaft_x  = -0.7f, forward_leaft_z = 0.7f;
        const float backward_right_x =  0.7f, backward_right_z = -0.7f;
        const float backward_leaft_x = -0.7f, backward_leaft_z = -0.7f;
        #endregion

        #region calcs dot(dir, lookingdir)
        dirFacingData[0] = (forward_x        * LookingDir.x) + (forward_z        * LookingDir.z);
        dirFacingData[1] = (forward_right_x  * LookingDir.x) + (forward_right_z  * LookingDir.z);
        dirFacingData[2] = (right_x          * LookingDir.x) + (right_z          * LookingDir.z);
        dirFacingData[3] = (backward_right_x * LookingDir.x) + (backward_right_z * LookingDir.z);
        dirFacingData[4] = (backward_x       * LookingDir.x) + (backward_z       * LookingDir.z);
        dirFacingData[5] = (backward_leaft_x * LookingDir.x) + (backward_leaft_z * LookingDir.z);
        dirFacingData[6] = (leaft_x          * LookingDir.x) + (leaft_z          * LookingDir.z);
        dirFacingData[7] = (forward_leaft_x  * LookingDir.x) + (forward_leaft_z  * LookingDir.z);
        #endregion


        #region clamps out negativ valus
        dirFacingData[0] = Mathf.Clamp01(dirFacingData[0]);
        dirFacingData[1] = Mathf.Clamp01(dirFacingData[1]);
        dirFacingData[2] = Mathf.Clamp01(dirFacingData[2]);
        dirFacingData[3] = Mathf.Clamp01(dirFacingData[3]);
        dirFacingData[4] = Mathf.Clamp01(dirFacingData[4]);
        dirFacingData[5] = Mathf.Clamp01(dirFacingData[5]);
        dirFacingData[6] = Mathf.Clamp01(dirFacingData[6]);
        dirFacingData[7] = Mathf.Clamp01(dirFacingData[7]);
        #endregion


        #region calck looking dir index
        for (int i = 0; i < 8; ++i)
            if (dirFacingData[i] >= 0.9)
                dirFacingData[8] = i;

        #endregion




        return dirFacingData;
    }


    /// <summary>
    /// <para>Calculates how mutch damages that is applyed in every direktion</para>
    /// <para>Totaly 8 directions </para>
    /// <para>returns a float array(damage_data):         </para>
    /// <para>parydata[0] damage applyed world space forward                       </para>
    /// <para>parydata[1] damage applyed world space forward-right                 </para>
    /// <para>parydata[2] damage applyed world space right                         </para>
    /// <para>parydata[3] damage applyed world space backlwards-right              </para>
    /// <para>parydata[4] damage applyed world space backlwards                    </para>
    /// <para>parydata[5] damage applyed world space backlwards-leaft              </para>
    /// <para>parydata[6] damage applyed world space leaft                         </para>
    /// <para>parydata[7] damage applyed world space forward-leaft                 </para>
    /// </summary> 
    /// <returns>float list with damage data</returns>
    public float[] ParryAttack(float[] parryData, float parryStrengh, float[] attackData, float attackStrengh)
    {

        #region dirFacingData_visulated
        // X is the character and [0] -> [7] is the data from the list
        //           [0]
        //        [7]   [1]
        //     [6]    X    [2]
        //        [5]   [3]
        //           [4]
        #endregion
        float[] damage = new float[8];
        //Calculates damage in all directions 
        for (int i = 0; i < 8; ++i)
        {
            damage[i] =  attackData[(i + 4) % 8] * attackStrengh - parryData[i] * parryStrengh;
            damage[i] = (damage[i] < 0) ? 0 : damage[i];
        }
   

 

        return damage;
    }


}
