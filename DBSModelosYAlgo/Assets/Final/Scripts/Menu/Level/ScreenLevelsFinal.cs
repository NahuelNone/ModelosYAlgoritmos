using UnityEngine;
using UnityEngine.UI;

public class ScreenLevelsFinal : MonoBehaviour, IScreenFinal
{
    Button[] _buttons;

    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>(true);
    }

    public void Active()
    {
        gameObject.SetActive(true);
        foreach (var b in _buttons)
            b.interactable = true;
    }

    public void Deactivate()
    {
        foreach (var b in _buttons)
            b.interactable = false;
        gameObject.SetActive(false);
    }

    public string Free()
    {
        Destroy(gameObject);
        return "Levels screen deleted";
    }

    public void BTN_Back()
    {
        ScreenManagerFinal.Instance.Pop();
    }

    public void BTN_Level1()
    {
        var config = FindObjectOfType<ConfigSMFinal>();
        if (config != null)
            config.StartGame();
        else
            Debug.LogWarning("ConfigSMFinal no encontrado desde ScreenLevelsFinal.");
    }

    public void BTN_Level2()
    {
        var config = FindObjectOfType<ConfigSMFinal>();
        if (config != null)
            config.StartGame();
        else
            Debug.LogWarning("ConfigSMFinal no encontrado desde ScreenLevelsFinal.");
    }

    public void BTN_Level3()
    {
        var config = FindObjectOfType<ConfigSMFinal>();
        if (config != null)
            config.StartGame();
        else
            Debug.LogWarning("ConfigSMFinal no encontrado desde ScreenLevelsFinal.");
    }


}
