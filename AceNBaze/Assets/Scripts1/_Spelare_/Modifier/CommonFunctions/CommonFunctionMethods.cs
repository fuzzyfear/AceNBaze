using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Contains common functions that will be by more then one function
/// </summary>
public class CommonFunctionMethods : MonoBehaviour
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

        Vector3 mheading = (castPoint.GetPoint(dist) - playerPos);
        float   mdist    = mheading.magnitude;

        return mheading / mdist;
    }
}
