using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// DEBUG CLASS remove from final game
/// </summary>

public class DEBUG_LokingAndMovmenDir : _FunctionBase
{

    [Header("ONLY FOR DEBUGG, REMOVE BEFOR DOIN FINAL BUILD")]
    [SerializeField] private Vector3 LokingDir;
    [SerializeField] private Vector3 MovingDir;

    public DEBUG_LokingAndMovmenDir() : base() { }

    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {

         LokingDir = modifier.commonFunctionMethods.GetDirAgentToMouse(baseAbilitys);
         MovingDir = modifier.commonFunctionMethods.GetAgentMovingDir(baseAbilitys);


        Vector3 pos = baseAbilitys.mainTransform.position;

        Debug.DrawRay(pos, LokingDir*5f, Color.blue);
        Debug.DrawRay(pos, MovingDir*5f, Color.red);



        Vector3 f = new Vector3(  0, 0,  1 );
        Vector3 b = new Vector3(  0, 0, -1 );
        Vector3 r = new Vector3(  1, 0,  0 );
        Vector3 l = new Vector3( -1, 0,  0 );

        Vector3 fr = new Vector3( 0.5f, 0,  0.5f);
        Vector3 fl = new Vector3(-0.5f, 0,  0.5f);
        Vector3 br = new Vector3( 0.5f, 0, -0.5f);
        Vector3 bl = new Vector3(-0.5f, 0, -0.5f);
 


        Debug.DrawRay(pos, r*4f, Color.green);
        Debug.DrawRay(pos, l*4f, Color.green);
        Debug.DrawRay(pos, f*4f, Color.green);
        Debug.DrawRay(pos, b*4f, Color.green);
        Debug.DrawRay(pos, br*4f, Color.green);
        Debug.DrawRay(pos, bl*4f, Color.green);
        Debug.DrawRay(pos, fl*4f, Color.green);
        Debug.DrawRay(pos, fr*4f, Color.green);











    }
}
