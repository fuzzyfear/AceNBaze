using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// DEBUG CLASS remove from final game
/// </summary>

public class DEBUG_F_LokingAndMovmenDir : _FunctionBase
{

    [Header("ONLY FOR DEBUGG, REMOVE BEFOR DOIN FINAL BUILD")]
    [SerializeField] private Vector3 LokingDir;
    [SerializeField] private Vector3 MovingDir;



    [SerializeField] private Vector3[] dirs2 = new Vector3[8];


        [SerializeField] private Vector3[] dirs = {new Vector3( 0.0f, 0.0f,  1.0f),
                      new Vector3( 0.0f, 0.0f, -1.0f),
                      new Vector3( 1.0f, 0.0f,  0.0f),
                      new Vector3(-1.0f, 0.0f,  0.0f),
                      new Vector3( 0.7f, 0.0f,  0.7f),
                      new Vector3(-0.7f, 0.0f,  0.7f),
                      new Vector3( 0.7f, 0.0f, -0.7f),
                      new Vector3(-0.7f, 0.0f, -0.7f) };


    // blockCubes[0] world space forward          
    // blockCubes[1] world space backlwards       
    // blockCubes[2] world space right            
    // blockCubes[3] world space leaft            
    // blockCubes[4] world space forward-right    
    // blockCubes[5] world space forward-leaft    
    // blockCubes[5] world space backlwards-right 
    // blockCubes[5] world space backlwards-leaft 
    [SerializeField] private GameObject[] blockCubes = new GameObject[8];



 

    public DEBUG_F_LokingAndMovmenDir() : base()
    {
      

    }

    private void Start()
    {
        for (int i = 0; i < blockCubes.Length; ++i)
        {
                blockCubes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(blockCubes[i].GetComponent<BoxCollider>());
            //blockCubes[i].SetActive(false);
        }
    }


    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {

         LokingDir = modifier.commonFunctionMethods.GetDirAgentToMouse(baseAbilitys);
         MovingDir = modifier.commonFunctionMethods.GetAgentMovingDir(baseAbilitys);


        Vector3 pos = baseAbilitys.mainTransform.position;
    



        //Dirs
        for (int i = 0; i < 8; ++i)
            Debug.DrawRay(pos, dirs[i] * 2f, Color.green);



        float[] test = modifier.commonFunctionMethods.GetCharacterDirectionData(LokingDir);
        for (int i = 0; i < 8; ++i)
            dirs2[i] = dirs[i] * test[i];

        // how mutch looking in the dirs
        for (int i = 0; i < 8; ++i)
            Debug.DrawRay(pos, dirs2[i] * 2f, Color.magenta);


        for (int i = 0; i < 8; ++i)
        {
            blockCubes[i].transform.position = pos + dirs2[i] * 2f;
            blockCubes[i].transform.localScale = new Vector3(test[i], test[i]*((baseAbilitys.characterStats.cWstats.parry) ? 4f: 1f) , test[i]);
        }




            Debug.DrawRay(pos, LokingDir * 2, Color.blue);
        Debug.DrawRay(pos, MovingDir * 2, Color.red);


    }
}
