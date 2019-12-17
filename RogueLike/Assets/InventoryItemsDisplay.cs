using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryItemsDisplay : MonoBehaviour
{
    [SerializeField] private Player _Player = null;

    private TextMeshProUGUI InventoryText;
    private Item[] InventoryItems;
    private List<string> ExistingItems = new List<string>();

    private void Awake()
    {
        InventoryText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInventoryItems();
        DisplayItemNames();
    }

    private void GetInventoryItems()
    {
        InventoryItems = _Player._inventory;
    }

    private void DisplayItemNames()
    {
        InventoryText.text = "";
        foreach (Item item in InventoryItems)
        {
            if (item != null)
            {
                //if (ExistingItems.Contains(item.name))
                //{
                //    string currentInventory = InventoryText.text;

                //}
                //else
                //{

                //}
                ExistingItems.Add(item.name);
                InventoryText.text += ItemColorTag(item.spriteColor) + "1x<pos=10%>" + item.name + "</color>" + "\n";
            }
        }
    }

    private string ItemColorTag(Color itemColor)
    {
        string colorTag = "<color=#" + ColorUtility.ToHtmlStringRGB(itemColor) + ">";
        return colorTag;
    }
}
