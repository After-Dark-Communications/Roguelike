using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryItemsDisplay : MonoBehaviour
{
    [SerializeField] private Player _Player = null;

    private TextMeshProUGUI InventoryText;
    private Item[] InventoryItems;

    private int ScrollHeight = 0;

    private void Awake()
    {
        InventoryText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
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
            if (ScrollHeight < InventoryItems.Length - 1)
            {
                ScrollHeight += 1;
            }
        }
        DisplayItemNames();
    }

    private void GetInventoryItems()
    {
        InventoryItems = _Player._inventory;
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
