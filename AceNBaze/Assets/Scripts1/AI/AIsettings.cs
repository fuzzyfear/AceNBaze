using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIsettings : MonoBehaviour
{
    [SerializeField] private float aggroRadius = 4f;
    public static float AggroRadius => Instance.aggroRadius;



    [SerializeField] private float attackRadius = 2f;
    public static float AttackRadius => Instance.attackRadius;


    public static AIsettings Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }




    // =============================================================
    // AI senses 
    // =============================================================

    //Used for raycasts
    private Quaternion startAngle = Quaternion.AngleAxis(angle: -60, Vector3.up);
    public static Quaternion StartAngle => Instance.startAngle;

    //Used for raycasts
    private Quaternion stepAngle = Quaternion.AngleAxis(angle: 5, Vector3.up);
    public static Quaternion StepAngle => Instance.stepAngle;

    [Tooltip("sense radius cone")][Range(0, 73)]
    [SerializeField] private int aiSenseCone = 24;
    public static int AiSenseCone   => Instance.aiSenseCone;
    public static int aiSenseCirkle = 73;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="origin"> origen of the cast</param>
    /// <param name="direcion"> direction the cast should go to</param>
    /// <param name="tagTarget"> loking for hits with object that has this tag</param>
    /// <param name="numberOfrays"> how many steps the sense ray should check (starts att -60 degre and moves 5 degerse for each steap</param>
    /// <returns></returns>
    public static Transform InAttackDist(Transform origin, 
                                         Vector3   direcion, 
                                         string    tagTarget, 
                                         int       numberOfrays, 
                                         float     distance)
    {
        RaycastHit hit;
        var angle     = origin.rotation * AIsettings.StartAngle;
        var direction = angle * Vector3.forward;
        var pos       = origin.position;

        for (int i = 0; i < numberOfrays; ++i)
        {

            if (Physics.Raycast(origin: pos, direction, out hit, distance))
            {
                if (hit.collider.tag == tagTarget)
                {
                    Debug.DrawRay(start: pos, dir: direction * hit.distance, Color.red);
                    return hit.collider.transform;
                }
                else
                    Debug.DrawRay(start: pos, dir: direction * hit.distance, Color.yellow);
            }
            else
                Debug.DrawRay(start: pos, dir: direction * distance, Color.white);

            direction = AIsettings.StepAngle * direction;


        }
        return null;
    }


}
