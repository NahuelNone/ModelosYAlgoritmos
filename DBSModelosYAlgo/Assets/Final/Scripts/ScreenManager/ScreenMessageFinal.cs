using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenMessageFinal : MonoBehaviour, IScreenFinal
{

    Button[] _buttons;

    private void Awake()
    {

        _buttons = GetComponentsInChildren<Button>();

        foreach (var button in _buttons)
        {

            button.interactable = false;

        }

    }

    public void Active()
    {

        foreach (var button in _buttons)
        {

            button.interactable = true;

        }

    }

    public void Deactivate()
    {

        foreach (var button in _buttons)
        {

            button.interactable = false;

        }

    }   

    public string Free()
    {

        Destroy(gameObject);

        return "Messege Screen Deleted";

    }   

}
