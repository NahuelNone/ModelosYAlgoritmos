using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigSMFinal : MonoBehaviour
{

    public GameObject menu;

    // Raíces de cada nivel (asignar en el Inspector)
    public Transform level1Root;
    public Transform level2Root;
    public Transform level3Root;

    // Menú principal (asignar el objeto que tiene ScreenMenuFinal)
    public ScreenMenuFinal menuScreen;

    // Nivel actualmente activo
    private Transform currentLevelRoot;

    private int currentLevelIndex = 0;

    public static int LevelToStartOnLoad = 0;

    public bool pausa;

    private void Start()
    {

        menu.SetActive(true);

        if (level1Root != null) level1Root.gameObject.SetActive(false);
        if (level2Root != null) level2Root.gameObject.SetActive(false);
        if (level3Root != null) level3Root.gameObject.SetActive(false);

        currentLevelRoot = null;
        pausa = false;

        if (LevelToStartOnLoad > 0)
        {
            // venimos de un "Retry"
            int lvl = LevelToStartOnLoad;
            LevelToStartOnLoad = 0;
            StartLevel(lvl);
        }
        else
        {
            // arranque normal → menú
            ScreenManagerFinal.Instance.Push(menuScreen);
        }
    }



    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        // si hay una pantalla de Game Over, ESC no hace nada
        if (FindObjectOfType<ScreenGameOverFinal>() != null)
            return;

        if (currentLevelRoot == null || !currentLevelRoot.gameObject.activeInHierarchy)
            return;

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
    // APAGAR TODO
    DeactivateAllLevels();

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

    currentLevelIndex = levelIndex;
    currentLevelRoot = chosen;

    // Encendemos SOLO este nivel
    chosen.gameObject.SetActive(true);

    // Empuja la pantalla de juego y desactiva Menú / Niveles
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

        LevelToStartOnLoad = 0;   // para que arranque en el menú
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void RestartCurrentLevel()
    {
        if (currentLevelIndex == 0) return;

        Time.timeScale = 1f;
        pausa = false;

        LevelToStartOnLoad = currentLevelIndex;
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void ShowGameOver()
    {
        // evitar duplicados
        if (FindObjectOfType<ScreenGameOverFinal>() != null)
            return;

        Time.timeScale = 0f;

        GameObject prefab = Resources.Load<GameObject>("Screen_GameOver");
        GameObject go = Instantiate(prefab);
        ScreenGameOverFinal screen = go.GetComponent<ScreenGameOverFinal>();

        ScreenManagerFinal.Instance.Push(screen);
    }

    public void DeactivateAllLevels()
    {
        if (level1Root != null) level1Root.gameObject.SetActive(false);
        if (level2Root != null) level2Root.gameObject.SetActive(false);
        if (level3Root != null) level3Root.gameObject.SetActive(false);

        // ya no hay nivel activo
        currentLevelRoot = null;
        currentLevelIndex = 0;
    }


}
