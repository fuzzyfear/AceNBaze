using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterBaseAbilitys : MonoBehaviour
{
    [System.Serializable]
    public struct layerMaskes
    {
        [SerializeField] private LayerMask _grundMask;
        [SerializeField] private LayerMask _enemyMask;
        [SerializeField] private LayerMask _itemMask;

        public LayerMask grundMask => _grundMask;
        public LayerMask enemyMask => _enemyMask;
        public LayerMask itemMask  => _itemMask;

    }




    [SerializeField] private layerMaskes     _layerMaskes;
    [Space]
    [SerializeField] private NavMeshAgent    _agnet;
    [SerializeField] private CharackterStats _character;
    [SerializeField] private Animator        _animator;
    [SerializeField] private Camera          _camar;


    public NavMeshAgent agent    => _agnet;
    public Animator     animator => _animator;
    public Camera       camar    => _camar;
    public layerMaskes  maskes   => _layerMaskes;

    public CharackterStats character
    {
        get { return _character; }
        set { _character = value; }
    }

 


}



