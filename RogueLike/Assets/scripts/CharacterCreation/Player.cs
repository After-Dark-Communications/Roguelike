using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    public class Player : Being
    {
        [SerializeField] private int _ID;
        [SerializeField] private Enum _Class = null;

        [SerializeField] private int _Mana;
        [SerializeField] private int _Dex;
        [SerializeField] private int _Magic;

        [SerializeField] private bool _Alive;
        [SerializeField] private int _LowestFloorReached;
        [SerializeField] private int _ExperienceLevel;
        [SerializeField] private int _Kills;
        [SerializeField] private int _Wealth;
        [SerializeField] private string _killedBy;
        [SerializeField] private DateTime _StartDate;
        [SerializeField] private DateTime _EndDate;
        [SerializeField] private int _GameVersion;
        [SerializeField] private Time _TimePlayed;


        public Player(string name, int classId)
        {
            _ID = 1; //.txt?
            _Class = (PlayerClassEnum)classId;
            _Name = name;

            _Health = 8;
            _Strength = 5;
            _Magic = 5;
            _Dex = 5;
            _Mana = 10;
            _ArmorClass = 3;

            _Experience = 0;
            _Gold = 10;

            if(_Class.Equals(PlayerClassEnum.Warrior))
            {
                _Strength = _Strength + 1;
            }
            if (_Class.Equals(PlayerClassEnum.Mage))
            {
                _Magic = _Magic + 1;
            }
            if (_Class.Equals(PlayerClassEnum.Ranger))
            {
                _Dex = _Dex + 1;
            }

            _Alive = true;
            _LowestFloorReached = 0;
            _ExperienceLevel = 0;
            _Kills = 0;
            _Wealth = _Gold;
            _StartDate = DateTime.Now;
            _GameVersion = 0; //GetGameVersion
        }

        public void PickUp()
        {

        }

        public void LevelUp()
        {

        }

        new public void Die()
        {
            _Alive = false;
            _killedBy = ""; //GetKiller
            _EndDate = DateTime.Now;
            //timeplayed is end date min startdate;
        }
    }
