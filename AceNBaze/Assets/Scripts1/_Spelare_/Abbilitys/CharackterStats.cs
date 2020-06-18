using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharackterStats : MonoBehaviour
{
    [System.Serializable]
    public struct Stats
    {
        public int     level;
        public int     maxHP     , currentHP;
        public int     maxStamina, currentStamina;
        public Vector3 wapen;            // vec3(type          , speed, damage)                        for now redo to scriptable objet 
        public Vector3 armor;            // Vec3(type          , weight, protection)                   for now redo to scriptable objet 
        public Vector3[] statusEffekts;  // vec3(what to effekt, type of effekt, how mutch to effekt)  for now redo to scriptable objet 


        public void loadDefultStas(CharacterInfo BaseStats)
        {
            //TODO: Addera så att alla stas här går att läsa från character info
            level = 0;
            maxHP = BaseStats.HP;
            currentHP = maxHP;
            maxStamina = BaseStats.dashCooldown;
            currentStamina = maxStamina;
            wapen = new Vector3(-1, BaseStats.attackSpeed, BaseStats.dmg);
            armor = new Vector3(-1, 10, 200);
            statusEffekts = new Vector3[] { };
        }
    }


    [SerializeField] private Stats         _characterStats;
    [SerializeField] private CharacterInfo _baseStats;


    public Stats characterStats { get { return _characterStats; }
                                  set { _characterStats = value; } }

    public CharackterStats(Stats playerStats)
    {
        _characterStats = playerStats;
    }
    private void Start()
    {
         characterStats.loadDefultStas(_baseStats);
    }
    //public CharackterStats()
    //{
    //    characterStats.loadDefultStas();

    //}







}
