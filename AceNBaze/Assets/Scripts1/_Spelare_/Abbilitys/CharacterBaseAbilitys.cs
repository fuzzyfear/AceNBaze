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

        public LayerMask GrundMask => _grundMask;
        public LayerMask EnemyMask => _enemyMask;
        public LayerMask ItemMask  => _itemMask;

    }




    [SerializeField] private layerMaskes     _layerMaskes;
    [Space]
    [SerializeField] private NavMeshAgent    _agnet;
    [SerializeField] private CharackterStats _character;
    [SerializeField] private Animator        _animator;
    [SerializeField] private Camera          _camar;
    [SerializeField] private Transform       _mainTransform;


    public NavMeshAgent agent         => _agnet;
    public Animator     animator      => _animator;
    public Camera       camar         => _camar;
    public layerMaskes  maskes        => _layerMaskes;
    public Transform    mainTransform => _mainTransform;







    public CharackterStats characterStats
    {
        get { return _character; }
        set { _character = value; }
    }


    private void Start()
    {
        _agnet.speed = _character.GetMovmentSpeed();
    
    }


}



