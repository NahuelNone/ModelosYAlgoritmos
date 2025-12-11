using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigSMFinal : MonoBehaviour
{
    // Raíces de cada nivel (asignar en el Inspector)
    public Transform level1Root;
    public Transform level2Root;
    public Transform level3Root;

    // Menú principal (asignar el objeto que tiene ScreenMenuFinal)
    public ScreenMenuFinal menuScreen;

    // Nivel actualmente activo
    private Transform currentLevelRoot;

    public bool pausa;

    private void Start()
    {
        // Apagamos todos los niveles al arrancar
        if (level1Root != null) level1Root.gameObject.SetActive(false);
        if (level2Root != null) level2Root.gameObject.SetActive(false);
        if (level3Root != null) level3Root.gameObject.SetActive(false);

        currentLevelRoot = null;

        // Arrancamos en el menú
        ScreenManagerFinal.Instance.Push(menuScreen);
        pausa = false;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        // Si no hay nivel activo, estamos en menú / niveles → no hay pausa
        if (currentLevelRoot == null || !currentLevelRoot.gameObject.activeInHierarchy)
            return;

        // Si hay una pantalla de mensaje arriba, ESC la cierra primero
        if (FindObjectOfType<ScreenMessageFinal>() != null)
        {
            ScreenManagerFinal.Instance.Pop();
            return;
        }

        if (!pausa)
            OpenPause();
        else
            ClosePause();
    }

    // --- Arrancar juego rápido desde el botón JUGAR del menú ---
    public void StartGame()
    {
        StartLevel(1);
    }

    // --- Arrancar un nivel concreto desde la pantalla de niveles ---
    public void StartLevel(int levelIndex)
    {
        // Apagamos todos los niveles
        if (level1Root != null) level1Root.gameObject.SetActive(false);
        if (level2Root != null) level2Root.gameObject.SetActive(false);
        if (level3Root != null) level3Root.gameObject.SetActive(false);

        Transform chosen = null;

        switch (levelIndex)
        {
            case 1: chosen = level1Root; break;
            case 2: chosen = level2Root; break;
            case 3: chosen = level3Root; break;
            default:
                Debug.LogError("Nivel inválido: " + levelIndex);
                return;
        }

        if (chosen == null)
        {
            Debug.LogError("No se asignó el Transform del nivel " + levelIndex + " en ConfigSMFinal.");
            return;
        }

        // Guardamos cuál es el nivel actual
        currentLevelRoot = chosen;

        // Encendemos ese nivel
        chosen.gameObject.SetActive(true);

        // Lo apilamos como pantalla jugable
        ScreenManagerFinal.Instance.Push(new ScreenGOFinal(chosen));
    }

    public void OpenPause()
    {
        if (pausa) return;

        GameObject prefab = Resources.Load<GameObject>("PausaScreen");
        GameObject go = Instantiate(prefab);
        ScreenPauseFinal pauseScreen = go.GetComponent<ScreenPauseFinal>();

        ScreenManagerFinal.Instance.Push(pauseScreen);

        PauseManagerFinal pm = FindObjectOfType<PauseManagerFinal>();
        if (pm != null)
            pm.TogglePause();   // Time.timeScale = 0

        pausa = true;
        Debug.Log("Pausa");
    }

    public void ClosePause()
    {
        if (!pausa) return;

        ScreenManagerFinal.Instance.Pop();   // saca la pantalla de pausa

        PauseManagerFinal pm = FindObjectOfType<PauseManagerFinal>();
        if (pm != null)
            pm.TogglePause();   // Time.timeScale = 1

        pausa = false;
        Debug.Log("Cerrando pausa");
    }

    // Desde el menú de pausa: volver al menú principal y reiniciar todo
    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        pausa = false;

        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }
}
