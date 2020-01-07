using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatusBars : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _HealthText = null;
    [SerializeField] private TextMeshProUGUI _ManaText = null;
    [SerializeField] private TextMeshProUGUI _ExpText = null;
    [SerializeField] private Player _Player = null;

    void Start()
    {
        
    }

    void Update()
    {
        SetHealthText();
        SetManaText();
        SetExpText();
    }

    private void SetHealthText()
    {
        string[] healths = _Player.GetHealthValues();
        _HealthText.text = healths[0] + "/" + healths[1];
    }
    private void SetManaText()
    {
        _ManaText.text = _Player._Mana + "/" + _Player._MaxMana;
    }
    private void SetExpText()
    {
        _ExpText.text = _Player.GetExperience();
    }
}
