using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiInputs : InputInterfaceParent
{



    //BEFOR_MERGE: notera jag började detta skript med avsikten att låta det simulera ett virtuelt keybord, så att AI "logiken" skulle pressa de virtuella knapparna för att trigga saker. dette för  




    [System.Serializable]
    public enum inputAction  { NON      ,       
                               MOVE     ,
                               INTERACT ,
                               PARRY    ,
                               ATTACK_1 ,
                               ATTACK_2 ,
                               ATTACK_3 ,
                               ATTACK_4 ,
                               ATTACK_5 ,
                               ATTACK_6 ,
                               ATTACK_7 ,
                               ATTACK_8 ,
                               ATTACK_9 ,
                               ABILITY_1,
                               ABILITY_2,
                               ABILITY_3,
                               ABILITY_4,
                               ABILITY_5,
                               ABILITY_6,
                               ABILITY_7,
                               ABILITY_8,
                               ABILITY_9,
                               }


    [System.Serializable]
    public enum usefulInputActions { ANY_ClICK, ANY_HOLD }




    const int numberOfActions = 8;
    [SerializeField] private inputAction[] actionsCLICK  = new inputAction[numberOfActions];
    [SerializeField] private inputAction[] actionsHOLD   = new inputAction[numberOfActions];
    [SerializeField] private inputAction[] actionsRELESE = new inputAction[numberOfActions];



    private void Start()
    {
        for (int i = 0; i < numberOfActions; ++i)
        {
            actionsCLICK[i]   = 0;
            actionsHOLD[i]    = 0;
            actionsRELESE[i]  = 0;
        }

    }





    public void PressKey(inputAction pressedKey)
    {
        int firstFree = -1;
        for(int i = 0; i < numberOfActions; ++i)
        {
            if(firstFree != -1 &&  actionsCLICK[i] == 0) { firstFree = i; }


            if(actionsCLICK[i] == pressedKey)
            {
                actionsHOLD[i] = pressedKey;
            }
        }
    }

    public void ReleseButton(inputAction releasedKey)
    {
        for (int i = 0; i < numberOfActions; ++i)
        {
            if (actionsCLICK[i] == releasedKey)
            {

            }
        }
    }




    /// <summary>
    /// Resets all the click events
    /// </summary>
    public void LateUpdate()
    {
       
    }

    




    public override bool Move_Click()
    {
        return base.Move_Click();
    }

    public override bool Interact_Click()
    {
        return base.Interact_Click();
    }

    public override bool Parry_Click()
    {
        return base.Parry_Click();
    }






    public override bool Attack_1_Click()
    {
        return base.Attack_1_Click();
    }


    public override bool Ability_1_Click()
    {
        return base.Ability_1_Click();
    }

   













}
