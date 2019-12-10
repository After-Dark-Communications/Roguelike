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

    public byte gearSlotWeapon = 37;
    public byte gearSlotArmor = 38;
    Item[] inventory = new Item[38];

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

    /// <summary>
    /// Equip an item, on specific slot
    /// </summary>
    /// <param name="itemSlot"></param>
    /// <param name="item"></param>
    public void PickUpItem(Item item)
    {
        int openArray = 0;

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                openArray = i;
                break;
            }
        }
        inventory[openArray] = item;
    }
    /// <summary>
    /// Equip an item
    /// </summary>
    /// <param name="item"></param>
    public void EquipItem(Item item)
    {
        byte assingedItemSlot = 0;
        if (!item.isArmor)
        {
            assingedItemSlot = gearSlotWeapon;
        } else
        {
            assingedItemSlot = gearSlotArmor;
        }
        inventory[assingedItemSlot] = item;
    }

    new public void Die()
    {
        _Alive = false;
        _killedBy = ""; //GetKiller
        _EndDate = DateTime.Now;
        //timeplayed is end date min startdate;
    }
}
