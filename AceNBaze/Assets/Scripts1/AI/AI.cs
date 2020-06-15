using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AI : MonoBehaviour
{

    public TargetDummyBehaviour TEMP_player;
    public Slider           TEMP_attackbar;
    public Slider           TEMP_Blockbar;
    public CharacterInfo    TEMP_Stats;
    public bool             TEMP_attackSpeed;
    public bool             TEMP_moveANDattack;
    public Camera           TEMP_cam;
    public Vector3          TEMP_offestAttackSlider;
    public Vector3          TEMP_offestBlockSlider;

    public int TEMP_desitionValue = 0;
    public int TEMP_blockValue    = 2;

    public bool blockDone = true;
    public bool blocking
    {
        set
        {
            GetComponent<TargetDummyBehaviour>().blocking = value;
        }
    }



    [SerializeField] private NavMeshAgent _agent;
    public NavMeshAgent getAgent { get { return _agent; } }

    private StateMachine _brain;
    private Vector3      _destination;
    private Quaternion   _desierdRotation;
    private Vector3      _direction;
    private GameObject   _target;


    public GameObject Target { get { return _target;  } set { _target = value; } }



    void Start()
    {

        Dictionary<System.Type, BaseState> knowHowTo = new Dictionary<System.Type, BaseState>() {
                                                        { typeof(WanderState), new WanderState(ai: this) },
                                                        { typeof(ChaseState ), new ChaseState (ai: this) },
                                                        { typeof(AttackState), new AttackState(ai: this) },
                                                        { typeof(BlockState),  new BlockState (ai: this) } };

        _brain = gameObject.AddComponent<StateMachine>();
        _brain.SetState(knowHowTo);


        //TEMP
        TEMP_attackSpeed = true;
       
    }




    private void Update()
    {

        TEMP_attackbar.transform.position = TEMP_cam.WorldToScreenPoint(transform.position + TEMP_offestAttackSlider);
        TEMP_Blockbar.transform.position = TEMP_cam.WorldToScreenPoint(transform.position + TEMP_offestBlockSlider);
        _brain.StateTick();
    }



    public void RapperstartCrution(IEnumerator rutino)
    {

        StartCoroutine(rutino);
    }
    public void RapperStopCrution(IEnumerator rutino)
    {
        StopCoroutine(rutino);

    }

}
