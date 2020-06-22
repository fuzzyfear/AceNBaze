using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// Simple click to move skript
/// </summary>
public class AttackClick : _FunctionBase
{

    public AttackClick() : base() { }

    public override void Tick(CharacterBaseAbilitys stats, LockManager modifier)
    {

        if (Input.GetKeyDown(Controlls.instanse.attack))
        {

            Vector3    mouse = Input.mousePosition;
            Ray        castPoint = stats.camar.ScreenPointToRay(mouse);
            RaycastHit hit;

            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                //if (attackSpeed)
                //{
                //    if (hit.collider.gameObject.layer == 11)
                //    {
                //        attackTarget = hit;
                //        moveAndAttack = true;
                //    }
                //    else
                //    {
                //        Debug.Log("Miss, no enemmy selected");
                //        attackSpeed = false;
                //        StartCoroutine(WaitForAttackSpeed());
                //    }
                //}
            }

        }
       
    }
}
