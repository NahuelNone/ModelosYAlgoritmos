using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenPauseFinal : MonoBehaviour, IScreenFinal
{
    public Button[] _buttons;
    string _result;

    ConfigSMFinal configSMFinal;

    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>(true);

        foreach (var button in _buttons)
            button.interactable = false;

        configSMFinal = FindObjectOfType<ConfigSMFinal>();
    }

    public void BTN_Messege()
    {
        _result = "Messege";
        ScreenManagerFinal.Instance.Push("Canvas_Messege");
    }

    public void BTN_Back()
    {
        if (configSMFinal != null)
            configSMFinal.ClosePause();
        else
            Debug.LogWarning("ConfigSMFinal no encontrado.");
    }

    public void Active()
    {

        gameObject.SetActive(true);

        foreach (var button in _buttons)
            button.interactable = true;
    }

    public void Deactivate()
    {
        foreach (var button in _buttons)
            button.interactable = false;
    }

    public string Free()
    {
        Destroy(gameObject);
        return _result;
    }

    public void BTN_Menu()
    {
        if (configSMFinal != null)
            configSMFinal.ReturnToMenu();
        else
            Debug.LogWarning("ConfigSMFinal no encontrado desde pausa.");
    }

}
