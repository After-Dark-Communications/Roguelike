using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public new string name;
    public string itemTag;
    [SerializeField] private uint value;
    [SerializeField] private string description;
    [SerializeField] private uint requirementLevel;
    [SerializeField] private byte maxStack;
    public bool isConsumable;
    public bool isArmor;

    public Sprite spriteImage;
    public Color spriteColor = new Color(1, 1, 1, 1);
    public int sortingOrder = 10;
}
