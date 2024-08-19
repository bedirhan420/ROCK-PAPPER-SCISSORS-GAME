using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combobox : MonoBehaviour
{
    // Start is called before the first frame update
     public Dropdown dropdown;
    public RPS rps; // RPS scriptini buraya sürükleyip bırakın

    private void Start()
    {
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int index)
    {
        string selectedOption = dropdown.options[index].text;
        if (rps != null && rps.winner == selectedOption)
        {
            // Kazandıysanız bir mesaj gösterin
            Debug.Log("Kazandınız!");
        }
    }
}
