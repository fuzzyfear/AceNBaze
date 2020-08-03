using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Parent class for the input system. 
/// player and ai will inherant from this class.
/// </summary>
public abstract class InputInterfaceParent : MonoBehaviour
{










    /// <summary>
    /// Base function to indicate movment.
    /// for player will it be a rapper for input.getkey(movmentButton)
    /// </summary>
    /// <returns>True if movment action was triggerd</returns>
    public abstract bool Move();


    /// <summary>
    /// Base function to intract.
    /// <returns>True if interaction action was triggerd</returns>
    public abstract bool Interact();


    #region Attacks

    /// <summary>
    /// genereic base attack 1
    /// </summary>
    /// <returns></returns>
    public abstract bool Attack_1();
    /// <summary>
    /// genereic base attack 2
    /// </summary>
    /// <returns></returns>
    public abstract bool Attack_2();
    /// <summary>
    /// genereic base attack 3
    /// </summary>
    /// <returns></returns>
    public abstract bool Attack_3();
    /// <summary>
    /// genereic base attack 4
    /// </summary>
    /// <returns></returns>
    public abstract bool Attack_4();
    /// <summary>
    /// genereic base attack 5
    /// </summary>
    /// <returns></returns>
    public abstract bool Attack_5();
    /// <summary>
    /// genereic base attack 6
    /// </summary>
    /// <returns></returns>
    public abstract bool Attack_6();
    /// <summary>
    /// genereic base attack 7
    /// </summary>
    /// <returns></returns>
    public abstract bool Attack_7();
    /// <summary>
    /// genereic base attack 8
    /// </summary>
    /// <returns></returns>
    public abstract bool Attack_8();
    /// <summary>
    /// genereic base attack 9
    /// </summary>
    /// <returns></returns>
    public abstract bool Attack_9();
    /// <summary>
    /// genereic base attack 10
    /// </summary>
    /// <returns></returns>
    public abstract bool Attack_10();

    #endregion
}
