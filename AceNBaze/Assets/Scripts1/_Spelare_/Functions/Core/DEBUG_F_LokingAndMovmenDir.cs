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


   public Vector3 f2;
   public Vector3 b2;
   public Vector3 r2;
   public Vector3 l2;
   public Vector3 fr2;
   public Vector3 fl2;
   public Vector3 br2;
   public Vector3 bl2;

    public float fm;
    public float bm;
    public float rm;
    public float lm;
    public float frm;
    public float flm;
    public float brm;
    public float blm;



    public DEBUG_F_LokingAndMovmenDir() : base() { }

    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {

         LokingDir = modifier.commonFunctionMethods.GetDirAgentToMouse(baseAbilitys);
         MovingDir = modifier.commonFunctionMethods.GetAgentMovingDir(baseAbilitys);


        Vector3 pos = baseAbilitys.mainTransform.position;




        #region Parer directions
        Vector3 f = new Vector3(  0, 0,  1 )     ;
        Vector3 b = new Vector3(  0, 0, -1 )     ;
        Vector3 r = new Vector3(  1, 0,  0 )     ;
        Vector3 l = new Vector3( -1, 0,  0 )     ;
        float grad = 0.7f;
        Vector3 fr = new Vector3( grad, 0,  grad);
        Vector3 fl = new Vector3(-grad, 0,  grad);
        Vector3 br = new Vector3( grad, 0, -grad);
        Vector3 bl = new Vector3(-grad, 0, -grad);
 
        Debug.DrawRay(pos, r *2f, Color.green);
        Debug.DrawRay(pos, l *2f, Color.green);
        Debug.DrawRay(pos, f *2f, Color.green);
        Debug.DrawRay(pos, b *2f, Color.green);
        Debug.DrawRay(pos, br*2f, Color.green);
        Debug.DrawRay(pos, bl*2f, Color.green);
        Debug.DrawRay(pos, fl*2f, Color.green);
        Debug.DrawRay(pos, fr*2f, Color.green);
        #endregion


        float[] test = modifier.commonFunctionMethods.GetCharacterParyData(baseAbilitys);

        fm = test[0];// f2.magnitude;
        bm = test[1];//b2.magnitude;
        rm = test[2];//r2.magnitude;
        lm = test[3];//l2.magnitude;
        frm = test[4];//fr2.magnitude;
        flm = test[5];//fl2.magnitude;
        brm = test[6];//br2.magnitude;
        blm = test[7];//bl2.magnitude;

        #region how mutch is agent turned toward parer directions
        f2  = f  * fm;// Mathf.Clamp01( Vector3.Dot(f, LokingDir ));// new Vector3(  0                           , 0,  Mathf.Clamp01(LokingDir.z)  );
        b2  = b  * bm;// Mathf.Clamp01( Vector3.Dot(b, LokingDir ));//new Vector3(  0                           , 0, -Mathf.Clamp01(-LokingDir.z) );
        r2  = r  * rm;// Mathf.Clamp01( Vector3.Dot(r, LokingDir ));//new Vector3(  Mathf.Clamp01(LokingDir.x)  , 0,  0                           );
        l2  = l  * lm;// Mathf.Clamp01( Vector3.Dot(l, LokingDir ));//new Vector3( -Mathf.Clamp01(- LokingDir.x), 0,  0                           );
        fr2 = fr * frm;// Mathf.Clamp01( Vector3.Dot(fr, LokingDir));
        fl2 = fl * flm;// Mathf.Clamp01( Vector3.Dot(fl, LokingDir));
        br2 = br * brm;// Mathf.Clamp01( Vector3.Dot(br, LokingDir));
        bl2 = bl * blm;// Mathf.Clamp01( Vector3.Dot(bl, LokingDir));


    

        


        Debug.DrawRay(pos, r2  * 2f, Color.magenta);
        Debug.DrawRay(pos, l2  * 2f, Color.magenta);
        Debug.DrawRay(pos, f2  * 2f, Color.magenta);
        Debug.DrawRay(pos, b2  * 2f, Color.magenta);
        Debug.DrawRay(pos, br2 * 2f, Color.magenta);
        Debug.DrawRay(pos, bl2 * 2f, Color.magenta);
        Debug.DrawRay(pos, fl2 * 2f, Color.magenta);
        Debug.DrawRay(pos, fr2 * 2f, Color.magenta);
        #endregion



        Debug.DrawRay(pos, LokingDir * 2, Color.blue);
        Debug.DrawRay(pos, MovingDir * 2, Color.red);


    }
}
