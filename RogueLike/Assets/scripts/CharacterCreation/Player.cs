using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Being
{
    //Unity Settings
    [SerializeField] private SpawnItems _itemSpawner;
    [SerializeField] private KeyCode _PickUp = KeyCode.G;
    //Basics
    public readonly int _ID;
    public readonly Enum _Class = null;
    //Stats
    public int _Mana;
    public int _Dex;
    public int _Magic;
    //High-Score
    public bool _Alive;
    public int _LowestFloorReached;
    public int _ExperienceLevel;
    public int _Kills;
    public int _Wealth;
    public string _killedBy;
    public readonly DateTime _StartDate;
    public DateTime _EndDate;
    public int _GameVersion;
    public Time _TimePlayed;
    //Inventory
    Interact _interact;
    public readonly byte gearSlotWeapon = 37;
    public readonly byte gearSlotArmor = 38;
    public Item[] _inventory;

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

        if (_Class.Equals(PlayerClassEnum.Warrior))
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
    private void Awake()
    {
        _interact = new Interact();
        _inventory = new Item[38];
    }
    private void Update()
    {
        if (Input.GetKeyDown(_PickUp))
        {
            PickUpItem();
        }
    }

    private void LevelUp()
    {

    }
    private void PickUpItem()
    {
        byte size = (byte)(_inventory.Length - 2);
        foreach (GameObject go in _itemSpawner._SpawnedGo)
        {
            if (go.transform.position.x == gameObject.transform.position.x && go.transform.position.y == gameObject.transform.position.y && go.activeSelf)
            {
                _interact.PickupItem(go, _inventory, size);
                return;
            }
        }
    }

    private void EquipItem(Item item)
    {
        byte assingedItemSlot = 0;
        if (!item.isArmor)
        {
            assingedItemSlot = gearSlotWeapon;
        }
        else
        {
            assingedItemSlot = gearSlotArmor;
        }
        _inventory[assingedItemSlot] = item;
    }

    new public void Die()
    {
        _Alive = false;
        _killedBy = ""; //GetKiller
        _EndDate = DateTime.Now;
        //timeplayed is end date min startdate;
    }
}
