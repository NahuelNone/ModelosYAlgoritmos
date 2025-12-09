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

        // Buscamos el ConfigSMFinal de la escena
        configSMFinal = FindObjectOfType<ConfigSMFinal>();
    }

    // Botón que abre la pantalla de mensaje
    public void BTN_Messege()
    {
        _result = "Messege";
        ScreenManagerFinal.Instance.Push("Canvas_Messege");
    }

    // Botón "Volver" (reanudar el juego)
    public void BTN_Back()
    {
        if (configSMFinal != null)
            configSMFinal.ClosePause();
        else
            Debug.LogWarning("ConfigSMFinal no encontrado.");
    }

    // === Implementación de IScreenFinal ===
    public void Active()
    {

        gameObject.SetActive(true);

        // Solo activamos los botones; la pausa la maneja ConfigSMFinal
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
        // Esta pantalla se está eliminando del stack
        Destroy(gameObject);
        return _result;
    }

}
