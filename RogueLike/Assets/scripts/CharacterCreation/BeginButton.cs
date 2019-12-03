using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

    public class BeginButton : MonoBehaviour
    {
        public InputField InputFieldName;
        public Dropdown DropDownClassSelect;

        public void Start()
        {
            Dropdown.OptionData DOD0 = new Dropdown.OptionData("Class");
            Dropdown.OptionData DOD1 = new Dropdown.OptionData(PlayerClassEnum.Warrior.ToString());
            Dropdown.OptionData DOD2 = new Dropdown.OptionData(PlayerClassEnum.Mage.ToString());
            Dropdown.OptionData DOD3 = new Dropdown.OptionData(PlayerClassEnum.Ranger.ToString());
            DropDownClassSelect.options.Add(DOD0);
            DropDownClassSelect.options.Add(DOD1);
            DropDownClassSelect.options.Add(DOD2);
            DropDownClassSelect.options.Add(DOD3);
        }

        public void ButtonClicked()
        {
            if (InputFieldName.text == "" || InputFieldName.text == "Name" || DropDownClassSelect.value.Equals("0") == true)
            {
                EditorUtility.DisplayDialog("Input Required", "Please make sure you have entered a name and a class!", "I understand and will do better next time", "I understand and will do better next time");
                return;
            }
            Player player = new Player(InputFieldName.text, DropDownClassSelect.value);
        }
    }
