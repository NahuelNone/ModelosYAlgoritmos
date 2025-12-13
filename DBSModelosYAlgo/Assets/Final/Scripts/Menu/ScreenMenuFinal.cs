using UnityEngine;
using UnityEngine.UI;

public class ScreenMenuFinal : MonoBehaviour, IScreenFinal
{
    Button[] _buttons;

    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>(true);
    }

    // Se llama cuando el menú pasa a ser la pantalla activa del stack
    public void Active()
    {
        gameObject.SetActive(true);

        foreach (var b in _buttons)
            b.interactable = true;

        // Apagar todos los niveles cuando estoy en el menú
        var config = FindObjectOfType<ConfigSMFinal>();
        if (config != null)
            config.DeactivateAllLevels();
    }


    // Se llama cuando otra pantalla se apila encima (ej: el juego)
    public void Deactivate()
    {
        foreach (var b in _buttons)
            b.interactable = false;

        // Si no querés que desaparezca el menú cuando entras al juego,
        // podés comentar esta línea.
        gameObject.SetActive(false);
    }

    public string Free()
    {
        // Si nunca vas a volver al menú, podés destruirlo
        // o simplemente dejarlo activo/desactivo.
        Destroy(gameObject);
        return "Menu destroyed";
    }

    // -------------- BOTONES --------------

    // Botón “Jugar”
    public void BTN_Play()
    {
        Debug.Log("BTN_Play ejecutado");

        var config = FindObjectOfType<ConfigSMFinal>();
        if (config != null)
            config.StartGame();
        else
            Debug.LogWarning("ConfigSMFinal no encontrado desde ScreenMenuFinal.");
    }

    // Botón “Salir”
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
