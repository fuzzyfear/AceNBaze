﻿using System;
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

        public LayerMask GrundMask => _grundMask;
        public LayerMask EnemyMask => _enemyMask;
        public LayerMask ItemMask  => _itemMask;

    }




    [System.Serializable]
    public struct OperationFalgs
    {
        bool paring;
    }





    [SerializeField] private layerMaskes     _layerMaskes;
    [Space]
    [SerializeField] private NavMeshAgent    _agnet;
    [SerializeField] private CharackterStats _character;
    [SerializeField] private Animator        _animator;
    [SerializeField] private Camera          _camar;
    [SerializeField] private Transform       _mainTransform;


    /// <summary>
    /// Do not change valus in agent directliy, use actions throug the lock manager to do what.
    /// </summary>
    public NavMeshAgent agent         => _agnet;
    /// <summary>
    /// Do not change valus in animator directliy, use actions throug the lock manager to do what.
    /// </summary>
    public Animator     animator      => _animator;
    /// <summary>
    /// Do not change valus in camar directliy, use actions throug the lock manager to do what.
    /// </summary>
    public Camera       camar         => _camar;
    public layerMaskes  maskes        => _layerMaskes;
    /// <summary>
    /// Do not change valus in mainTransform directliy, use actions throug the lock manager to do what.
    /// </summary>
    public Transform    mainTransform => _mainTransform;

    public CharackterStats characterStats
    {
        get { return _character; }
        set { _character = value; }
    }









}



