using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Containts all the controll buttons, 
/// is sigle ton
/// maybe chanign later 
/// 
/// </summary>
public class Controlls : MonoBehaviour
{
    public static Controlls instanse;

    public Controlls()
    {
        if(instanse == null)
        {
            instanse = this;
        }
        else
        {
            Debug.LogError("Attemted to create second controller");
        }
    }


    

    [SerializeField] private KeyCode MOVMENT_KEY      = KeyCode.Mouse1;
    [SerializeField] private KeyCode MOVMENT_KEY_dash = KeyCode.Space;
    [SerializeField] private KeyCode ATTACK_KEY       = KeyCode.Mouse0;
    [SerializeField] private KeyCode BLOCK_KEY        = KeyCode.B;


    public KeyCode movment => MOVMENT_KEY;
    public KeyCode dash    => MOVMENT_KEY_dash;
    public KeyCode attack  => ATTACK_KEY;
    public KeyCode block   => BLOCK_KEY;


}
