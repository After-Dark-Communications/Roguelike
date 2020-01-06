using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using UnityEngine.SceneManagement;

public class BeginButton : MonoBehaviour
{
    public InputField InputFieldName;
    public Dropdown DropDownClassSelect;

    public void Start()
    {
        DropDownClassSelect.AddOptions(CreateOptions());
    }
    public void ButtonClicked()
    {
        if (InputFieldName.text == "" || InputFieldName.text == "Name" || DropDownClassSelect.value.Equals("0") == true)
        {
            //EditorUtility.DisplayDialog("Input Required", "Please make sure you have entered a name and a class!", "I understand and will do better next time", "I understand and will do better next time");
            return;
        }
        Player player = new Player(InputFieldName.text, DropDownClassSelect.value);
        SceneManager.LoadScene(2);
    }

    private List<Dropdown.OptionData> CreateOptions()
    {
        List<Dropdown.OptionData> TempList = new List<Dropdown.OptionData>();
        Dropdown.OptionData DefaultDOD = new Dropdown.OptionData("Class");
        TempList.Add(DefaultDOD);
        foreach (string classes in Enum.GetNames(typeof(PlayerClassEnum)))
        {
            TempList.Add(new Dropdown.OptionData(classes));
        }
        Dropdown.OptionData HiddenDOD = new Dropdown.OptionData();
        TempList.Add(HiddenDOD);
        return TempList;
    }
}
