using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Interact
{

    private readonly string _ItemTag = "Item";
    /// <summary>
    /// Picks up the gear that the player is standing on.
    /// </summary>
    /// <param name="pickup">Item to try and pick up.</param>
    /// <param name="_PlayerInventory">The players inventory.</param>
    /// <param name="_InventorySlots">How many slots the inventory has for items.(default of 36)</param>
    public void PickupItem(GameObject pickup, Item[] _PlayerInventory, byte _InventorySlots = 36) //Important
    {
        int FreeSlots = 0;
        for (int i = 0; i < _PlayerInventory.Length - 2; i++)
        {
            if (_PlayerInventory[i] == null)
            {
                FreeSlots++;
            }
        }

        if (pickup.GetComponent<Pickup>()._item.isArmor == false)//equip weapon
        {
            if (CanEquip(_PlayerInventory, pickup.GetComponent<Pickup>()))
            {
                EquipGear(pickup, _PlayerInventory, false, (byte)(_PlayerInventory.Length - 2));
                pickup.SetActive(false);
                return;
            }
        }
        else if (pickup.GetComponent<Pickup>()._item.isArmor == true)//equip armor
        {
            if (CanEquip(_PlayerInventory, pickup.GetComponent<Pickup>(), true))
            {
                EquipGear(pickup, _PlayerInventory, false, (byte)(_PlayerInventory.Length - 1));
                pickup.SetActive(false);
                return;
            }
        }


        if (FreeSlots >= 1)
        {
            for (int i = 0; i < _PlayerInventory.Length - 2; i++)
            {
                if (_PlayerInventory[i] == null)
                {
                    Item item = pickup.GetComponent<Pickup>()._item;
                    _PlayerInventory[i] = item;
                    Debug.Log("picked up " + pickup.name);
                    pickup.SetActive(false);
                    break;
                }
            }
        }
        else
        {
            Debug.Log("Inventory full");
        }
    }
    /// <summary>
    /// Try to open the door.
    /// </summary>
    public void TryOpenDoor(Item[] Inventory, GameObject door, bool locked = false)
    {
        //locked door: monochrome_288
        //unlocked door: monochrome_291
        //opened door: monochrome_290
        byte inventoryIndex = 0;
        bool Haskey = false;
        for (int i = 0; i < Inventory.Length - 2; i++)
        {
            if (Inventory[i]?.name == "Key") // does the same as if (Inventory[i] != null && Inventory[i].name == "Key")
            {
                Haskey = true;
                inventoryIndex = (byte)i;
                break;
            }
        }

        if (locked && Haskey == false)
        {
            Debug.Log("no key, no entry.");
        }
        else if (locked && Haskey == true)
        {
            //open door
            door.SetActive(false);
            Inventory[inventoryIndex] = null;
            Debug.Log("Door Opened");
        }
        else if (!locked)
        {
            door.SetActive(false);
            Debug.Log("Door Opened");
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
    /// Is the player of high enough level equip the gear or not?
    /// </summary>
    /// <param name="PlayerLevel">The players current level</param>
    /// <param name="RequiredLevel">The level the player is required to be to equip the gear</param>
    /// <returns> returns wether or not the player is the right level to equip the gear</returns>
    public bool CouldEquip(int PlayerLevel, int RequiredLevel)
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
    /// Returns wether the player can equip the gear or not.
    /// </summary>
    /// <param name="Inventory">The players inventory.</param>
    /// <param name="ItemToEquip">The Item to try and equip.</param>
    /// <param name="isArmor">Is the item to equip armor or not?</param>
    /// <returns>Returns wether the item can be equiped.</returns>
    public bool CanEquip(Item[] Inventory, Pickup ItemToEquip, bool isArmor = false)
    {
        if (ItemToEquip._item.isConsumable == false)
        {
            if (Inventory[Inventory.Length - 2] == null && isArmor == false)
            {
                return true;
            }
            else if (Inventory[Inventory.Length - 1] == null && isArmor == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }

    /// <summary>
    /// Equip the gear in the specified slot and unequips the item in said slot if need be.
    /// </summary>
    /// <param name="OccupiedSlot">Is the slot occupied or not.</param>
    /// <param name="GearSlot">The slot to put the item in.</param>
    public void EquipGear(GameObject pickup, Item[] Inventory, bool OccupiedSlot, byte GearSlot)//Important
    {
        if (Inventory[GearSlot] == null)
        {
            Item item = pickup.GetComponent<Pickup>()._item;
            Inventory[GearSlot] = item;
            Debug.Log("equiped " + pickup.name);
        }
    }
    /// <summary>
    /// Equips the gear in the specified slot, regardless of wether or not there is an item in it.
    /// </summary>
    /// <param name="GearSlot">The slot to put the item in.</param>
    /// <param name="Inventory">The players inventory.</param>
    /// <param name="item">The item to put in the gear</param>
    public void EquipGear(byte GearSlot, Item[] Inventory, Item item)
    {
        Inventory[GearSlot] = null;
        Inventory[GearSlot] = item;
    }
    /// <summary>
    /// Unequips the gear in the specified slot
    /// </summary>
    /// <param name="GearSlot">The slot that the gear is in.</param>
    /// <param name="Inventory">The players inventory.</param>
    public void UnEquipGear(byte GearSlot, Item[] Inventory)//Important
    {
        Item itemToUnEquip = Inventory[GearSlot];
        for (int i = 0; i < Inventory.Length - 2; i++)
        {
            if (Inventory[i] == null)
            {
                Inventory[i] = itemToUnEquip;
                Inventory[GearSlot] = null;
                Debug.Log("Unequiped item: " + itemToUnEquip.name);
                break;
            }
            else
            {
                Debug.Log("Inventory full, can't unequip");
            }
        }
    }
    /// <summary>
    /// Uses the item.
    /// </summary>
    public void UseItem(Item item, Player player)//Important
    {
        /* TODO:
         * public int dealDamage;
         * public byte range;
         */

        if (item.opensDoor)
        {
            // Open Door
        }
        player._Experience += item.giveExpFlat;
        player._ExperienceLevel += item.giveLevel;
        player._Wealth += item.giveGold;
        
        player._MaxHealth += item.increaseMaxHealth;
        player._MaxMana += item.increaseMaxMana;
        player._Strength += item.modifyStrength;
        player._Dex += item.modifyDex;
        player._Magic += item.modifyMagic;

        if ((player._Health += item.restoreHealth) > player._MaxHealth)
        {
            player._Health = player._MaxHealth;
        } else
        {
            player._Health += item.restoreHealth;
        }
        if ((player._Mana += item.restoreHealth) > player._MaxMana)
        {
            player._Mana = player._MaxMana;
        }
        else
        {
            player._Mana += item.restoreMana;
        }

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
    /// <param name="playerMoney">The amount of money the current player has</param>
    /// <param name="itemValue">The value of the item being bought.</param>
    /// <param name="itemToBuy">The Item the player is buying</param>
    /// <param name="_PlayerInventory">The PlayerInventory</param>
    public void BuyItem(int playerMoney, int itemValue, Item itemToBuy, Item[] _PlayerInventory)
    {
        for (int i = 0; i < _PlayerInventory.Length - 2; i++)
        {
            if (_PlayerInventory[i] == null)
            {
                _PlayerInventory[i] = itemToBuy;
                playerMoney -= itemValue;
                Debug.Log("Bought a " + itemToBuy.name + " for " + itemValue + ". New Balance: " + playerMoney);
                break;
            }
            else
            {
                Debug.Log("Inventory full");
            }
        }
    }

    public bool CanBuy(int Money, int ItemPrice)
    {
        if (ItemPrice > Money)
        {
            return false;
        }
        else
        {
            return true;
        }
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
