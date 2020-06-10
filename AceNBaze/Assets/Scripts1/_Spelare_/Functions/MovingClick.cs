using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingClick : FunctionBase
{
    [SerializeField] private KeyCode MOVMENT_KEY = KeyCode.Mouse1;





    public MovingClick() : base() { }

    public override void Tick(CharacterInfo stats, ref FunctionTick.flags locks)
    {
       
    }




  //void MoveToMouse()
  //{

  //    //Temp ändring för att ändra hur du rör dig
  //    if (toogleMovment)
  //    {
  //        ConstatMovment = true;
  //        if (Input.GetKey(MOVMENT_KEY))
  //            uppdatemovementTarget = !uppdatemovementTarget;
  //    }
  //    else
  //    {
  //        uppdatemovementTarget = (ConstatMovment) ? Input.GetKey(MOVMENT_KEY) : Input.GetKeyDown(MOVMENT_KEY);
  //    }


  //    if (uppdatemovementTarget)
  //    {
  //        Vector3 mouse = Input.mousePosition;
  //        Ray castPoint = cam.ScreenPointToRay(mouse);
  //        RaycastHit hit;

  //        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
  //        {
  //            agent.SetDestination(hit.point);
  //        }
  //    }
  //}


}
