using UnityEngine;
using UnityEngine.UI;

public class ScreenMenuFinal : MonoBehaviour, IScreenFinal
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

        var config = FindObjectOfType<ConfigSMFinal>();
        if (config != null)
            config.DeactivateAllLevels();
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
        return "Menu destroyed";
    }

    public void BTN_Play()
    {
        Debug.Log("BTN_Play ejecutado");

        var config = FindObjectOfType<ConfigSMFinal>();
        if (config != null)
            config.StartGame();
        else
            Debug.LogWarning("ConfigSMFinal no encontrado desde ScreenMenuFinal.");
    }

    public void BTN_Quit()
    {

        Debug.Log("BTN_Quit ejecutado");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void BTN_Levels()
    {
        ScreenManagerFinal.Instance.Push("Canvas_Levels");
    }


}
