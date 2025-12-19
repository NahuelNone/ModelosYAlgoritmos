using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigSMFinal : MonoBehaviour
{

    public GameObject menu;

    public Transform level1Root;
    public Transform level2Root;
    public Transform level3Root;

    public ScreenMenuFinal menuScreen;

    private Transform currentLevelRoot;

    private int currentLevelIndex = 0;

    public static int LevelToStartOnLoad = 0;

    public bool pausa;

    [SerializeField] private int maxLevelIndex = 3;

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
            int lvl = LevelToStartOnLoad;
            LevelToStartOnLoad = 0;
            StartLevel(lvl);
        }
        else
        {
            ScreenManagerFinal.Instance.Push(menuScreen);
        }
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

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

    public void StartGame()
    {
        StartLevel(1);
    }
    public void StartLevel(int levelIndex)
{
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

    chosen.gameObject.SetActive(true);

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
            pm.TogglePause();

        pausa = true;
        Debug.Log("Pausa");
    }

    public void ClosePause()
    {
        if (!pausa) return;

        ScreenManagerFinal.Instance.Pop();   

        PauseManagerFinal pm = FindObjectOfType<PauseManagerFinal>();
        if (pm != null)
            pm.TogglePause();  

        pausa = false;
        Debug.Log("Cerrando pausa");
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        pausa = false;

        LevelToStartOnLoad = 0;  
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

        currentLevelRoot = null;
        currentLevelIndex = 0;
    }

    public void GoToNextLevel()
    {
        if (currentLevelIndex <= 0)
        {
            Debug.LogWarning("GoToNextLevel llamado sin nivel actual.");
            return;
        }

        int next = currentLevelIndex + 1;

        if (next > maxLevelIndex)
        {
            Time.timeScale = 1f;
            pausa = false;

            LevelToStartOnLoad = 0;   
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
            return;
        }

        Time.timeScale = 1f;
        pausa = false;

        LevelToStartOnLoad = next; 
        var s = SceneManager.GetActiveScene();
        SceneManager.LoadScene(s.buildIndex);
    }

}
