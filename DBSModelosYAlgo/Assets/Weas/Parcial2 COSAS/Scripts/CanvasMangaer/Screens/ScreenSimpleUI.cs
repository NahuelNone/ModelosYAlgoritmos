using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSimpleUI : MonoBehaviour, IScreen
{
    private Button[] _buttons;
    [SerializeField] private bool _disappearwhenDesactivate;
    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>(true);

        ActivateButtons(false);

        gameObject.SetActive(false);
    }

    void ActivateButtons(bool enableBool)
    {
        foreach (var button in _buttons)
        {
             button.interactable = enableBool;
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        ActivateButtons(true);

    }

    public void Desactivate()
    {
        if (_disappearwhenDesactivate)
        {
            gameObject.SetActive(false);
        }
        ActivateButtons(false);
    }

    public void Realease()
    {
        gameObject.SetActive(false);
    }
}
