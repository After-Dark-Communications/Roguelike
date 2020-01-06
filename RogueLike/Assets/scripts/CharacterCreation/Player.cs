﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class Player : Being
{
    //Unity Settings
    [Header("Basic Settings")]
    [SerializeField] private SpawnItems _itemSpawner;
    [SerializeField] private KeyCode _PickUp = KeyCode.G;
    [Header("Movement Settings")]
    [SerializeField] private float _MoveDelay = .2f;
    [SerializeField] private float _HorizontalMoveDistance = 1f;
    [SerializeField] private float _VerticalMoveDistance = 1f;
    [SerializeField] private Tilemap _WallTile;
    private bool _permitMove = false;
    private bool _StartedCoroutine = false;
    private Vector3 _newPos;
    
    //Basics
    public readonly int _ID;
    public readonly Enum _Class = null;
    //Stats
    [Header("Stats")]
    public int _MaxMana;
    public int _Mana;
    public int _Dex;
    public int _Magic;
    //High-Score
    [Header("High Score")]
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
    [Header("Inventory")]
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
            _Strength += 1;
        }
        if (_Class.Equals(PlayerClassEnum.Mage))
        {
            _Magic += 1;
        }
        if (_Class.Equals(PlayerClassEnum.Ranger))
        {
            _Dex += 1;
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
        _inventory = new Item[6];
    }
    private void Start()
    {
        _newPos = gameObject.transform.position;
    }
    private void Update()
    {
        GetMovementInput();
        if (Input.GetKeyDown(_PickUp))
        {
            PickUpItem();
        }
    }

    //private void LevelUp()
    //{
    //
    //}
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

    new public void Die()
    {
        _Alive = false;
        _killedBy = ""; //GetKiller
        _EndDate = DateTime.Now;
        //timeplayed is end date min startdate;
    }

    void GetMovementInput()
    {
        //Debug.DrawLine(new Vector3Int((int)transform.position.x, (int)transform.position.y, 0), new Vector3Int((int)transform.position.x + (int)Input.GetAxisRaw("Horizontal"), (int)transform.position.y + (int)Input.GetAxisRaw("Vertical"), 0), Color.white);
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (_permitMove)
            {
                TryMove();
            }
            if (!_StartedCoroutine)
            {
                StartCoroutine("Countdown");
            }
        }
        //}

    }

    void TryMove() //attempt to move in a given direction, checks if there is a wall or not
    {
        float X_Speed = Input.GetAxisRaw("Horizontal") * _HorizontalMoveDistance;
        float Y_Speed = Input.GetAxisRaw("Vertical") * _VerticalMoveDistance;

        if (!Occupied((int)X_Speed, (int)Y_Speed, _WallTile, this.transform))
        {
            _newPos += new Vector3(X_Speed, Y_Speed);
            gameObject.transform.position = _newPos + new Vector3(0, 0, -1);
        }
    }

    protected IEnumerator Countdown()
    {
        _StartedCoroutine = true;

        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            _permitMove = false;
            normalizedTime += Time.deltaTime / _MoveDelay;
            yield return null;
        }
        _permitMove = true;
        _StartedCoroutine = false;
    }
}
