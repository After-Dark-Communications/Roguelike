using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Interact
{
    private List<GameObject> _PlayerInventory { get { return _PlayerInventory; } set { new List<Item>(); } }
    private readonly byte _InventorySlots;
    private readonly string _ItemTag = "Item";

    public Interact(List<GameObject> inventory, byte TotalSlots)
    {
        _PlayerInventory = inventory;
        _InventorySlots = TotalSlots;
    }
    public Interact(byte TotalSlots)
    {
        _InventorySlots = 5;
        _PlayerInventory = new List<GameObject>();
    }
    /// <summary>
    /// Picks up the gear that the player is standing on.
    /// </summary>
    /// <param name="pickup">Item to try and pick up.</param>
    public void PickupItem(GameObject pickup) //Important
    {
        if (_PlayerInventory.Count >= _InventorySlots)
        {
            Debug.Log("Inventory full");
        }
        else
        {
            _PlayerInventory.Add(pickup);
        }
    }
    public void PickupItem(GameObject pickup, List<GameObject> Inventory)
    {
        if (Inventory.Count >= 5)
        {
            Debug.Log("Inventory full");
        }
        else
        {
            Inventory.Add(pickup);
        }
    }
    /// <summary>
    /// Try to open the door.
    /// </summary>
    public void TryOpenDoor(Tile Doortile, bool locked = false, bool Haskey = true)
    {
        //locked door: monochrome_288
        //unlocked door: monochrome_291
        //opened door: monochrome_290
        if (locked && !Haskey)
        {
            Debug.Log("no key, no entry.");
        }
        else if (locked && Haskey)
        {
            //open

        }
        else if (!locked)
        {
            //open
        }
    }
    /// <summary>
    /// Try to open a chest.
    /// </summary>
    public void TryOpenChest()
    {
        //chest: monochrome_200
        //DESTROY CHEST ON EXECUTE FUNCTION
    }
    /// <summary>
    /// Can the player equip the gear or not?
    /// </summary>
    /// <param name="PlayerLevel">The players current level</param>
    /// <param name="RequiredLevel">The level the player is required to be to equip the gear</param>
    /// <returns> returns wether or not the player is the right level to equip the gear</returns>
    public bool CanEquip(int PlayerLevel, int RequiredLevel)
    {
        if (RequiredLevel > PlayerLevel)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    /// <summary>
    /// Equip the gear in the specified slot and unequips the item in said slot if need be.
    /// </summary>
    /// <param name="OccupiedSlot">Is the slot occupied or not.</param>
    /// <param name="GearSlot">The slot to put the item in.</param>
    public void EquipGear(bool OccupiedSlot, byte GearSlot)//Important
    {

    }
    /// <summary>
    /// Equips the gear in the specified slot, regardless of wether or not there is an item in it.
    /// </summary>
    /// <param name="GearSlot">The slot to put the item in.</param>
    public void EquipGear(byte GearSlot)
    {

    }
    /// <summary>
    /// Unequips the gear in the specified slot
    /// </summary>
    /// <param name="GearSlot">The slot that the gear is in.</param>
    /// <param name="DestroyItem">Wether or not the unequiped item will be destroyed.</param>
    public void UnEquipGear(byte GearSlot, bool DestroyItem = false)//Important
    {

    }
    /// <summary>
    /// Uses the item.
    /// </summary>
    public void UseItem(Item item)//Important
    {

    }
    /// <summary>
    /// Drops the selected item.
    /// </summary>
    public void DropItem()//Important
    {

    }
    /// <summary>
    /// Sell the selected item.  
    /// </summary>
    /// <param name="ItemValue">The value of the item being sold.</param>
    public void SellItem(int ItemValue, Item itemToSell)
    {

    }
    /// <summary>
    /// Buys the selected item.
    /// </summary>
    /// <param name="ItemValue">The value of the item being bought.</param>
    public void BuyItem(int ItemValue, Item itemToBuy)
    {

    }

    /// <summary>
    /// wether or not the item can be unequiped.
    /// </summary>
    /// <param name="FreeInventorySlot">the amount of free inventory slots.</param>
    /// <param name="CursedItem">wethere the item is cursed or not.</param>
    /// <returns>Returns wethere the item can be unequiped</returns>
    public bool CanUnequip(int FreeInventorySlot, bool CursedItem = false)
    {
        return true;
    }

    /// <summary>
    /// Wether or not the item can be used (i.e. a health potion when you health is full)
    /// </summary>
    /// <returns>Returns if the item can be used.</returns>
    public bool CanUse()
    {
        return true;
    }
    /// <summary>
    /// Wether or not the item can be dropped on the ground.
    /// </summary>
    /// <returns></returns>
    public bool CanDrop()
    {
        return true;
    }

}
