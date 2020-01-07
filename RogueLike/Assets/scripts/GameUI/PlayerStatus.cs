using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private Player _Player = null;
    private string _StatusValuesString;
    private TextMeshProUGUI StatusValuesText;

    void Start()
    {
        StatusValuesText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        UpdateValueString();
        StatusValuesText.text = _StatusValuesString;
    }

    // player name
    // -
    // player level
    // stength
    // dexterity
    // magic
    // armor class
    // -
    // -
    // not gonna happen
    // not gonna happen
    // not gonna happen
    // -
    // player gold
    // dungeon level
    // turn count

    private void UpdateValueString()
    {
        string[] playerValues = _Player.GetStatusValues();
        playerValues[0] = "Test Naam";
        _StatusValuesString = playerValues[0] + "\n" + "\n" +
                                playerValues[1] + "\n" +
                                playerValues[2] + "\n" +
                                playerValues[3] + "\n" +
                                playerValues[4] + "\n" + "\n" +
                                playerValues[5] + "\n" +
                                "\n\n\n\n\n\n" +
                                playerValues[6] + "\n" +
                                playerValues[7] + "\n" +
                                "0";
    }
}
