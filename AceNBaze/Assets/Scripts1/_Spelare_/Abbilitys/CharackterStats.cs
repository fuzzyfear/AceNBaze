using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharackterStats : MonoBehaviour
{
    /// <summary>
    /// Temporary unit scriptable object system has ben implemented
    /// have puted in a bunche of things that could be need in the finel
    /// </summary>
    [System.Serializable]
    public struct Weapon
    {
        [Header("Stats variables")]
        public int weaponSpeed;   //Attack cooldown (using 1/attack)
        public int weaponDamage;
        [Space]
        public int weaponWeight;  // later affect the speed, mabey remove
        public int weaponType;    // axe, sword, club
        public int weaponStatEffekt;     //ice, bleed, fire, water, 
        public int weaponStatEffetkPower; //how string is effekt
        
        [Space]
        [Space]
        [Header("Opperation variables")]
        [SerializeField] private float _colldown;
        [SerializeField] private bool  _notColldown;
        public float Colldown
        {
            get
            {
                return _colldown;
            }
            set
            {
                _colldown = value;
                _notColldown = _colldown == 0f;
            }
        }
        public bool NotColldown => _notColldown;

        public Weapon( int speed, int damage)
        {
            weaponType            = -1;
            weaponWeight          = -1;
            weaponSpeed           = speed;
            weaponDamage          = damage;

            weaponStatEffekt      = -1;
            weaponStatEffetkPower = -1;


            _colldown    = 0;
            _notColldown = true;
        }

    }





        [System.Serializable]
    public struct Stats
    {
        public int     level;
        public int     movmentSpeed;
        public int     maxHP     , currentHP;
        public int     maxStamina, currentStamina;
        public Weapon  weapon;            // for now redo to scriptable objet 
        public Vector3 armor;            // Vec3(type          , weight, protection)                   for now redo to scriptable objet 
        public Vector3[] statusEffekts;  // vec3(what to effekt, type of effekt, how mutch to effekt)  for now redo to scriptable objet 

        public void loadDefultStas(CharacterInfo BaseStats)
        {
            //TODO: Addera så att alla stas här går att läsa från character info
            level          = 0;
            movmentSpeed   = BaseStats.movementSpeed;
            maxHP          = BaseStats.HP;
            currentHP      = maxHP;
            maxStamina     = BaseStats.dashCooldown;
            currentStamina = maxStamina;        
            armor          = new Vector3(-1, 10, 200);
            statusEffekts  = new Vector3[] { };
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


    public Stats characterStats { get { return _characterStats; }
                                  set { _characterStats = value; } }


    private void Start()
    {
        _characterStats.loadDefultStas(_baseStats);
        _characterStats.SetWeapon(new Weapon(_baseStats.attackSpeed, _baseStats.dmg));
    }


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
        int currentSpeed = characterStats.movmentSpeed;
        return currentSpeed;
    }
    /// <summary>
    /// Getter for base speed
    /// </summary>
    /// <returns></returns>
    public int GetBaseMovmentSpeed()
    {
        return characterStats.movmentSpeed;
    }







}
