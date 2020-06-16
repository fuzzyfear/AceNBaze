using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_animator_trigger : MonoBehaviour
{

    //skript endast gjor för demot

    public PlayerController player;

    public Animator anim;

    /// <summary>
    /// attack, walk, idle, hurt, dash
    /// </summary>
    /// <param name="name"></param>
    public void setValue(string name)
    {
        switch (name)
        {
            case "attack":
                anim.SetFloat("B", 1f);
                break;
            case "hurt":
                anim.SetFloat("B", 0.75f);
                break;
            case "walk":
                anim.SetFloat("B", 0.25f);
                break;
            case "idle":
                anim.SetFloat("B", 0f);
                break;
            case "dash":
                anim.SetFloat("B", 0.5f);
                break;
        }
        //switch (name)
        //{
        //    case "attack":
        //        anim.SetFloat("A", 0f);
        //        anim.SetFloat("B", 0.75f);
        //        break;
        //    case "hurt":
        //        anim.SetFloat("A", 1f);
        //        anim.SetFloat("B", 0.75f);
        //        break;
        //    case "walk":
        //        anim.SetFloat("M", 0.5f);
        //        anim.SetFloat("B", 0f);
        //        break;
        //    case "idle":
        //        anim.SetFloat("M", 0f);
        //        anim.SetFloat("B", 0f);
        //        break;
        //    case "dash":
        //        anim.SetFloat("M", 0.75f);
        //        anim.SetFloat("B", 0f);
        //        break;
        //    case "idleA":
        //        anim.SetFloat("A", 0.7f);
        //        anim.SetFloat("B", 0.75f);
        //        break;
        //}
    }

    public void attack()
    {
        player.realAttack();
        anim.SetFloat("A", 1f);
    }

   

}
