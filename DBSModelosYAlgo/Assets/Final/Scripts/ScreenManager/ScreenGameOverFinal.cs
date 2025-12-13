using UnityEngine;
using UnityEngine.UI;

public class ScreenGameOverFinal : MonoBehaviour, IScreenFinal
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
        return "GameOver screen deleted";
    }

    public void BTN_Menu()
    {
        var config = FindObjectOfType<ConfigSMFinal>();
        if (config != null)
            config.ReturnToMenu();
    }
}
