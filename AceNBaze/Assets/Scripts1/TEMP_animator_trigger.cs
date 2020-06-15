using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_animator_trigger : MonoBehaviour
{

    //skript endast gjor för demot

    public PlayerController player;

    public Animator anim;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">attack,attackM, walk, idle, block, dash</param>
    public void setValue(string name)
    {
        switch (name)
        {
            case "attack":
                anim.SetFloat("A", 0f);
                break;
            case "block":
                anim.SetFloat("A", 0.4f);
                break;
            case "walk":
                anim.SetFloat("M", 0f);
                break;
            case "idle":
                anim.SetFloat("M", 0.5f);
                break;
            case "dash":
                anim.SetFloat("M", 1f);
                break;
            case "idleA":
                anim.SetFloat("A", 0.7f);
                break;
        }
    }

    public void attack()
    {
        player.realAttack();
        anim.SetFloat("A", 1f);
    }

   

}
