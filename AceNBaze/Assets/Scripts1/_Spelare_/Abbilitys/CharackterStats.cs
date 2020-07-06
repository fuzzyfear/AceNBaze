using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharackterStats : MonoBehaviour
{


  //NOTE: alla slides (stamina, weapon stamina m.m) har max värde 1 pga engklare att hantera då, varia deras längd med återhämtnings hastighetn 



    /// <summary>
    /// Temporary unit scriptable object system has ben implemented
    /// have puted in a bunche of things that could be need in the finel
    /// </summary>
    [System.Serializable]
    public struct Weapon
    {
        [Header("Stats variables")]
        public float coldownTime;   //Attack cooldown (using 100/attack)
        public int   weaponDamage;
        public float weaponRange;
        [Space]
        public int weaponWeight;  // later affect the speed, mabey remove
        public int weaponType;    // axe, sword, club
        public int weaponStatEffekt;     //ice, bleed, fire, water, 
        public int weaponStatEffetkPower; //how string is effekt
        
        [Space]
        [Space]
        [Header("Opperation variables")]
        [SerializeField] private float _attackStamina;
        private const float            _maxStamina = 1f;
        [SerializeField] private bool  _notRecovering;
        [SerializeField] private float _recoveringSpeed;
        public float attakcStamina
        {
            get
            {
                return _attackStamina;
            }
            set
            {
                _attackStamina    = value;
                _notRecovering = (_attackStamina == _maxStamina);
            }
        }
        public bool NotColldown => _notRecovering;



        public float collDownSpeed => _recoveringSpeed;

        public Weapon( float speed, int damage, float range)
        {

            _attackStamina = _maxStamina;
            _notRecovering = true;


            weaponType            = -1;
            weaponWeight          = -1;
            coldownTime           = speed;
            _recoveringSpeed      = _maxStamina/coldownTime;
            weaponDamage          = damage;
            weaponRange           = range;

            weaponStatEffekt      = -1;
            weaponStatEffetkPower = -1;


   
        }

    }





        [System.Serializable]
    public struct Stats
    {
        public int     level;
        public int     movmentSpeed;
        [Space]
        public int     maxHP     , currentHP;
        [Space]
        public float staminMax;
        public float staminaCurrent;
        [Tooltip("StaminaMax/BaseStats.staminaColldown")]
        public float staminaRecovery;
        [Space]
        public float dashTime;
        public float dashSpeed;
        [Tooltip("StaminaMax/dashTime")]
        public float dashStaminaDraineSpeed;
        [Space]
        public Weapon  weapon;               // for now redo to scriptable objet 
        public Vector3 armor;                // Vec3(type          , weight, protection)                   for now redo to scriptable objet 
        public Vector3[] statusEffekts;     // vec3(what to effekt, type of effekt, how mutch to effekt)  for now redo to scriptable objet 

        public void loadDefultStas(CharacterInfo BaseStats)
        {
            //TODO: Addera så att alla stas här går att läsa från character info
            level                  = 0;
            movmentSpeed           = BaseStats.movementSpeed;
            maxHP                  = BaseStats.HP;
            currentHP              = maxHP;
            staminMax              = BaseStats.staminaBaseMax;
            staminaCurrent         = staminMax;
            staminaRecovery        = staminMax / BaseStats.staminaColldown;
            dashTime               = BaseStats.baseDashTime;
            dashSpeed              = BaseStats.bashDashSpeed;
            dashStaminaDraineSpeed = staminMax / dashTime;
            armor                  = new Vector3(-1, 10, 200);
            statusEffekts          = new Vector3[] { };
        }

        /// <summary>
        /// Set wat weapon to use
        /// </summary>
        /// <param name="weapon"></param>
        public void SetWeapon(Weapon weapon)
        {
            this.weapon = weapon;
        }

    }


    [SerializeField] private Stats         _characterStats;
    [SerializeField] private CharacterInfo _baseStats;


    public Stats cStats { get { return _characterStats; }
                                  set { _characterStats = value; } }


    private void Start()
    {
        _characterStats.loadDefultStas(_baseStats);
        _characterStats.SetWeapon(new Weapon(_baseStats.attackColldown, _baseStats.dmg, _baseStats.attackRange));


    }

    //TODO: Fundera på att ta bort dessa, så att stats endast lagarar funktrioner
    // bygg möjligen in det i något
    /// <summary>
    /// Returns current movmentSpeed with status effekts 
    /// Example: base speed 10 
    ///          status effekts: -10% base speed and 5 speed
    ///          return speed = 10 - 10*0.1 +5 = 14 speed
    /// </summary>
    /// <returns></returns>
    public int GetMovmentSpeed()
    {
        //TODO: Add so status effekts effekts the speed;
        int currentSpeed = cStats.movmentSpeed;
        return currentSpeed;
    }
    /// <summary>
    /// Getter for base speed
    /// </summary>
    /// <returns></returns>
    public int GetBaseMovmentSpeed()
    {
        return cStats.movmentSpeed;
    }




    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.root.position, Vector3.forward * _characterStats.weapon.weaponRange);
    }


}
