using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public new string name;
    public string itemTag = "Item";
    [SerializeField] private uint value;
    [SerializeField] private string description;
    [SerializeField] private uint requirementLevel;
    [SerializeField] private byte maxStack;
    public bool isConsumable;
    public bool isArmor;

    [Header("Visual Display")]
    public Sprite spriteImage;
    public Color spriteColor = new Color(1, 1, 1, 1);
    public int sortingOrder = 10;

    [Header("Item Effects")]
    /* 
     * giveExpPercentage
     * opensChest ???
     */
    public bool opensDoor;
    [Tooltip("Use nagative value if you want to take damage, i.e. poision")]
    public int dealDamage;
    public int range;

    public int giveGold;
    public int giveExpFlat;
    public int giveLevel;
    public int restoreMana;
    public int increaseMaxMana;
    [Tooltip("Use nagative value if you want to decrease Strength")]
    public int modifyStrength;
    [Tooltip("Use nagative value if you want to decrease Dex")]
    public int modifyDex;
    [Tooltip("Use nagative value if you want to decrease Magic")]
    public int modifyMagic;
    public int restoreHealth;
    public int increaseMaxHealth;
}
