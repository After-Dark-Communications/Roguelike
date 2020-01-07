using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryItemsDisplay : MonoBehaviour
{
    [SerializeField] private Player _Player = null;

    private TextMeshProUGUI InventoryText;
    private List<Item> InventoryItems = new List<Item>();

    private int ScrollHeight = 0;

    private void Awake()
    {
        InventoryText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        InventoryItems.Clear();
        GetInventoryItems();
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            if (ScrollHeight > 0)
            {
                ScrollHeight -= 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            if (ScrollHeight < InventoryItems.Count - 1)
            {
                ScrollHeight += 1;
            }
        }
        DisplayItemNames();
    }

    private void GetInventoryItems()
    {
        for (int i = 0; i < _Player._inventory.Length; i++)
        {
            if (_Player._inventory[i] != null)
            {
                InventoryItems.Add(_Player._inventory[i]);
            }
        }
    }

    private void DisplayItemNames()
    {
        InventoryText.text = "";

        for (int i = ScrollHeight; i < ScrollHeight + 10; i++)
        {
            Item item = InventoryItems[i];
            if (item != null)
            {
                if (i == ScrollHeight + 9)
                {
                    InventoryText.text += ItemColorTag(item.spriteColor) + "<alpha=#66>" + "v<pos=10%>" + item.name + "</color>" + "\n";
                }
                else if (i == ScrollHeight && ScrollHeight > 0)
                {
                    InventoryText.text += ItemColorTag(item.spriteColor) + "<alpha=#66>" + "^<pos=10%>" + item.name + "</color>" + "\n";
                }
                else if (item.isEquipped == true)
                {
                    InventoryText.text += ItemColorTag(item.spriteColor) + "E<pos=10%>" + item.name + "</color>" + "\n";
                }
                else
                {
                    InventoryText.text += ItemColorTag(item.spriteColor) + "1x<pos=10%>" + item.name + "</color>" + "\n";
                }
            }
        }
    }

    private string ItemColorTag(Color itemColor)
    {
        string colorTag = "<color=#" + ColorUtility.ToHtmlStringRGB(itemColor) + ">";
        return colorTag;
    }
}
