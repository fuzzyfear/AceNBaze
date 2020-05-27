using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerControll_dash : MonoBehaviour
{
    public Camera        cam;
    public NavMeshAgent  agent;
    public CharacterInfo playerStats;
    public Slider        hp;
    public LayerMask     enemy;


    [SerializeField]private KeyCode MOVMENT_KEY_dash = KeyCode.Space;
    [SerializeField]private KeyCode MOVMENT_KEY      = KeyCode.Mouse1;
    [SerializeField]private KeyCode ATTACK_KEY       = KeyCode.Mouse0;


    [SerializeField] private float dashDistanse = 4f;
    private float sqrDashDistnase;
    private bool dashing = false;
    private float walkingSpeedNORMAL;
    private float walkingAccelerationNORMAL;

    public void Start()
    {
       
        sqrDashDistnase = dashDistanse * dashDistanse;
  


        agent.speed = playerStats.movementSpeed;
        hp.maxValue = playerStats.HP;
        hp.value    = hp.maxValue;
   
    }

    // Update is called once per frame
    void Update()
    {



        if (MoveDash())
            return;
        
        

        MoveToMouse();
        Attack();
    }


    bool MoveDash()
    {
        if (dashing)
        {
            if(agent.remainingDistance == 0)
            {
              agent.speed = walkingSpeedNORMAL;
                agent.acceleration = walkingAccelerationNORMAL;
                dashing = false;
            }
        }
        else if (Input.GetKey(MOVMENT_KEY_dash) && Input.GetKeyDown(MOVMENT_KEY) == true)
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = cam.ScreenPointToRay(mouse);
            RaycastHit hit;
  
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                // 
             
                float dist = (agent.transform.position - hit.point).sqrMagnitude;
        
                if(dist <= sqrDashDistnase )
                {

              
                    walkingSpeedNORMAL = agent.speed;
                    agent.speed =  walkingSpeedNORMAL * 10f;
                    walkingAccelerationNORMAL = agent.acceleration;
                    agent.acceleration =  agent.acceleration*10f;
                    agent.SetDestination(hit.point);
                    dashing = true;
                }
  
                

            }
        }


       





        return dashing;
    }





    void MoveToMouse()
    {
        if (Input.GetKeyDown(MOVMENT_KEY) == true)
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = cam.ScreenPointToRay(mouse);
            RaycastHit hit;

            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(ATTACK_KEY) == true)
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = cam.ScreenPointToRay(mouse);
            RaycastHit hit;

            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, enemy))
            {
                Debug.Log(hit.collider.gameObject.name + " takes " + playerStats.dmg + " dmg");
                hit.collider.gameObject.GetComponent<TargetDummyBehaviour>().TakeDmg(playerStats.dmg);
            }
        }
    }
}
