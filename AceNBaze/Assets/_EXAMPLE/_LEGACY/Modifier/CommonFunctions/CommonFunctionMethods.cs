﻿using System.Collections;
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
    /// <para>Totaly 8 directions </para>
    /// <para>returns a float array(parydata):         </para>
    /// <para>parydata[0] world space forward          </para>
    /// <para>parydata[1] world space backlwards       </para>
    /// <para>parydata[2] world space right            </para>
    /// <para>parydata[3] world space leaft            </para>
    /// <para>parydata[4] world space forward-right    </para>
    /// <para>parydata[5] world space forward-leaft    </para>
    /// <para>parydata[5] world space backlwards-right </para>
    /// <para>parydata[5] world space backlwards-leaft </para>
    /// <para> Each dir range between 0 -> 1, grades how strong the parry/attack is in that dir</para>
    /// </summary> 
    /// <returns>float list with pary data</returns>
    public float[] GetCharacterParryData(Vector3 LookingDir)
    {
        float[] parydata  = new float[8];

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
        parydata[0] = (forward_x        * LookingDir.x) + (forward_z        * LookingDir.z);
        parydata[1] = (backward_x       * LookingDir.x) + (backward_z       * LookingDir.z);
        parydata[2] = (right_x          * LookingDir.x) + (right_z          * LookingDir.z);
        parydata[3] = (leaft_x          * LookingDir.x) + (leaft_z          * LookingDir.z);
        parydata[4] = (forward_right_x  * LookingDir.x) + (forward_right_z  * LookingDir.z);
        parydata[5] = (forward_leaft_x  * LookingDir.x) + (forward_leaft_z  * LookingDir.z);
        parydata[6] = (backward_right_x * LookingDir.x) + (backward_right_z * LookingDir.z);
        parydata[7] = (backward_leaft_x * LookingDir.x) + (backward_leaft_z * LookingDir.z);
        #endregion

        #region clamps out negativ valus
        parydata[0] = Mathf.Clamp01(parydata[0]);
        parydata[1] = Mathf.Clamp01(parydata[1]);
        parydata[2] = Mathf.Clamp01(parydata[2]);
        parydata[3] = Mathf.Clamp01(parydata[3]);
        parydata[4] = Mathf.Clamp01(parydata[4]);
        parydata[5] = Mathf.Clamp01(parydata[5]);
        parydata[6] = Mathf.Clamp01(parydata[6]);
        parydata[7] = Mathf.Clamp01(parydata[7]);
        #endregion
        return parydata;
    }






    public bool InAttackRange(CharacterBaseAbilitys baseAbilitys,Vector3 LookingDir)
    {
        float[] parydata = new float[8];

        #region diraction variables
        const float forward_x = 0f, forward_z = 1f;
        const float backward_x = 0f, backward_z = -1f;
        const float right_x = 1f, right_z = 0f;
        const float leaft_x = -1f, leaft_z = 0f;

        const float forward_right_x = 0.7f, forward_right_z = 0.7f;
        const float forward_leaft_x = -0.7f, forward_leaft_z = 0.7f;
        const float backward_right_x = 0.7f, backward_right_z = -0.7f;
        const float backward_leaft_x = -0.7f, backward_leaft_z = -0.7f;
        #endregion
        Vector3 point = baseAbilitys.mainTransform.position;

        #region calcs dot(dir, lookingdir)
        parydata[0] = (forward_x * LookingDir.x) + (forward_z * LookingDir.z);
        parydata[1] = (backward_x * LookingDir.x) + (backward_z * LookingDir.z);
        parydata[2] = (right_x * LookingDir.x) + (right_z * LookingDir.z);
        parydata[3] = (leaft_x * LookingDir.x) + (leaft_z * LookingDir.z);
        parydata[4] = (forward_right_x * LookingDir.x) + (forward_right_z * LookingDir.z);
        parydata[5] = (forward_leaft_x * LookingDir.x) + (forward_leaft_z * LookingDir.z);
        parydata[6] = (backward_right_x * LookingDir.x) + (backward_right_z * LookingDir.z);
        parydata[7] = (backward_leaft_x * LookingDir.x) + (backward_leaft_z * LookingDir.z);
        #endregion

        #region clamps out negativ valus
        parydata[0] = Mathf.Clamp01(parydata[0]);
        parydata[1] = Mathf.Clamp01(parydata[1]);
        parydata[2] = Mathf.Clamp01(parydata[2]);
        parydata[3] = Mathf.Clamp01(parydata[3]);
        parydata[4] = Mathf.Clamp01(parydata[4]);
        parydata[5] = Mathf.Clamp01(parydata[5]);
        parydata[6] = Mathf.Clamp01(parydata[6]);
        parydata[7] = Mathf.Clamp01(parydata[7]);
        #endregion




        List<Ray> attackDis = new List<Ray>();

        bool returnValue;


        if (parydata[0] > 0.0f) { attackDis.Add(new Ray(point, new Vector3(forward_x, 0, forward_z))); }//castForward
        if (parydata[1] > 0.0f) { attackDis.Add(new Ray(point, new Vector3(backward_x, 0, backward_z))); }//castbackward
        if (parydata[2] > 0.0f) { attackDis.Add(new Ray(point, new Vector3(right_x, 0, right_z))); }//castRight
        if (parydata[3] > 0.0f) { attackDis.Add(new Ray(point, new Vector3(leaft_x, 0, leaft_z))); }//castLeaft
        if (parydata[4] > 0.0f) { attackDis.Add(new Ray(point, new Vector3(forward_right_x, 0, forward_right_z))); }//castForward_right
        if (parydata[5] > 0.0f) { attackDis.Add(new Ray(point, new Vector3(forward_leaft_x, 0, forward_leaft_z))); }//castForward_leaft
        if (parydata[6] > 0.0f) { attackDis.Add(new Ray(point, new Vector3(backward_right_x, 0, backward_right_z))); }//castBackward_right
        if (parydata[7] > 0.0f) { attackDis.Add(new Ray(point, new Vector3(backward_leaft_x, 0, backward_leaft_z))); }//castBackward_leaft


    
 

        
        return true;
    }


}
