using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{


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
                                                        { typeof(AttackState), new AttackState(ai: this) } };

        _brain = gameObject.AddComponent<StateMachine>();
        _brain.SetState(knowHowTo);


    }




    private void Update()
    {
        _brain.StateTick();
    }



}
