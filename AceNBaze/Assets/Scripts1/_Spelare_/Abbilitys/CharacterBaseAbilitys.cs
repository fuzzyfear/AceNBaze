using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterBaseAbilitys : MonoBehaviour
{
    [SerializeField] private NavMeshAgent    _agnet;
    [SerializeField] private CharackterStats _character;
    [SerializeField] private Animator        _animator;

    public NavMeshAgent agent => _agnet;
    public Animator animator  => _animator;
    public CharackterStats character
    {
        get { return _character; }
        set { _character = value; }
    }

 


}



